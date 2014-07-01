using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using mshtml;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Elements
    {
 
        public static List<Element> elements(this WatiN_IE watinIe, string tagName)
        {
            return watinIe.elements().elements(tagName);            
        }
 
        /// <summary>
        /// Returns a list of all Html Elements in the page (note that there could be quite a lot of them).
        /// 
        /// Note: this is a recursive search
        /// </summary>
        /// <param name="watinIe"></param>
        /// <returns>List of WatiN.Core.Element</returns>
        public static List<Element> elements(this WatiN_IE watinIe)
        {
            return (from element in watinIe.IE.Elements
                    select element).toList();
        } 		 		
 		
        /// <summary>
        /// returns the elements that match the provided tag
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static List<Element> elements(this List<Element> elements, string tagName)
        {
            tagName = tagName.trim().lower();       //normalize the data
            if (tagName.notValid())
                return new List<Element>();
            return (from element in elements
                    where element.TagName.lower().trim() == tagName
                    select element).toList();
        }
        public static List<string> tagNames(this List<Element> elements)
        {    		
            return (from element in elements
                    select element.TagName).Distinct().toList();
        }
 
        public static Dictionary<string, List<Element>> indexedByTagName(this List<Element> elements)
        {
            return elements.indexedByTagName(true);
        }
 		
        public static Dictionary<string, List<Element>> indexedByTagName(this List<Element> elements, bool includeEmptyFields)
        {
            var result = new Dictionary<string,List<Element>>();
            foreach(var element in elements)
                if (includeEmptyFields || element.str().valid())
                    result.add(element.TagName, element);
            return result;
        }     	
  
        public static string tagName(this Element element)
        {
            if (element.notNull())
                return element.TagName.trim();
            return null;
        }
    	
        public static string id(this Element element)
        {
            return (element != null)
                       ? element.Id.trim()
                       : "";
        }
    	
        public static string className(this Element element)
        {
            return (element != null)
                       ? element.ClassName.trim()
                       : "";
        }
 
        public static string text(this Element element)
        {
            return (element != null)
                       ? element.Text.trim()
                       : "";
        }
 
        public static string title(this Element element)
        {
            return (element != null)
                       ? element.Title.trim()
                       : "";
        }
 
        public static string innerText(this Element element)
        {
            return (element != null)
                       ? element.Text.trim()
                       : "";
        }
        public static string innerHtml(this Element element)
        {
            return (element != null)
                       ? element.InnerHtml
                       : "";
        }
 
        public static string outerHtml(this Element element)
        {
            return (element != null)
                       ? element.OuterHtml
                       : "";
        }
 
        public static string html(this Element element)
        {
            return element.outerHtml();
        }
    	
        public static IHTMLElement htmlElement(this Element element)
        {
            if (element.NativeElement is IEElement)
                return (element.NativeElement as IEElement).AsHtmlElement;
            //return (IHTMLElement) element.HTMLElement;            
            return null;
        }    	   	
 
        public static void remove(this Element element)
        {
            element.outerHtml("");	
        }
 
        public static void remove(this List<Element> elements)
        {
            foreach(var element in elements)
                element.outerHtml("");	
        }
 
        public static List<T> outerHtml<T>(this List<T> elements, string outerHtml)
            where T : Element
        {
            foreach(var element in elements)
                element.outerHtml(outerHtml);	
            return elements;
        }
 
        public static List<T> innerHtml<T>(this List<T> elements, string innerHtml)
            where T : Element
        {
            foreach(var element in elements)
                element.innerHtml(innerHtml);	
            return elements;
        }
 
        public static T outerHtml<T>(this T element, string outerHtml)
            where T : Element
        {
            if (element!= null)
            {
                var htmlElement = element.htmlElement();
                if (htmlElement != null)    			
                    htmlElement.outerHTML = outerHtml;    				   			
            }
            return element;
        }
 
        public static T innerHtml<T>(this T element, string innerHtml)
            where T : Element
        {
            if (element!= null)
            {
                var htmlElement = element.htmlElement();
                if (htmlElement != null)    			
                    htmlElement.innerHTML= innerHtml;    				   			
            }
            return element;
        }	
 
        public static Element @class(this List<Element> elements, string className)
        {
            foreach(var element in elements)
                if (element.ClassName == className)
                    return element;
            return null;
        }
        public static List<Element> @classes(this List<Element> elements, string className)
        {
            return (from element in elements
                    where (element.ClassName == className)
                    select element).toList();
        }
 
        public static List<string> classes(this List<Element> elements)
        {
            return (from element in elements
                    where (element.ClassName != null)
                    select element.ClassName).toList();
        }
 
        public static List<Element> elements(this IElementContainer elementsContainer)
        {
            return (from element in elementsContainer.Elements
                    select element).toList();
        }
    	
        public static List<Element> elements(this IElementContainer elementsContainer, string tagName)
        {
            return elementsContainer.elements().elements(tagName);
        }
 
        public static List<T> elements<T>(this WatiN_IE watinIe)
            where T : Element
        {
            return (from element in watinIe.elements()
                    where element is T
                    select (T)element).toList();
        }
 
        public static List<string> ids(this List<Element> elements)
        {
            return (from element in elements
                    where (element.Id != null)
                    select element.Id).toList();
        }
 
        public static Dictionary<string,Element> byId(this List<Element> elements)
        {		
            var result = new Dictionary<string,Element>();
            foreach(var element in elements)
                if (element.Id != null)
                    result.add(element.Id, element);
            return result;
        }
 
 
        public static T element<T>(this WatiN_IE watinIe, string id)
            where T : Element
        {
            return watinIe.elements().with_Id<T>(id);
        }
 
        /// <summary>
        /// Searches all elements for the an element that matches the provided value.
        /// 
        /// The value returned is the first match, and the search order is:
        ///     - Html Tag name
        ///     - id attribute 
        ///     - innerHtml       
        /// </summary>
        /// <param name="watinIe"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Element element(this WatiN_IE watinIe, string value)
        {            
            if (watinIe.isNull() || value.notValid())
                return null;
            var element_by_Tag = watinIe.elements().with_TagName(value);
            if (element_by_Tag.notNull())
                return element_by_Tag;

            var element_by_Id = watinIe.elements().with_Id(value);
            if (element_by_Id.notNull())
                return element_by_Id;

            var element_by_InnerText = watinIe.elements().with_InnerText(value);
            if (element_by_InnerText.notNull())
                return element_by_InnerText;
            return  null;
        }
 
        public static Element with_TagName(this List<Element> elements, string tagName)
        {
            tagName = tagName.lower().trim();
            foreach(var element in elements)
                if (element.tagName().lower() == tagName)
                    return element;
            return null;
        }
        public static T with_Id<T>(this List<Element> elements, string id)
            where T : Element
        {
            var element = elements.with_Id(id);
            if (element is T)
                return (T)element;
            return null;			
        }
        public static Element with_Id(this List<Element> elements, string idValue)
        {
            idValue = idValue.lower().trim();
            foreach(var element in elements)
                if (element.id().lower() == idValue)
                    return element;
            return null;
        }
        public static Element with_InnerText(this List<Element> elements, string innerText)
        {
            innerText = innerText.lower().trim();
            foreach(var element in elements)
                if (element.innerText().lower() == innerText)
                    return element;
            return null;
        }
 
        public static List<Element> texts(this List<Element> elements, string text)
        {
            return elements.texts(text,false);
        }
 
        public static List<Element> texts(this List<Element> elements, string text, bool useRegEx)
        {
            if (useRegEx)
                return (from element in elements
                        where element.text().regEx(text)
                        select element).toList();
            else
                return (from element in elements
                        where element.text() == text
                        select element).toList();
        }
 
        public static Element text(this List<Element> elements, string text)
        {
            foreach(var element in elements)
                if (element.Id != null && element.text() == text)
                    return element;
            return null;
        }
 
        public static List<string> strs(this List<Element> elements)
        {
            return (from element in elements
                    where element.str().valid()
                    select element.str()).toList();    
        }
    	
        public static Element str(this List<Element> elements, string text)
        {
            foreach(var element in elements)
                if (element.str() == text)
                    return element;
            return null;
        }
 
        public static List<IHTMLDOMAttribute> attributesRaw(this Element element)
        { 
            var domAttributes = new List<IHTMLDOMAttribute>();  
            if (element.notNull() && element.htmlElement() is IHTMLDOMNode)
            {						
                var domNode = (IHTMLDOMNode)element.htmlElement();
                foreach(var attribute in (IHTMLAttributeCollection)domNode.attributes)
                {		
                    var domAttribute = attribute as IHTMLDOMAttribute; 
                    if (domAttribute.notNull() && domAttribute.specified)																	
                        domAttributes.Add(domAttribute);
                }						
            }
            return domAttributes; 
        }

        public static Dictionary<string,string> attributes(this Element element)
        {
            var attributeValues = new Dictionary<string,string>();
            foreach(IHTMLDOMAttribute attribute in attributesRaw(element)) 
                attributeValues.add(attribute.nodeName.str(), attribute.nodeValue.str());
            return attributeValues;
        }
		
        public static string attribute(this Element element, string attributeName)
        {
            if (element.notNull() && attributeName.notNull())
            { 
                var attributes = element.attributes();
                if (attributes.hasKey(attributeName))
                    return attributes[attributeName];
            }
            return "";
        }
        public static Element body(this WatiN_IE  watinIe)
        {
            return watinIe.element("body");
        }
        public static Element head(this WatiN_IE  watinIe)
        {
            return watinIe.element("head");
        }
    }
}