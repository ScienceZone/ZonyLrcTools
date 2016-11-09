using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonyLrcTools.EnumDefine;
using System.Threading;
using ZonyLrcTools.Untils;
using LibPlug.Model;
using System.IO;
using LibPlug.Interface;
using LibPlug;

namespace ZonyLrcTools.UI
{
    public partial class UI_Main : Form
    {
        public UI_Main()
        {
            InitializeComponent();
        }

        private void button_SetWorkDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _folderDlg = new FolderBrowserDialog();
            _folderDlg.Description = "请选择程序的工作目录:";
            _folderDlg.ShowDialog();

            if(!string.IsNullOrEmpty(_folderDlg.SelectedPath))
            {
                disEnabledButton();
                setBottomStatusText(StatusHeadEnum.NORMAL, "开始扫描目录...");
                GlobalMember.AllMusics.Clear();listView_MusicInfos.Items.Clear();

                if (FileUtils.SearchFiles(_folderDlg.SelectedPath, SettingManager.SetValue.FileSuffixs.Split(';')))
                {
                    progress_DownLoad.Value = 0; progress_DownLoad.Maximum = GlobalMember.AllMusics.Count;
                    getMusicInfo(GlobalMember.AllMusics);
                }
                else
                {
                    setBottomStatusText(StatusHeadEnum.NORMAL, "没有搜索到文件...");
                    enabledButton();
                }
            }
        }

        private void UI_Main_Load(object sender, EventArgs e)
        {
            SettingManager.Load();
            if (!SettingManager.SetValue.IsAgree) new UI_About().ShowDialog();
            setBottomStatusText(StatusHeadEnum.WAIT, "等待用户操作...");

            if(GlobalMember.MusicTagPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR,"加载MusicTag插件管理器失败...");
            if (GlobalMember.LrcPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR, "加载歌词下载插件失败...");

            CheckForIllegalCrossThreadCalls = false;
        }

        private void UI_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_DonateAuthor_Click(object sender, EventArgs e)
        {
            new UI_Donate().ShowDialog();
        }

        private void button_AboutSoftware_Click(object sender, EventArgs e)
        {
            new UI_About().ShowDialog();
        }

        private void button_PluginsMrg_Click(object sender, EventArgs e)
        {
            new UI_PluginsManager().ShowDialog();
        }

        /// <summary>
        /// 填充主界面ListView
        /// </summary>
        /// <param name="musics"></param>
        private void fillMusicListView()
        {
            setBottomStatusText(StatusHeadEnum.NORMAL, "正在填充列表...");
            progress_DownLoad.Value = 0;
            foreach(var info in GlobalMember.AllMusics)
            {
                listView_MusicInfos.Items.Insert(info.Key, new ListViewItem(new string[]
                {
                    Path.GetFileName(info.Value.Path),
                    Path.GetDirectoryName(info.Value.Path),
                    info.Value.TagType,
                    info.Value.SongName,
                    info.Value.Artist,
                    info.Value.Album,
                    ""
                }));
                progress_DownLoad.Value += 1;
            }
        }

