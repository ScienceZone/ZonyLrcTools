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
                setBottomStatusText(StatusHeadEnum.NORMAL, "开始扫描目录...");
                button_SetWorkDirectory.Enabled = button_DownLoadLyric.Enabled = button_DownLoadAlbumImage.Enabled = false;
                GlobalMember.AllMusics.Clear();listView_MusicInfos.Items.Clear();

                if (FileUtils.SearchFiles(_folderDlg.SelectedPath, SettingManager.SetValue.FileSuffixs.Split(';')))
                {
                    progress_DownLoad.Value = 0; progress_DownLoad.Maximum = GlobalMember.AllMusics.Count;
                    new Thread(() => 
                    {
                        getMusicInfo(GlobalMember.AllMusics);
                        fillMusicListView(GlobalMember.AllMusics);
                        setBottomStatusText(StatusHeadEnum.SUCCESS, string.Format("扫描成功，一共有{0}个音乐文件！", GlobalMember.AllMusics.Count));
                        button_SetWorkDirectory.Enabled = button_DownLoadLyric.Enabled = button_DownLoadAlbumImage.Enabled = true;
                    }).Start();
                }
                else setBottomStatusText(StatusHeadEnum.NORMAL, "没有搜索到文件...");
            }
        }

        private void UI_Main_Load(object sender, EventArgs e)
        {
            SettingManager.Load();
            if (!SettingManager.SetValue.IsAgree) new UI_About().ShowDialog();
            setBottomStatusText(StatusHeadEnum.WAIT, "等待用户操作...");
            if(GlobalMember.MusicTagPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR,"加载MusicTag插件管理器失败...");

            CheckForIllegalCrossThreadCalls = false;
        }

        private void UI_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void setBottomStatusText(string head, string content)
        {
            statusLabel_StateText.Text = string.Format("{0}:{1}", head, content);
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
        private void fillMusicListView(Dictionary<int,MusicInfoModel> musics)
        {
            foreach(var info in musics)
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
        private void getMusicInfo(Dictionary<int,MusicInfoModel> musics)
        {
            Parallel.ForEach(musics, (item)=> 
            {
                //为每个Task实例化T对象
                //var _plug = GlobalMember.MusicTagPluginsManager.DllAssembly.CreateInstance(GlobalMember.MusicTagPluginsManager.Plug.FullName) as IPlug_MusicTag;
                //_plug.LoadTag(item.Value.Path, item.Value);
                GlobalMember.MusicTagPluginsManager.Plugins[0].LoadTag(item.Value.Path, item.Value);
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
                pictureBox_AlbumImage.Image = Image.FromStream(GlobalMember.MusicTagPluginsManager.Plugins[0].LoadAlbumImg(GlobalMember.AllMusics[_selectCount].Path));
            }
        }

        private void button_FeedBack_Click(object sender, EventArgs e)
        {
            new UI_FeedBack().ShowDialog();
        }
    }
}
