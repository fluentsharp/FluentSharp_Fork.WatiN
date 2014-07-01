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

namespace UnitTests.FluentSharp_WatiN.ExtensionMethods
{
    [TestFixture]
    public class Test_WatiN_IE_ExtensionMethods_Link : NUnitTests
    {
        [Test] public void link()
        {
            var ie = "links".add_IE_Hidden_PopupWindow();
            ie.showMessage("test");
            
            assert_True(ie.html().contains("test"));
            assert_Not_Null(ie.element("body"));

            //assert_Fail("tbc");
            ie.script_IE_WaitForClose();

            //ie.parentForm().close();
        }
        [Test] public void links()
        {            
            this.ignore_If_Offline();
            var firstLinkTitle = "GitHub Profile";
            var ie = "links".add_IE_Hidden_PopupWindow();
            ie.silent(true);
        
            ie.open("http://o2platform.com");
            var links = ie.links();
            links.assert_Not_Null()
                 .assert_Not_Empty();
            assert_Are_Equal(links.first().text(), firstLinkTitle);     
        }

        

    }
}
