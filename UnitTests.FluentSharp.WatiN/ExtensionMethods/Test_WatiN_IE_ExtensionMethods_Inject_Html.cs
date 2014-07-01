using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp_WatiN.ExtensionMethods
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
    
        //Workflows
        [Test] public void Check_That_Links_With_NewLines_In_InnerText_Can_Still_Be_Found()
        {
            //fix and regression test for https://github.com/o2platform/FluentSharp/issues/3

            var simpleText = "this is inside the link";
            var linkText = simpleText.lineBeforeAndAfter();

            assert_Size_Is(ie.links(),0);

            ie.body().add_Link(linkText);

            assert_Size_Is(ie.links(),1);
            
            var link = ie.link(simpleText);

            assert_Not_Null(link);

            assert_Are_Equal(link, ie.link(simpleText));
            assert_Are_Equal(link, ie.link(linkText));

            //a tipical example of this problem was with simple links with spaces (as shown in the example below)

            ie.body().add_Link("    Login   ");
            assert_Size_Is (ie.links(),2);
            assert_Not_Null(ie.link("Login"));
            assert_Not_Null(ie.link("login"));
            assert_Not_Null(ie.link("LOGIN"));
            assert_Not_Null(ie.link("     Login"));
            assert_Not_Null(ie.link("     LOGIN"));

            assert_Is_True(ie.hasLink("Login"));
            assert_Is_True(ie.hasLink("login"));
            assert_Is_True(ie.hasLink("login"));
            assert_Is_True(ie.hasLink("     Login"  ));
            assert_Is_True(ie.hasLink("     LOGIN"  ));
            assert_Is_True(ie.hasLink("    Login   "));
            

        }
    }
}
