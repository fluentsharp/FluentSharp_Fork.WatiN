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
    public class Test_WatiN_IE_ExtensionMethods_Elements : NUnitTests
    {
        public WatiN_IE ie;
        public string testMessage = "<p>this is a message:</p> <p><b>in bold</b> : <i>in italic</i></p>";
        [SetUp] public void setup()
        {
            ie = "Test_WatiN_IE_ExtensionMethods_Elements".popupWindow_Hidden_With_IE();
            ie.showMessage(testMessage);
        }
        [TearDown] public void tearDown()
        {
            ie.parentForm().close();
        }

        [Test] public void elements()
        {
            var elements = ie.elements();
            elements.assert_Not_Null()
                    .assert_Not_Empty()
                    .assert_Size_Is(9);            
        }

        [Test] public void elements__tagName()
        {
            var elements_P      = ie.elements("p"   );
            var elements_Body   = ie.elements("body");
            var elements_Head   = ie.elements("head");
            var elements_B      = ie.elements("b"   );
            var elements_I      = ie.elements("i"   );
            var elements_ABC    = ie.elements("abc" );
            var elements_Null   = ie.elements(null  );
            var elements_Empty  = ie.elements(""    );
            
            elements_P    .assert_Not_Null().assert_Size_Is(2);   
            elements_Body .assert_Not_Null().assert_Size_Is(1);
            elements_Head .assert_Not_Null().assert_Size_Is(1);
            elements_B    .assert_Not_Null().assert_Size_Is(1);            
            elements_I    .assert_Not_Null().assert_Size_Is(1);
            elements_ABC  .assert_Not_Null().assert_Size_Is(0);
            elements_Null .assert_Not_Null().assert_Size_Is(0);
            elements_Empty.assert_Not_Null().assert_Size_Is(0);


            elements_P.assert_Is_Equal_To    (ie.elements("p"));
            elements_P.assert_Is_Equal_To    (ie.elements("P"));
            elements_P.assert_Is_Equal_To    (ie.elements("   P"));
            elements_P.assert_Is_Equal_To    (ie.elements("P   "));
            elements_P.assert_Is_Equal_To    (ie.elements("P".line()));
            elements_P.assert_Is_Equal_To    (ie.elements("P \t"));
            elements_P.assert_Is_Equal_To    (ie.elements("P \r"));
            elements_P.assert_Is_Equal_To    (ie.elements("P \n"));
            elements_P.assert_Is_Equal_To    (ie.elements("P \r\n"));            
            elements_P.assert_Is_Not_Equal_To(ie.elements("i"));
            elements_P.assert_Is_Not_Equal_To(ie.elements("body"));
            elements_P.assert_Is_Not_Equal_To(ie.elements(""));

            elements_Body.assert_Is_Equal_To     (ie.elements("body"));
            elements_Body.assert_Is_Equal_To     (ie.elements("Body"));
            elements_Body.assert_Is_Equal_To     (ie.elements("BODY"));
            elements_Body.assert_Is_Equal_To     (ie.elements("bodY"));
            elements_Body.assert_Is_Equal_To     (ie.elements("Body   "));
            elements_Body.assert_Is_Equal_To     (ie.elements("   Body"));
            elements_Body.assert_Is_Not_Equal_To (ie.elements("Bo dy"));
            elements_Body.assert_Is_Not_Equal_To (ie.elements("Bod"));
            elements_Body.assert_Is_Not_Equal_To (ie.elements("p"));
            elements_Body.assert_Is_Not_Equal_To (ie.elements(""));
        }

        [Test] public void element__tagName()
        {
            var element_P      = ie.element("p"   );
            var element_Body   = ie.element("body");
            var element_Head   = ie.element("head");
            var element_B      = ie.element("b"   );
            var element_I      = ie.element("i"   );
            var element_ABC    = ie.element("abc" );
            var element_Null   = ie.element(null  );
            var element_Empty  = ie.element(""    );
            
            element_P    .assert_Not_Null().assert_Are_Equal((element)=>element.tagName(),"P");
            element_Body .assert_Not_Null().assert_Are_Equal((element)=>element.tagName(),"BODY");
            element_Head .assert_Not_Null().assert_Are_Equal((element)=>element.tagName(),"HEAD");
            element_B    .assert_Not_Null().assert_Are_Equal((element)=>element.tagName(),"B");
            element_I    .assert_Not_Null().assert_Are_Equal((element)=>element.tagName(),"I");
            element_ABC  .assert_Is_Null();
            element_Null .assert_Is_Null();
            element_Empty.assert_Is_Null();

            element_Body .assert_Is_Equal_To(ie.elements("body").first());
            element_Body .assert_Is_Equal_To(ie.element("Body"));
            element_Body .assert_Is_Equal_To(ie.element("BODY"));
            element_Body .assert_Is_Equal_To(ie.element("BODY   "));
            element_Body .assert_Is_Equal_To(ie.element("   BODY   "));
        }
   
        [Test] public void body()
        {
            var body = ie.body();
            assert_Not_Null(body);
            assert_Are_Equal(body, ie.element("body"));
            assert_Not_Equal(body, ie.element("head"));
            assert_Is_Null  ((null as WatiN_IE).head());
        }

        [Test] public void head()
        {
            var head = ie.head();
            assert_Not_Null(head);
            assert_Are_Equal(head, ie.element("head"));
            assert_Not_Equal(head, ie.element("body"));
            assert_Not_Equal(head, ie.element(null));
            assert_Is_Null  ((null as WatiN_IE).head());
        }
    }
}
