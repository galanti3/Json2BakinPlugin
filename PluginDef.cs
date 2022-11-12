using System.Drawing;
using System.Windows.Forms;

namespace Json2BakinPlugin
{
    public class PluginDef : Yukar.Common.Plugin.PluginDef
    {
        public override PluginType GetPluginType()
        {
            return PluginType.MINITOOL;
        }

        public override string GetName()
        {
            return Properties.Resources.PluginName;
        }
        
        public override DialogResult ShowDialog(IWin32Window parent)
        {
            using (var dialog = new TestForm())
            {
                return dialog.ShowDialog(parent);
            }
        }

        public override Image GetIcon()
        {
            return Properties.Resources.smile;
        }
    }
}
