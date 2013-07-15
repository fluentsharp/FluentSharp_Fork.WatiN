using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Forms
    {
 
        public static List<WatiN.Core.Form> forms(this WatiN_IE watinIe)
        {
            return (from form in watinIe.IE.Forms
                    select form).toList();
        }
 
 
    }
}