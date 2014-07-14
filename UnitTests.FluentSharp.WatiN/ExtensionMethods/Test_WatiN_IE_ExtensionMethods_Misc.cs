using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.Watin;
using NUnit.Framework;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_WatiN_IE_ExtensionMethods_Misc : NUnitTests
    {
        [Test] public void html()
        {
            var testMessage = "This is a message".add_5_RandomLetters();
            var ie          = "IE".add_IE_Hidden_PopupWindow()
                                  .showMessage(testMessage);
            var html        = ie.html();
            html.assert_Not_Null()
                .assert_Contains(testMessage)
                .assert_Contains("BODY", "HTML");

            assert_Is_Empty((null as WatiN_IE).html());
        }
    }
}
