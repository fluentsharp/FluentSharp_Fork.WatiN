using System.Windows.Forms;
using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.REPL;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp_WatiN.ExtensionMethods
{
    [TestFixture]
    public class Test_WatiN_IE_ExtensionMethods_WinForms : NUnitTests
    {
        [Test] public void add_IE()
        {
            var panel = "IE in PopupWindow".popupWindow_Hidden();
            var ie = panel.add_IE();
            
            var url_Before = ie.url();
            var htmlDocument_Before = ie.htmlDocument();
            var htmlDocumentElement_Before = ie.htmlDocumentElement();            
            
            ie.open("http://o2platform.com");

            var htmlDocument_After = ie.htmlDocument();
            var htmlDocumentElement_After = ie.htmlDocumentElement();
            var url_After = ie.url();
            
            Assert.IsNotNull(panel);
            Assert.IsNotNull(ie);

            Assert.IsNull(url_Before);
            Assert.IsNull(htmlDocument_Before);     
            Assert.IsNull(htmlDocumentElement_Before);            
            
            Assert.IsNotNull(url_After);
            Assert.IsNotNull(htmlDocument_After);            
            Assert.IsNotNull(htmlDocumentElement_After);
            
            assert_Is_Empty(panel.parentForm().controls<TextBox>(true));
            panel.parentForm().close();
        }
        [Test] public void add_IE_with_NavigationBar()
        {
            var panel = "IE in PopupWindow with navigation bar".popupWindow();
            var ie = panel.add_IE_with_NavigationBar();
            ie.open("about:blank").waitForComplete();
            assert_Are_Equal(ie.url(), "about:blank");
            panel.parentForm().controls<TextBox>(true).assert_Not_Empty().assert_Size_Is(1);            
                        
            var textBox =  panel.parentForm().control<TextBox>(true);            
            
            assert_Are_Equal(textBox.get_Text(), "about:blank");

            panel.parentForm().show().bringToFront();
            
            textBox.set_Text("http://o2platform.com".line());
            
            ie.waitForComplete();
    
            assert_Are_Equal(ie.url(), "http://o2platform.com/");
        }
    }
}
