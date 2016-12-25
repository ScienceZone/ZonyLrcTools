using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Linq;
using ZonyLrcTools.EnumDefine;
using ZonyLrcTools.Untils;
using LibPlug.Model;
using LibPlug;
using LibNet;
using LibPlug.Interface;

namespace ZonyLrcTools.UI
{
    public partial class UI_Main : Form
    {
        public UI_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置工作目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SetWorkDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _folderDlg = new FolderBrowserDialog();
            _folderDlg.Description = "请选择程序的工作目录:";
            _folderDlg.ShowDialog();

            if (!string.IsNullOrEmpty(_folderDlg.SelectedPath))
            {
                disEnabledButton();
                clearContainer();
                setBottomStatusText(StatusHeadEnum.NORMAL, "开始扫描目录...");
                progress_DownLoad.Value = 0;

                string[] _files = FileUtils.SearchFiles(_folderDlg.SelectedPath, SettingManager.SetValue.FileSuffixs.Split(';'));
                for (int i = 0; i < _files.Length; i++) GlobalMember.AllMusics.Add(i,new MusicInfoModel() { Path=_files[i]});

                if (_files.Length > 0)
                {
                    progress_DownLoad.Value = 0; progress_DownLoad.Maximum = GlobalMember.AllMusics.Count;
                    getMusicInfoAndFillList(GlobalMember.AllMusics);
                }
                else
                {
                    setBottomStatusText(StatusHeadEnum.NORMAL, "并没有搜索到文件...");
                    enabledButton();
                }
            }
        }

