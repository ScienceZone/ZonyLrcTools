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
using System.Diagnostics;
using LibNet;

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
                GlobalMember.AllMusics.Clear();listView_MusicInfos.Items.Clear();progress_DownLoad.Value = 0;

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
            if (SettingManager.SetValue.IsCheckUpdate) checkUpdate();
            setBottomStatusText(StatusHeadEnum.WAIT, "等待用户操作...");

            if(GlobalMember.MusicTagPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR,"加载MusicTag插件管理器失败...");
            if (GlobalMember.LrcPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR, "加载歌词下载插件失败...");

            loadMenuIcon();
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

        private void listView_MusicInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView_MusicInfos.SelectedItems.Count != 0)
            {
                int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                MusicInfoModel _info = GlobalMember.AllMusics[_selectCount];
                textBox_Aritst.Text = _info.Artist;
                textBox_MusicTitle.Text = _info.SongName;
                textBox_Album.Text = _info.Album;
                Stream _imgStream = GlobalMember.MusicTagPluginsManager.Plugins[0].LoadAlbumImg(_info.Path);
                if(_imgStream != null) pictureBox_AlbumImage.Image = Image.FromStream(_imgStream);
                if (_info.IsBuildInLyric) textBox_Lryic.Text = GlobalMember.MusicTagPluginsManager.Plugins[0].LoadLyricText(_info.Path);
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
                var _tempDic = new Dictionary<int, MusicInfoModel>();
                _tempDic.Add(_selectCount, GlobalMember.AllMusics[_selectCount]);
                parallelDownLoadLryic(_tempDic);
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
            if (listView_MusicInfos.Items.Count != 0) parallelDownLoadLryic(GlobalMember.AllMusics);
            else setBottomStatusText(StatusHeadEnum.ERROR, "请选择歌曲目录再尝试下载歌词！");
        }

        /// <summary>
        /// 下载列表当中所有的专辑图像
        /// </summary>
        private void button_DownLoadAlbumImage_Click(object sender, EventArgs e)
        {
            if (listView_MusicInfos.Items.Count != 0)
            {
                disEnabledButton();
                parallelDownLoadAlbumImg();
            }
            else setBottomStatusText(StatusHeadEnum.ERROR, "请选择歌曲目录再尝试下载专辑图像！");
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
                if (_info.IsAlbumImg == true)
                {
                    listView_MusicInfos.Items[_selectCount].SubItems[6].Text = "略过";
                    setBottomStatusText(StatusHeadEnum.FAILED, "该歌曲已经有专辑图像了！");
                    return;
                }
                if(GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(PluginTypesEnum.AlbumImg)[0].DownLoad(_info.Artist, _info.SongName, out _imgBytes))
                {
                    GlobalMember.MusicTagPluginsManager.Plugins[0].SaveTag(_info,_imgBytes,string.Empty);
                    listView_MusicInfos.Items[_selectCount].SubItems[6].Text = "成功";
                    setBottomStatusText(StatusHeadEnum.SUCCESS, "下载专辑图像成功!");
                }
            }
        }

        /// <summary>
        /// 并行下载歌词任务
        /// </summary>
        private async void parallelDownLoadLryic(Dictionary<int,MusicInfoModel> list)
        {
            setBottomStatusText(StatusHeadEnum.NORMAL,"正在下载歌词...");
            progress_DownLoad.Maximum = list.Count; progress_DownLoad.Value = 0;
            await Task.Run(() => 
            {
                disEnabledButton();
                Parallel.ForEach(list, new ParallelOptions() { MaxDegreeOfParallelism = SettingManager.SetValue.DownloadThreadNum }, (item) =>
                {
                    string _path = Path.GetDirectoryName(item.Value.Path) + @"\" + Path.GetFileNameWithoutExtension(item.Value.Path) + ".lrc";
                    if (SettingManager.SetValue.IsIgnoreExitsFile && File.Exists(_path))
                    {
                        listView_MusicInfos.Items[item.Key].SubItems[6].Text = "略过";
                    }
                    else
                    {
                        byte[] _lrcData;
                        if (GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(PluginTypesEnum.LrcSource)[0].DownLoad(item.Value.Artist, item.Value.SongName, out _lrcData))
                        {
                            string _lrcPath = null;
                            _lrcData = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(SettingManager.SetValue.EncodingName), _lrcData);

                            #region > 输出方式 <
                            if (SettingManager.SetValue.UserDirectory.Equals(string.Empty)) // 同目录
                            {
                                _lrcPath = Path.GetDirectoryName(item.Value.Path) + @"\" + Path.GetFileNameWithoutExtension(item.Value.Path) + ".lrc";
                            }
                            else if (SettingManager.SetValue.UserDirectory.Equals("ID3v2")) // 写入ID3v2(bug)
                            {
                                #region > 注释 <
                                //string _lrcString = Encoding.GetEncoding(SettingManager.SetValue.EncodingName).GetString(_lrcData);
                                //GlobalMember.MusicTagPluginsManager.Plugins[0].SaveTag(item.Value, null, _lrcString);
                                //listView_MusicInfos.Items[item.Key].SubItems[6].Text = "成功";
                                //return;
                                #endregion
                            }
                            else // 自定义目录
                            {
                                _lrcPath = Path.Combine(SettingManager.SetValue.UserDirectory, Path.GetFileNameWithoutExtension(item.Value.Path) + ".lrc");
                            }
                            #endregion

                            // 编码转换
                            _lrcData = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(SettingManager.SetValue.EncodingName), _lrcData);
                            FileUtils.WriteFile(_lrcPath, _lrcData);
                            listView_MusicInfos.Items[item.Key].SubItems[6].Text = "成功";
                        }
                        else listView_MusicInfos.Items[item.Key].SubItems[6].Text = "失败";
                    }
                    progress_DownLoad.Value += 1;
                });
                enabledButton();
            });
        }

        /// <summary>
        /// 并行下载专辑图像任务
        /// </summary>
        private async void parallelDownLoadAlbumImg()
        {
            setBottomStatusText(StatusHeadEnum.NORMAL, "正在下载专辑图像...");
            await Task.Run(() => 
            {
                Parallel.ForEach(GlobalMember.AllMusics,new ParallelOptions() { MaxDegreeOfParallelism=SettingManager.SetValue.DownloadThreadNum }, (info) => 
                {
                    if (info.Value.IsAlbumImg) listView_MusicInfos.Items[info.Key].SubItems[6].Text = "略过";
                    else
                    {
                        byte[] _imgBytes;
                        if (GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(PluginTypesEnum.AlbumImg)[0].DownLoad(info.Value.Artist, info.Value.SongName, out _imgBytes))
                        {
                            GlobalMember.MusicTagPluginsManager.Plugins[0].SaveTag(info.Value, _imgBytes,string.Empty);
                            listView_MusicInfos.Items[info.Key].SubItems[6].Text = "成功";
                        }
                        else listView_MusicInfos.Items[info.Key].SubItems[6].Text = "失败";
                        progress_DownLoad.Value += 1;
                    }
                });
                enabledButton();
            });
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

        /// <summary>
        /// 填充主界面ListView
        /// </summary>
        /// <param name="musics"></param>
        private void fillMusicListView()
        {
            setBottomStatusText(StatusHeadEnum.NORMAL, "正在填充列表...");
            progress_DownLoad.Value = 0;
            foreach (var info in GlobalMember.AllMusics)
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
        private async void getMusicInfo(Dictionary<int, MusicInfoModel> musics)
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

        /// <summary>
        /// 打开歌曲所在文件夹
        /// </summary>
        private void ToolStripMenuItem_OpenFileFolder_Click(object sender, EventArgs e)
        {
            if(listView_MusicInfos.SelectedItems.Count != 0)
            {
                int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                string _path = GlobalMember.AllMusics[_selectCount].Path;
                FileUtils.OpenFilePos(_path);
            }
        }

        /// <summary>
        /// 加载菜单图标数据
        /// </summary>
        private void loadMenuIcon()
        {
            button_SetWorkDirectory.Image = Properties.Resources.directory;
            button_DownLoadAlbumImage.Image = button_DownLoadLyric.Image = Properties.Resources.download;
            button_FeedBack.Image = Properties.Resources.feedback;
            button_DonateAuthor.Image = Properties.Resources.donate;
            button_AboutSoftware.Image = Properties.Resources.about;
            button_PluginsMrg.Image = Properties.Resources.plugins;
            button_Setting.Image = Properties.Resources.setting;
            Icon = Properties.Resources.App;
        }

        /// <summary>
        /// 检测更新
        /// </summary>
        /// <returns></returns>
        private async void checkUpdate()
        {
            int _currentVer = 0010;
            await Task.Run(() => 
            {
                string _updateInfo =new NetUtils().HttpGet("http://www.myzony.com/updateInfo.txt", Encoding.Default);
                string[] _result = _updateInfo.TrimEnd(',').Split(',');
                int _dstVer = int.Parse(_result[0]);
                string _url = _result[1];
                if(_currentVer < _dstVer)
                {
                    if(MessageBox.Show("检测到新版本，是否下载?", "检测到更新", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Process.Start("Explorer " + _url);
                    }
                }
            });
        }
    }
}
