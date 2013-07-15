using FluentSharp.CoreLib;
using SHDocVw;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_InternetExplorer
    {
        public static InternetExplorerClass internetExplorer(this WatiN_IE watinIe)
        {
            return watinIe.InternetExplorer;
        }
        public static WatiN_IE  fullScreen(this WatiN_IE ie)
        {
            return ie.fullScreen(true);
        }
        public static WatiN_IE  fullScreen(this WatiN_IE ie, bool value)
        {
            var internetExplorer = ie.internetExplorer();
            if (internetExplorer.notNull())
                internetExplorer.FullScreen = value;
            return ie;
        }
    }
}