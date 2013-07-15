using FluentSharp.REPL;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;
using WatiN.Core;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_Ie_PopupWindow
    {
        [Test]
        public void Open_IE_PopupWindow()
        {
            var panel = "IE in PopupWindow".popupWindow().insert_LogViewer();
            var watinIe = panel.add_IE();
            Settings.WaitForCompleteTimeOut = 0;
            watinIe.script_Me().waitForClose();
            //var url_Before = watinIe.url();
            watinIe.open("http://www.google.co.uk");
            var url_After = watinIe.url();
            Assert.IsNotNull(panel);
            Assert.IsNotNull(watinIe);
            //Assert.IsNotNull(url_Before);
            Assert.IsNotNull(url_After);
            panel.parentForm().close();
        }
    }
}
