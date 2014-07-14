using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_WatiN_IE_ExtensionMethods_Inject_Html : NUnitTests
    {
        public WatiN_IE ie;        
        [SetUp] public void setup()
        {
            ie = "Test_WatiN_IE_ExtensionMethods_Inject_Html".popupWindow_Hidden_With_IE();            
            
        }
        [TearDown] public void tearDown()
        {
            ie.parentForm().close();
        }
        [Test] public void add_Link()
        {
            var href        = "href"     .add_5_RandomLetters();
            var id          = "id"       .add_5_RandomLetters();
            var innerText   = "innerText".add_5_RandomLetters();

            ie.links().assert_Is_Empty();
            ie.body().add_Link(id, href, innerText);
            ie.links().assert_Not_Empty().assert_Size_Is(1);

            var link = ie.links().first();

            link.assert_Not_Null()
                .assert_Are_Equal(link.id,id)
                .assert_Contains (link.href,href)
                .assert_Are_Equal(link.innerText,innerText);
            
            assert_Are_Equal(link, ie.links().first());
            assert_Are_Equal(link, ie.elements("a").first());
            assert_Are_Equal(link, ie.link(id));            
            assert_Are_Equal(link, ie.link(innerText));

            var innerText2   = "innerText 2".add_5_RandomLetters();
            ie.body().add_Link(innerText2);
            var link_NoId = ie.links().second();
            link_NoId.assert_Not_Null()                 
                     .assert_Contains (link_NoId.id,"Link_Id_")
                     .assert_Contains (link_NoId.href,"#")
                     .assert_Are_Equal(link_NoId.innerText,innerText2);            
        }
        [Test] public void add_Button()
        {
            var id    = "id"       .add_5_RandomLetters();
            var name  = "name"     .add_5_RandomLetters();            
            var value = "value".add_5_RandomLetters();

            ie.buttons().assert_Is_Empty();
            ie.body().add_Button(id, name, value);
            
            ie.buttons().assert_Not_Empty().assert_Size_Is(1);

            var button = ie.buttons().first();

            button.assert_Not_Null()
                .assert_Are_Equal(button.id,id)
                .assert_Contains (button.name,name)
                .assert_Are_Equal(button.value,value);
            
            assert_Are_Equal(button, ie.buttons().first());
            assert_Are_Equal(button, ie.elements("input").first());
            assert_Are_Equal(button, ie.button(id));   
            assert_Are_Equal(button, ie.button(name)); 
            assert_Are_Equal(button, ie.button(value));

            var innerText2   = "innerText 2".add_5_RandomLetters();
            ie.body().add_Button(innerText2);
            var button_NoId = ie.buttons().second();
            button_NoId.assert_Not_Null()                 
                     .assert_Contains (button_NoId.id,"Button_Id_")
                     .assert_Contains (button_NoId.name,"Button_Name_")                     
                     .assert_Are_Equal(button_NoId.value,innerText2);            
        }
        [Test] public void add_H1()
        {            
            var id          = "id"       .add_5_RandomLetters();
            var innerText   = "innerText".add_5_RandomLetters();

            ie.elements("h1").assert_Is_Empty();

            ie.body().add_H1(id, innerText);
            ie.elements("h1").assert_Not_Empty().assert_Size_Is(1);

            var h1 = ie.elements("h1").first();

            h1.assert_Not_Null()              
              .assert_Are_Equal(h1.id,id)              
              .assert_Are_Equal(h1.innerText,innerText);
            
            assert_Are_Equal(h1, ie.elements("h1").first());      
            
            var innerText2 = "innerText 2".add_5_RandomLetters();
            ie.body().add_H1(innerText2);
            var h1_NoId = ie.elements("h1").second();
            h1_NoId.assert_Not_Null()                 
                   .assert_Contains (h1_NoId.id,"H1_Id_")                   
                   .assert_Are_Equal(h1_NoId.innerText,innerText2);        
        }            
    }
}
