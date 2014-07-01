using System;
using FluentSharp.CoreLib;
using FluentSharp.WinForms;
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
    	
        public static T injectHtml_afterEnd<T>(this T element, string htmlToInject) where T : Element
        {
            return element.injectHtml("afterEnd", htmlToInject);
        }
    	
        public static T injectHtml<T>(this T element, string location, string htmlToInject) where T : Element
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
        /// <summary>
        /// Adds an Link to the current element.
        /// 
        /// This uses <code>add_Link<T>(this T element, string id, string href, string innerText)  </code> 
        /// with:
        ///     id   = "Link_Id".add_5_RandomLetters()
        ///     href = "#"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="innerText"></param>
        /// <returns></returns>
        public static T add_Link<T>(this T element,string innerText) where T : Element
        {
            var id = "Link_Id".add_5_RandomLetters();
            var href ="#";
            return element.add_Link(id, href,innerText);
        }
        /// <summary>
        /// Adds an Link to the current element. 
        /// 
        /// The new link is added at the end of the current element, using <code>injectHtml_beforeEnd</code> 
        /// using the provided id, href and innerText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="id"></param>
        /// <param name="href"></param>        
        /// <param name="innerText"></param>
        /// <returns></returns>
        public static T add_Link<T>(this T element, string id, string href, string innerText) where T : Element
        {
            var html = "<a href='{0}' id='{1}'>{2}</a>".format(href.htmlAttributeEncode(), 
                                                               id.htmlAttributeEncode(), 
                                                               innerText.htmlEncode());
            element.injectHtml_beforeEnd(html);
            return element;
        }

        public static T add_H1<T>(this T element, string innerText) where T : Element
        {
            var id = "H1_Id".add_5_RandomLetters();
            return element.add_H1(id, innerText);
        }
        public static T add_H1<T>(this T element, string id, string innerText) where T : Element
        {
            var html = "<h1 id='{0}'>{1}</a>".format(id.attributeEncode(),innerText.htmlEncode());                                                                   
            element.injectHtml_beforeEnd(html);
            return element;
        }
    }
}