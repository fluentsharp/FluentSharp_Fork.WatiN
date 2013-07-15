using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Divs
    { 
 
        public static Div div(this WatiN_IE watinIe, string idOrTitle)
        {
            foreach(var div in watinIe.divs())
                if ((div.Id != null && div.Id == idOrTitle) || 
                    (div.Title != null && div.Title ==idOrTitle))
                    return div;
            return null;
        }
        public static List<Div> divs(this WatiN_IE watinIe)
        {
            return (from div in watinIe.IE.Divs
                    select div).toList();
        }
 
        public static List<string> ids(this List<Div> divs)
        {
 
            return (from div in divs 
                    where div.Id != null
                    select div.Id).toList();
        }
    }
}