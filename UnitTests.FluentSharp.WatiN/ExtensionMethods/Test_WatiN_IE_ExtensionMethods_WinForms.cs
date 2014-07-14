using System.Windows.Forms;
using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.REPL;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp.WatiN
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

            Assert.AreEqual(url_Before,"about:blank");
            Assert.NotNull(htmlDocument_Before);     
            Assert.NotNull(htmlDocumentElement_Before);            
            
            Assert.IsNotNull(url_After);
            Assert.IsNotNull(htmlDocument_After);            
            Assert.IsNotNull(htmlDocumentElement_After);
            
            assert_Is_Empty(panel.parentForm().controls<TextBox>(true));
            panel.parentForm().close();
        }
        [Test] public void popupWindow_With_IE()
        {
            var title = "IE title".add_5_RandomLetters();
            var ie    = title.popupWindow_With_IE(); 

            assert_Not_Null(ie);
            assert_Are_Equal(ie.parentForm().title(), title);
            ie.parentForm().close();
        }        
        [Test] public void popupWindow_Hidden_With_IE()
        {
            var title = "IE title".add_5_RandomLetters();
            var ie    = title.popupWindow_Hidden_With_IE(); 

            assert_Not_Null(ie);
            assert_Are_Equal(ie.parentForm().opacity(), 0);
            assert_Are_Equal(ie.parentForm().title(), title);
            ie.parentForm().close();
        }
        [Test] public void add_IE_Hidden_PopupWindow()
        {
            var title = "IE title".add_5_RandomLetters();
            var ie    = title.add_IE_Hidden_PopupWindow();
            ie.parentForm().assert_Not_Null     ()
                           .assert_Are_Equal    ((form)=>form.opacity(), 0)
                           .assert_Are_Equal    ((form)=>form.title()  , title)
                           .assert_Are_Not_Equal((form)=>form.title()  , "title")
                           .close();            
        }
        [Test] public void add_IE_with_NavigationBar()
        {
            this.ignore_If_Offline();
            var panel = "IE in PopupWindow with navigation bar".popupWindow_Hidden();
            var ie = panel.add_IE_with_NavigationBar();
            ie.open("about:blank").waitForComplete();
            assert_Are_Equal(ie.url(), "about:blank");
            panel.parentForm().controls<TextBox>(true).assert_Not_Empty().assert_Size_Is(1);            
                        
            var textBox =  panel.parentForm().control<TextBox>(true);            
            
            assert_Are_Equal(textBox.get_Text(), "about:blank");
                        
            textBox.set_Text("http://o2platform.com".line());
            
            ie.waitForComplete();
    
            assert_Are_Equal(ie.url(), "http://o2platform.com/");
        }
        [Test] public void parentForm()
        {
            var ie = "test".popupWindow_Hidden_With_IE();
            var parentForm = ie.parentForm();
            assert_Not_Null (parentForm);
            assert_Are_Equal(parentForm,ie.HostControl.parentForm());
            parentForm.close();
        }
    }
}
