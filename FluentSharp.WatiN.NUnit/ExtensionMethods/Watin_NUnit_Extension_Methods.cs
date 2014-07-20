using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.Watin;

namespace FluentSharp.WatiN.NUnit
{
    public static class Watin_NUnit_Extension_Methods
    {
        public static WatiN_IE assert_Has_Valid_Url(this WatiN_IE watinIe, string message = NUnit_WatiN_Messages.ASSERT_HAS_VALID_URL)
        {
            if (watinIe.notNull())
                watinIe.url().isUri().assert_True(message);
            return watinIe;
        }
        public static WatiN_IE assert_Has_Link(this WatiN_IE watinIe, string linkId, string message = NUnit_WatiN_Messages.ASSERT_HAS_LINK)
        {
            watinIe.assert_Has_Valid_Url()
                   .waitForLink(linkId)
                   .assert_Not_Null(message.format(watinIe.url(), linkId));
            return watinIe;
        }
    }
}
