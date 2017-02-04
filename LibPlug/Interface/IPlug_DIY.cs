using LibPlug.Model;

namespace LibPlug.Interface
{
    public interface IPlug_DIY
    {
        PluginsAttribute PlugInfo { get; set; }
        void Init(ResourceModel shareResModel);
    }
}
