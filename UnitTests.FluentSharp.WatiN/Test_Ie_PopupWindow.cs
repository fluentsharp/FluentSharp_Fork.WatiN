using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_Ie_PopupWindow
    {
        //Settings.WaitForCompleteTimeOut = 0;
        [Test]
        public void Open_IE_PopupWindow()
        {
            var panel = "IE in PopupWindow".popupWindow().insert_LogViewer();
            var watinIe = panel.add_IE();
            
            var url_Before = watinIe.url();
            var htmlDocument_Before = watinIe.htmlDocument();
            var htmlDocumentElement_Before = watinIe.htmlDocumentElement();            
            
            watinIe.open("http://www.google.co.uk");

            var htmlDocument_After = watinIe.htmlDocument();
            var htmlDocumentElement_After = watinIe.htmlDocumentElement();
            var url_After = watinIe.url();
            
            Assert.IsNotNull(panel);
            Assert.IsNotNull(watinIe);

            Assert.IsNull(url_Before);
            Assert.IsNull(htmlDocument_Before);     
            Assert.IsNull(htmlDocumentElement_Before);            
            
            Assert.IsNotNull(url_After);
            Assert.IsNotNull(htmlDocument_After);            
            Assert.IsNotNull(htmlDocumentElement_After);

            panel.parentForm().close();
        }
    }
}
