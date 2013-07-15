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
            return (from element in watinIe.IE.Elements
                    where element.TagName == tagName
                    select element).toList();
        }
 
        public static List<Element> elements(this WatiN_IE watinIe)
        {
            return (from element in watinIe.IE.Elements
                    select element).toList();
        } 		 		
 		
        public static List<Element> elements(this List<Element> elements, string tagName)
        {
            return (from element in elements
                    where element.TagName == tagName
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
            return element.TagName.trim();
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
            return watinIe.elements().id<T>(id);
        }
 
        public static Element element(this WatiN_IE watinIe, string id)
        {
            return watinIe.elements().id(id);
        }
 
        public static Element id(this List<Element> elements, string id)
        {
            foreach(var element in elements)
                if (element.Id != null && element.Id == id)
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
 
        public static T id<T>(this List<Element> elements, string id)
            where T : Element
        {
            var element = elements.id(id);
            if (element is T)
                return (T)element;
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
            var attributes = element.attributes();
            if (attributes.hasKey(attributeName))
                return attributes[attributeName];
            return "";
        }
    }
}