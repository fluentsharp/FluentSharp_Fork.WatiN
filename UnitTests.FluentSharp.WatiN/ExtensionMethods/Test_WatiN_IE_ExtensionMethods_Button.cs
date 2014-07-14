using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_WatiN_IE_ExtensionMethods_Button : NUnitTests
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

        [Test] public void buttons()
        {
            ie.buttons().assert_Empty();
            
            var value1 = "value1".add_5_RandomLetters();
            var value2 = "value2".add_5_RandomLetters();

            ie.body().add_Button(value1)
                     .add_Button(value2);
            
            ie.buttons().assert_Not_Empty()
                        .assert_Size_Is(2)
                        .assert_Are_Equal(buttons => buttons.first() , ie.button(value1))
                        .assert_Are_Equal(buttons => buttons.second(), ie.button(value2));            
        }
    }
}