        /// <summary>
        /// 快捷键检测
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (listView_MusicInfos.SelectedItems.Count != 0)
            {
                if (keyData == (Keys.Control | Keys.S))
                {
                    int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                    MusicInfoModel _info = GlobalMember.AllMusics[_selectCount];
                    _info.Artist = textBox_Aritst.Text;
                    _info.SongName = textBox_MusicTitle.Text;
                    _info.Album = textBox_Album.Text;
                    GlobalMember.MusicTagPluginsManager.Plugins[0].SaveTag(_info, null, textBox_Lryic.Text);
                    MessageBox.Show("已经保存歌曲标签信息!", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return false;
        }

        private void UI_Main_Load(object sender, EventArgs e)
        {
            setBottomStatusText(StatusHeadEnum.WAIT, "等待用户操作...");

            if(GlobalMember.MusicTagPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR,"加载MusicTag插件管理器失败...");
            if (GlobalMember.LrcPluginsManager.LoadPlugins() == 0) setBottomStatusText(StatusHeadEnum.ERROR, "加载歌词下载插件失败...");

            SettingManager.Load();
            if (!SettingManager.SetValue.IsAgree) new UI_About().ShowDialog();
            if (SettingManager.SetValue.IsCheckUpdate) checkUpdate();

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
            if(listView_MusicInfos.SelectedItems.Count > 0)
            {
                var _tmpDic = new Dictionary<int, MusicInfoModel>();
                foreach (ListViewItem item in listView_MusicInfos.SelectedItems)
                {
                    var _query = GlobalMember.AllMusics.Where(x => x.Key == item.Index).SingleOrDefault();
                    _tmpDic.Add(_query.Key, _query.Value);
                    // 获得第一个选中的ListItem作为专辑信息显示
                    int _selectCount = item.Index;
                    #region > 加载歌曲信息 <
                    MusicInfoModel _info = GlobalMember.AllMusics[_selectCount];
                    textBox_Aritst.Text = _info.Artist;
                    textBox_MusicTitle.Text = _info.SongName;
                    textBox_Album.Text = _info.Album;
                    Stream _imgStream = GlobalMember.MusicTagPluginsManager.Plugins[0].LoadAlbumImg(_info.Path);
                    if (_imgStream != null) pictureBox_AlbumImage.Image = Image.FromStream(_imgStream);
                    if (_info.IsBuildInLyric) textBox_Lryic.Text = GlobalMember.MusicTagPluginsManager.Plugins[0].LoadLyricText(_info.Path);
                    #endregion
                }
            }
        }

        private void button_FeedBack_Click(object sender, EventArgs e)
        {
            new UI_FeedBack().ShowDialog();
        }

        /// <summary>
        /// 下载单首歌曲的歌词，支持多插件
        /// </summary>
        private void ToolStripMenuItem_DownLoadSelectMusic_Click(object sender, EventArgs e)
        {
            if(listView_MusicInfos.SelectedItems.Count != 0)
            {
                var _tempDic = new Dictionary<int, MusicInfoModel>();
                foreach (ListViewItem item in listView_MusicInfos.SelectedItems)
                {
                    _tempDic.Add(item.Index, GlobalMember.AllMusics[item.Index]);
                }
                // 选择下载插件
                var _dlg = new UI_PluginSelect();
                _dlg.ShowDialog();
                if (!string.IsNullOrEmpty(_dlg.SelectPluginName))
                {
                    var _plug = GlobalMember.LrcPluginsManager.BaseOnNameGetPlugin(_dlg.SelectPluginName);
                    parallelDownLoadLryic(_tempDic, _plug);
                }
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
            if (listView_MusicInfos.Items.Count != 0)
            {
                foreach (var item in GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(PluginTypesEnum.LrcSource))
                {
                    parallelDownLoadLryic(GlobalMember.AllMusics, item);
                }
            }
            else setBottomStatusText(StatusHeadEnum.ERROR, "请选择歌曲目录再尝试下载歌词！");
        }

        /// <summary>
        /// 下载列表当中所有的专辑图像
        /// </summary>
        private void button_DownLoadAlbumImage_Click(object sender, EventArgs e)
        {
            if (listView_MusicInfos.Items.Count != 0)
            {
                parallelDownLoadAlbumImg(GlobalMember.AllMusics);
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
                #region > 获得选中条目的歌曲信息并且加入容器 <
                var _tempDic = new Dictionary<int, MusicInfoModel>();
                foreach (ListViewItem item in listView_MusicInfos.SelectedItems)
                {
                    _tempDic.Add(item.Index, GlobalMember.AllMusics[item.Index]);
                }
                #endregion
                parallelDownLoadAlbumImg(_tempDic);
            }
        }

        /// <summary>
        /// 打开歌曲所在文件夹
        /// </summary>
        private void ToolStripMenuItem_OpenFileFolder_Click(object sender, EventArgs e)
        {
            if (listView_MusicInfos.SelectedItems.Count != 0)
            {
                int _selectCount = listView_MusicInfos.Items.IndexOf(listView_MusicInfos.FocusedItem);
                string _path = GlobalMember.AllMusics[_selectCount].Path;
                FileUtils.OpenFilePos(_path);
            }
        }

        /// <summary>
        /// 添加歌曲文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_AddDirectory_Click(object sender, EventArgs e)
        {
            disEnabledButton(); progress_DownLoad.Value = 0;
            FolderBrowserDialog _dlg = new FolderBrowserDialog();
            _dlg.Description = "请选择歌曲文件夹";
            if (_dlg.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(_dlg.SelectedPath))
                {
                    setBottomStatusText(StatusHeadEnum.NORMAL, "开始扫描目录...");
                    progress_DownLoad.Value = 0;
                    string[] _files = FileUtils.SearchFiles(_dlg.SelectedPath, SettingManager.SetValue.FileSuffixs.Split(';'));
                    if (_files.Length < 0)
                    {
                        setBottomStatusText(StatusHeadEnum.NORMAL, "没有搜索到任何支持的文件！");
                        return;
                    }
                    Dictionary<int, MusicInfoModel> _tmpDic = new Dictionary<int, MusicInfoModel>();
                    for (int i = 0; i < _files.Length; i++) _tmpDic.Add(GlobalMember.AllMusics.Count == 0 ? i : GlobalMember.AllMusics.Count + i, new MusicInfoModel { Path = _files[i] });
                    progress_DownLoad.Value = 0; progress_DownLoad.Maximum = _tmpDic.Count;
                    getMusicInfoAndFillList(_tmpDic);
                    GlobalMember.AllMusics.AddRange(_tmpDic);
                }
            }
            else enabledButton();
        }

        #region > 私有方法集合 <

        /// <summary>
        /// 并行下载歌词任务
        /// </summary>
        /// <param name="down">插件</param>
        /// <param name="list">待下载列表</param>
        private async void parallelDownLoadLryic(Dictionary<int, MusicInfoModel> list, IPlug_Lrc down)
        {
            setBottomStatusText(StatusHeadEnum.NORMAL, "正在下载歌词...");
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
                        if (down.DownLoad(item.Value.Artist, item.Value.SongName, out _lrcData,SettingManager.SetValue.IsDownTranslate))
                        {
                            string _lrcPath = null;
                            #region > 输出方式 <
                            if (SettingManager.SetValue.UserDirectory.Equals(string.Empty)) // 同目录
                            {
                                _lrcPath = Path.GetDirectoryName(item.Value.Path) + @"\" + Path.GetFileNameWithoutExtension(item.Value.Path) + ".lrc";
                            }
                            else // 自定义目录
                            {
                                _lrcPath = Path.Combine(SettingManager.SetValue.UserDirectory, Path.GetFileNameWithoutExtension(item.Value.Path) + ".lrc");
                            }
                            #endregion
                            #region > 将字节集编码进行转换 <
                            if (SettingManager.SetValue.EncodingName == "utf-8 bom") _lrcData = convertBytesToUTF_8_BOM(_lrcData);
                            else if (SettingManager.SetValue.EncodingName == "ANSI") _lrcData = Encoding.Convert(Encoding.UTF8, Encoding.Default, _lrcData);
                            else _lrcData = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(SettingManager.SetValue.EncodingName), _lrcData);
                            #endregion
                            FileUtils.WriteFile(_lrcPath, convertBytesToUTF_8_BOM(_lrcData));
                            listView_MusicInfos.Items[item.Key].SubItems[6].Text = "成功";
                        }
                        else listView_MusicInfos.Items[item.Key].SubItems[6].Text = "失败";
                    }
                    progress_DownLoad.Value += 1;
                });
                setBottomStatusText(StatusHeadEnum.SUCCESS, "歌词下载完成！");
                enabledButton();
            });
        }

        /// <summary>
        /// 并行下载专辑图像任务
        /// </summary>
        private async void parallelDownLoadAlbumImg(Dictionary<int, MusicInfoModel> list)
        {
            setBottomStatusText(StatusHeadEnum.NORMAL, "正在下载专辑图像...");
            progress_DownLoad.Maximum = list.Count; progress_DownLoad.Value = 0;
            await Task.Run(() =>
            {
                disEnabledButton();
                Parallel.ForEach(list, new ParallelOptions() { MaxDegreeOfParallelism = SettingManager.SetValue.DownloadThreadNum }, (info) =>
                {
                    lock (info.Value)
                    {
                        if (info.Value.IsAlbumImg) listView_MusicInfos.Items[info.Key].SubItems[6].Text = "略过";
                        else
                        {
                            byte[] _imgBytes;
                            if (GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(PluginTypesEnum.AlbumImg)[0].DownLoad(info.Value.Artist, info.Value.SongName, out _imgBytes, SettingManager.SetValue.IsDownTranslate))
                            {
                                GlobalMember.MusicTagPluginsManager.Plugins[0].SaveTag(info.Value, _imgBytes, string.Empty);
                                listView_MusicInfos.Items[info.Key].SubItems[6].Text = "成功";
                            }
                            else listView_MusicInfos.Items[info.Key].SubItems[6].Text = "失败";
                            progress_DownLoad.Value += 1;
                        }
                    }
                });
                setBottomStatusText(StatusHeadEnum.SUCCESS, "下载专辑图像完成...");
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
        private void fillMusicListView(Dictionary<int,MusicInfoModel> music)
        {
            setBottomStatusText(StatusHeadEnum.NORMAL, "正在填充列表...");
            progress_DownLoad.Value = 0;
            foreach (var info in music)
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
        /// 获得歌曲信息并且填充列表
        /// </summary>
        /// <param name="musics"></param>
        private async void getMusicInfoAndFillList(Dictionary<int, MusicInfoModel> musics)
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(musics, (item) =>
                {
                    GlobalMember.MusicTagPluginsManager.Plugins[0].LoadTag(item.Value.Path, item.Value);
                    progress_DownLoad.Value += 1;
                });
                fillMusicListView(musics);
                MessageBox.Show(string.Format("扫描成功，一共有{0}个音乐文件！", musics.Count), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setBottomStatusText(StatusHeadEnum.SUCCESS, string.Format("扫描成功，一共有{0}个音乐文件！", musics.Count));
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
        /// 清空容器
        /// </summary>
        private void clearContainer()
        {
            GlobalMember.AllMusics.Clear(); listView_MusicInfos.Items.Clear();
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
            int _currentVer = 0022;
            await Task.Run(() =>
            {
                string _updateInfo = new NetUtils().HttpGet("http://www.myzony.com/updateInfo.txt", Encoding.Default);
                string[] _result = _updateInfo.TrimEnd(',').Split(',');
                int _dstVer = int.Parse(_result[0]);
                string _url = _result[1];
                if (_currentVer < _dstVer)
                {
                    StringBuilder _sb = new StringBuilder();
                    foreach (var item in _result[2].Split('|'))
                    {
                        _sb.Append(item + "\r\n");
                    }
                    if (MessageBox.Show(string.Format("检测到新版本，是否下载?\r\n更新内容:\r\n{0}",_sb.ToString()), "检测到更新", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Process.Start(_url);
                    }
                }
            });
        }

        /// <summary>
        /// 将字节序列转换为UTF-8 带BOM格式
        /// </summary>
        /// <param name="source">待转换的字节序列</param>
        /// <returns></returns>
        private byte[] convertBytesToUTF_8_BOM(byte[] source)
        {
            byte[] _result = Encoding.Convert(Encoding.UTF8, SettingManager.SetValue.EncodingName.Equals("utf-8 bom") ? Encoding.UTF8 : Encoding.GetEncoding(SettingManager.SetValue.EncodingName), source);
            byte[] _tmpData = new byte[_result.Length + 3];
            _tmpData[0] = 0xef; _tmpData[1] = 0xbb; _tmpData[2] = 0xbf;
            Array.Copy(_result, 0, _tmpData, 3, _result.Length);
            return _result;
        }
        #endregion

        /// <summary>
        /// 保存专辑图像
        /// </summary>
        private void ToolStripMenuItem_SaveAlbumImage_Click(object sender, EventArgs e)
        {
            if (pictureBox_AlbumImage.Image != null)
            {
                SaveFileDialog _dlg = new SaveFileDialog();
                _dlg.Title = "保存专辑图像";
                _dlg.Filter = "*.png|*.png|*.bmp|*.bmp";
                _dlg.ShowDialog();
                if (!string.IsNullOrEmpty(_dlg.FileName))
                {
                    switch (Path.GetExtension(_dlg.FileName))
                    {
                        case ".png":
                            pictureBox_AlbumImage.Image.Save(_dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        case ".bmp":
                            pictureBox_AlbumImage.Image.Save(_dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                    }
                    setBottomStatusText(StatusHeadEnum.SUCCESS, "保存图像成功!");
                }
            }
            else setBottomStatusText(StatusHeadEnum.ERROR, "并没有图片让你保存哦!");
        }
    }
}