using System;
using FluentSharp.CoreLib;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Highlight
    {
        public static WatiN_IE enableFlashing(this WatiN_IE watinIe)
        {
            WatiN_IE.FlashingEnabled = true;
            return watinIe;
        }
    	
        public static WatiN_IE disableFlashing(this WatiN_IE watinIe)
        {
            WatiN_IE.FlashingEnabled = false;
            return watinIe;
        }
    	
        public static T flash<T>(this T element)
            where T : Element
        {
            return element.flash(WatiN_IE.FlashingCount);
        }
 
        public static T flash<T>(this T element, int timesToFlash)
            where T : Element
        {
            try
            {				
                if (WatiN_IE.FlashingEnabled)
                {
                    if (WatiN_IE.ScrollOnFlash)
                        element.scrollIntoView();				
                    element.Flash(timesToFlash);
                }
            }
            catch(Exception ex)
            {
                ex.log("in WatiN Element flash");
            }
            return element;
        }
 
        public static T select<T>(this T element)
            where T : Element
        {
            return element.highlight();			
        }
 
        public static T highlight<T>(this T element)
            where T : Element
        {
            try
            {
                element.Highlight(true);
            }
            catch(Exception ex)
            { 
                ex.log("in WatiN Element highlight");
            }
            return element;
 
        }
		
        public static T scrollIntoView<T>(this T element)
            where T : Element
        {
            try
            {
                var htmlElement= element.htmlElement();
                htmlElement.scrollIntoView(null);
            }
            catch(Exception ex)
            {
                ex.log("in WatiN scrollIntoView");
            }
            return element;
        }
    
    }
}