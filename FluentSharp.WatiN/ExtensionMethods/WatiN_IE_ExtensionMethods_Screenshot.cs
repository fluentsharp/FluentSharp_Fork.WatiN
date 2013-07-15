using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Screenshot
    {
        public static string screenshot(this WatiN_IE watinIe)
        {
            if (watinIe.InternetExplorer.isNull())
            {
                "Screenshots can only be taken when IE is in a separate process".error();
                return null;
            }
            else
            {
                "taking screenshot".info();
                var targetFile = PublicDI.config.getTempFileInTempDirectory(".jpg"); 
                "JPG File: {0}".info(targetFile);
                watinIe.IE.CaptureWebPageToFile(targetFile); 
                return targetFile;
            }    		
        }
    }
}