        /// <summary>
        /// 获得歌曲信息
        /// </summary>
        /// <param name="musics"></param>
        private async void getMusicInfo(Dictionary<int,MusicInfoModel> musics)
        {
            await Task.Run(() => 
            {
                Parallel.ForEach(musics, (item) =>
                {
                    GlobalMember.MusicTagPluginsManager.Plugins[0].LoadTag(item.Value.Path, item.Value);
                    progress_DownLoad.Value += 1;
                });
                fillMusicListView();
                MessageBox.Show(string.Format("扫描成功，一共有{0}个音乐文件！", GlobalMember.AllMusics.Count), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setBottomStatusText(StatusHeadEnum.SUCCESS, string.Format("扫描成功，一共有{0}个音乐文件！", GlobalMember.AllMusics.Count));
                enabledButton();
            });
        }

        private void listView_MusicInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView_MusicInfos.SelectedItems.Count != 0)
            {
                int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                textBox_Aritst.Text = GlobalMember.AllMusics[_selectCount].Artist;
                textBox_MusicTitle.Text = GlobalMember.AllMusics[_selectCount].SongName;
                textBox_Album.Text = GlobalMember.AllMusics[_selectCount].Album;
                Stream _imgStream = GlobalMember.MusicTagPluginsManager.Plugins[0].LoadAlbumImg(GlobalMember.AllMusics[_selectCount].Path);
                if(_imgStream != null) pictureBox_AlbumImage.Image = Image.FromStream(_imgStream);
            }
        }

        private void button_FeedBack_Click(object sender, EventArgs e)
        {
            new UI_FeedBack().ShowDialog();
        }

        private void ToolStripMenuItem_DownLoadSelectMusic_Click(object sender, EventArgs e)
        {
            if(listView_MusicInfos.SelectedItems.Count != 0)
            {
                int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                byte[] _lrcData;
                MusicInfoModel _info = GlobalMember.AllMusics[_selectCount];
                if(GlobalMember.LrcPluginsManager.Plugins[0].DownLoad(_info.Artist, _info.SongName, out _lrcData))
                {
                    string _lrcPath = Path.GetDirectoryName(_info.Path) + @"\" + Path.GetFileNameWithoutExtension(_info.Path) + ".lrc";
                    // 编码转换
                    _lrcData = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(SettingManager.SetValue.EncodingName), _lrcData);
                    FileUtils.WriteFile(_lrcPath, _lrcData);
                    listView_MusicInfos.Items[_selectCount].SubItems[6].Text = "成功";
                }else listView_MusicInfos.Items[_selectCount].SubItems[6].Text = "失败";
            }
        }

        private void button_Setting_Click(object sender, EventArgs e)
        {
            new UI_Settings().ShowDialog();
        }

        /// <summary>
        /// 歌词下载按钮点击事件
        /// </summary>
        private void button_DownLoadLyric_Click(object sender, EventArgs e)
        {
            parallelDownLoadLryic();
        }

        /// <summary>
        /// 并行下载歌词任务
        /// </summary>
        private async void parallelDownLoadLryic()
        {
            progress_DownLoad.Maximum = GlobalMember.AllMusics.Count; progress_DownLoad.Value = 0;
            await Task.Run(() => 
            {
                Parallel.ForEach(GlobalMember.AllMusics, new ParallelOptions() { MaxDegreeOfParallelism = SettingManager.SetValue.DownloadThreadNum }, (item) =>
                {
                    byte[] _lrcData;
                    if (GlobalMember.LrcPluginsManager.Plugins[0].DownLoad(item.Value.Artist, item.Value.SongName, out _lrcData))
                    {
                        string _lrcPath = Path.GetDirectoryName(item.Value.Path) + @"\" + Path.GetFileNameWithoutExtension(item.Value.Path) + ".lrc";
                        // 编码转换
                        _lrcData = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(SettingManager.SetValue.EncodingName), _lrcData);
                        FileUtils.WriteFile(_lrcPath, _lrcData);
                        listView_MusicInfos.Items[item.Key].SubItems[6].Text = "成功";
                    }
                    else listView_MusicInfos.Items[item.Key].SubItems[6].Text = "失败";
                    progress_DownLoad.Value += 1;
                });
            });
        }

        /// <summary>
        /// 下载列表当中所有的专辑图像
        /// </summary>
        private void button_DownLoadAlbumImage_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 下载单首歌曲的专辑图像
        /// </summary>
        private void ToolStripMenuItem_DownLoadSelectedAlbumImg_Click(object sender, EventArgs e)
        {
            if (listView_MusicInfos.SelectedItems.Count != 0)
            {
                byte[] _imgBytes;
                int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                MusicInfoModel _info = GlobalMember.AllMusics[_selectCount];
                if (_info.IsAlbumImg == true) return;
                GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(PluginTypesEnum.AlbumImg)[0].DownLoad(_info.Artist,_info.SongName,out _imgBytes);

            }
        }

        /// <summary>
        /// 设置底部状态标识文本
        /// </summary>
        /// <param name="head">状态标识</param>
        /// <param name="content">状态内容</param>
        private void setBottomStatusText(string head, string content)
        {
            statusLabel_StateText.Text = string.Format("{0}:{1}", head, content);
        }

        private bool write

        #region > 下载按钮启用/停用 <
        private void disEnabledButton()
        {
            button_SetWorkDirectory.Enabled = button_DownLoadLyric.Enabled = button_DownLoadAlbumImage.Enabled = false;
        }

        private void enabledButton()
        {
            button_SetWorkDirectory.Enabled = button_DownLoadLyric.Enabled = button_DownLoadAlbumImage.Enabled = true;
        }
        #endregion
    }
}
