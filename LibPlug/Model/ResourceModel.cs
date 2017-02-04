using LibPlug.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LibPlug.Model
{
    /// <summary>
    /// 插件与主程序共享资源模型
    /// </summary>
    public class ResourceModel
    {
        /// <summary>
        /// 歌曲信息集合
        /// </summary>
        public Dictionary<int, MusicInfoModel> MusicInfos { get; set; }
        /// <summary>
        /// 主UI界面ListView
        /// </summary>
        public ListViewNF UI_Main_ListView { get; set; }
        /// <summary>
        /// 主UI界面列表右键关联菜单
        /// </summary>
        public ContextMenuStrip UI_Main_ListView_RightClickMenu { get; set; }
        /// <summary>
        /// 主UI界面顶部菜单
        /// </summary>
        public ToolStrip UI_Main_TopButtonMenu { get; set; }
        /// <summary>
        /// 主UI底部状态标签
        /// </summary>
        public ToolStripStatusLabel UI_Main_BottomLabel { get; set; }
    }
}
