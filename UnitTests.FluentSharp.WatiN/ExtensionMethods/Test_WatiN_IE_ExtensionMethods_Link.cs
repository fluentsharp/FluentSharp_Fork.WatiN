using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.REPL;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_WatiN_IE_ExtensionMethods_Link : NUnitTests
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
        [Test] public void link()
        {            
            ie.links().assert_Empty();

            var id        = "id"       .add_5_RandomLetters();
            var innerText = "innerText".add_5_RandomLetters();
            ie.body().add_Link(id, "href",innerText);
                            
            ie.links().assert_Not_Empty()
                      .assert_Size_Is(1)
                      .first().assert_Not_Null()
                              .assert_Are_Equal((link)=>link.id(), id)
                              .assert_Are_Equal((link)=>link.innerText(), innerText);
            
            
            ie.link(id)       .assert_Not_Null();
            ie.link(innerText).assert_Not_Null();

            var firstLink = ie.links().first();
            assert_Are_Equal(firstLink, ie.link(id));
            assert_Are_Equal(firstLink, ie.link(innerText));            
        }
        [Test] public void links()
        {            
            ie.links().assert_Empty();

            var id1        = "id"       .add_5_RandomLetters();
            var id2        = "id"       .add_5_RandomLetters();
            var innerText1 = "innerText".add_5_RandomLetters();            
            var innerText2 = "innerText".add_5_RandomLetters();

            ie.body().add_Link(id1, "href",innerText1);
            ie.body().add_Link(id2, "href",innerText2);

            ie.links().assert_Not_Empty()
                      .assert_Size_Is(2);

            var firstLink  = ie.links().first();
            var secondLink = ie.links().second();

            assert_Are_Equal(firstLink , ie.link(id1));
            assert_Are_Equal(firstLink , ie.link(innerText1));      
            assert_Are_Equal(secondLink, ie.link(id2));
            assert_Are_Equal(secondLink, ie.link(innerText2));            

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
