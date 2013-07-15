using System;
using FluentSharp.CoreLib;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Inject_Html
    {
        public static Element injectHtml_beforeBegin(this Element element, string htmlToInject)
        {
            return element.injectHtml("beforeBegin", htmlToInject);
        }
    	
        public static Element injectHtml_afterBegin(this Element element, string htmlToInject)
        {
            return element.injectHtml("afterBegin", htmlToInject);
        }
    	
        public static Element injectHtml_beforeEnd(this Element element, string htmlToInject)
        {
            return element.injectHtml("beforeEnd", htmlToInject);
        }
    	
        public static Element injectHtml_afterEnd(this Element element, string htmlToInject)
        {
            return element.injectHtml("afterEnd", htmlToInject);
        }
    	
        public static Element injectHtml(this Element element, string location, string htmlToInject)
        {
            try
            {
                element.htmlElement().insertAdjacentHTML(location,htmlToInject);
            }
            catch(Exception ex)
            {
                ex.log("in WatiN Element injectHtml -> location:{0} payload:{1} ".format(location,htmlToInject));
            }
            return element;
        }
    
    }
}