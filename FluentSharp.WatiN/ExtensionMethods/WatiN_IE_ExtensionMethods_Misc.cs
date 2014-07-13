using System;
using System.Text;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.WinForms;
using SHDocVw;
using WatiN.Core;
using mshtml;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Misc
    {
        
        public static WatiN_IE      showMessage(this WatiN_IE ie, string message, int sleepValue)
        {
            ie.showMessage(message);
            ie.wait(sleepValue);
            return ie;
        }
    	
        public static WatiN_IE      showMessage(this WatiN_IE ie, string message)
        {
            return ie.showMessage(message,false);
        }
    	
        public static WatiN_IE      showMessage(this WatiN_IE ie, string message, bool runOnSeparateThread)
        {    		
            message = message.Replace("".line(), "<br/>");
            var messageTemplate = "<html><body><div style = \"position:absolute; top:50%; width:100%; text-align: center;font-size:20pt; font-family:Arial\">{0}</div></body></html>";
			
            if (runOnSeparateThread)    			
                O2Thread.mtaThread(()=>{ie.set_Html(messageTemplate.format(message));});
            else
                ie.set_Html(messageTemplate.format(message));
            return ie;
        }
        // uri & url
 
        public static string        outerHtml(this HTMLHtmlElementClass htmlElementClass)
        {
            if (htmlElementClass.notNull())
                return htmlElementClass.outerHTML;
            return "";
        }  		
        public static bool          isDocumentAvailable(this WatiN_IE ie)
        {
            return ie.htmlDocument().notNull();
        }
       
        /// <summary>
        /// Returns the rendered HTML code of the current page
        /// 
        /// This is done using <code>ie.element("HTML").outherHTML()</code> 
        /// because Watin's HTML value (via <code>ie.IE.HTML</code>) was actually the
        /// html of the BODY element
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        public static string        html(this WatiN_IE ie)
        {            
            return ie.element("HTML").outerHtml();
            
            //return ie.documentElement().html();
            /*
			try
    		{    			
	    		if (ie.IE.InternetExplorer.notNull() && ie.IE.InternetExplorer is IWebBrowser2)
	    		{
	    			var webBrowser = (IWebBrowser2)ie.IE.InternetExplorer;
	    			if (webBrowser.Document.notNull() && webBrowser.Document is HTMLDocumentClass)
	    			{
	    				var htmlDocument = (HTMLDocumentClass)webBrowser.Document;
	    				if (htmlDocument.documentElement.notNull())
	    					return htmlDocument.documentElement.outerHTML;
	    			}    			
	    		}    		
    		}
    		catch(Exception ex)
    		{
    			ex.log("in WatiN_IE html()");
    		}
    		return "";*/
        }    	
        public static WatiN_IE      html(this WatiN_IE ie, string newHtml)
        {
            return ie.set_Html(newHtml);
        }
    	
        public static WatiN_IE      set_Html(this WatiN_IE ie, string newHtml)
        {
            ie.open(newHtml.saveWithExtension(".html"));
            return ie;
        }
    	
        public static Uri           uri(this WatiN_IE watinIe)
        {
            try
            {    		
	            if (watinIe.isDocumentAvailable())
                    return watinIe.IE.Uri;
            }
            catch(Exception ex)
            {
                ex.log("[WatiN_IE][uri]");                
            }
            return null;
        } 
        public static WatiN_IE      if_NoPageLoaded(this WatiN_IE watinIe, Action callback)
        {
            if (watinIe.noPageLoaded())
                callback();
            return watinIe;
        } 		
        public static bool          noPageLoaded(this WatiN_IE watinIe)
        {
            return watinIe.url().isUri().isFalse() || 
                   watinIe.url() == "about:blank";
        } 			
        public static string        url(this WatiN_IE watinIe)
        {	            
            var uri = watinIe.uri();
            if(uri.notNull())
                return uri.str();
            return null;
            
        }    	
        public static bool          url(this WatiN_IE watinIe, string url)
        {
            return  (watinIe.url() == url);
        }
        public static string        title(this WatiN_IE watinIe)
        {
            return watinIe.IE.Title;
        } 
        public static bool          title(this WatiN_IE watinIe, string title)
        {
            return watinIe.IE.Title == title;
        } 
        public static string        processId(this WatiN_IE watinIe)
        {
            return watinIe.IE.ProcessID.str();
        }
        // region close 
 
        public static WatiN_IE      close(this WatiN_IE watinIe)
        {
            "closing WatIN InternetExplorer Process".info();
            watinIe.close();
            //watinIe.Close();
            return watinIe;
        }
 
        public static WatiN_IE      closeInNSeconds(this WatiN_IE watinIe, int seconds)
        {
            if (seconds > 60)
            {
                "in WatiN_IE closeInNSeconds, provided value bigger than 60 secs, so changing the delay (before close) to 60".error();
                seconds = 60;
            }
            "IE instance will be closed in {0} seconds".info(seconds);
            O2Thread.mtaThread( 
                ()=>{ 
                        watinIe.wait(5000);  
                        watinIe.close();   
                }); 
            return watinIe;
        }
      	

        public static WatiN_IE      silent(this WatiN_IE watinIe, bool value)
        {
            if (watinIe.IE.InternetExplorer != null)
                if (watinIe.IE.InternetExplorer is IWebBrowser2)
                    (watinIe.IE.InternetExplorer as IWebBrowser2).Silent = value;
            return watinIe; 			
        }				
		
        public static WatiN_IE      open(this WatiN_IE watinIe, string url)
        {
            return watinIe.open(url,0);
        }
 
        public static WatiN_IE      open(this WatiN_IE watinIe, string url, int miliseconds)
        {
            if (watinIe.isNull())
                return watinIe;
            "[WatIN] open: {0}".info(url);
            watinIe.execute(
                ()=>{
                        watinIe.IE.GoTo(url);
                        watinIe.wait(miliseconds);
                });
            return watinIe;
        }
    	
        public static WatiN_IE      open_ASync(this WatiN_IE watinIe, string url)
        {
            O2Thread.mtaThread(()=> watinIe.open(url));
            return watinIe;
        }
    	
        public static WatiN_IE      open_usingPOST(this WatiN_IE watinIe, string postUrl,string postData)
        {
            return watinIe.open_usingPOST(postUrl, "application/x-www-form-urlencoded", postData);
        }
    	
        public static WatiN_IE      open_usingPOST(this WatiN_IE watinIe, string postUrl, string contentType, string postData)
        {    		
            try
            {
                "[WatIN] open using POST: {0} ({1} bytes)".info(postUrl, postData.size());
                var browser = (watinIe.IE.InternetExplorer as IWebBrowser2); 
				if (browser.notNull())
                {
                    object postDataByte = Encoding.UTF8.GetBytes(postData);
                    object additionalHeaders = "Content-Type: " + contentType.line();
                    object nullValue = null;
                    browser.Navigate(postUrl, ref nullValue, ref nullValue, ref postDataByte, ref additionalHeaders);
                    watinIe.IE.WaitForComplete();
                }
                else
                    "[WatiN_IE open_usingPOST] could not get a reference to the browser object".error();
            }
            catch(Exception ex)
            {
                ex.log("in WatiN_IE open_usingPOST");
            }
            return watinIe;
        }
    	
        public static WatiN_IE      openWithBasicAuthentication(this WatiN_IE watinIe, string url, string username, string password)
        {
            var encodedHeader = "Authorization: Basic " + "{0}:{1}".format(username, password).base64Encode();
            return watinIe.openWithExtraHeader(url, encodedHeader);
        }        
        public static IWebBrowser2  iWebBrowser(this WatiN_IE watinIe)
        {            
             if (watinIe.IE.notNull() && watinIe.IE.InternetExplorer.notNull())
             {
                 var internetExplorer = watinIe.IE.InternetExplorer;
                 if (internetExplorer is IWebBrowser2)
                    return (IWebBrowser2)internetExplorer;
             }
             return null;
        }
        public static WatiN_IE      openWithExtraHeader(this WatiN_IE watinIe, string url, string extraHeader)
        {
            try
            {
                "[WatIN] open with extra Header: {0} ({1} bytes)".info(url, extraHeader.size());
                object headerObject = extraHeader.line();
                object flags = null;                
                var iWebBrowser = watinIe.iWebBrowser();
                if (iWebBrowser.notNull())
                {
                    iWebBrowser.Navigate(url, 
                                                                       ref flags, 
                                                                       ref flags, 
                                                                       ref flags, 
                                                                       ref headerObject);
                    watinIe.IE.WaitForComplete();
                }
            }
            catch(Exception ex)
            {
                ex.log("in WatiN_IE openWithExtraHeader(...)");
            }

            return watinIe;
        }
    	
    	public static WatiN_IE      refresh(this WatiN_IE ie)
        {
            if (ie.url().valid())
                ie.open(ie.url());            
            return ie;
        }
        public static WatiN_IE      setTimeout(this WatiN_IE watinIe, int timeout)
        {
            Settings.WaitForCompleteTimeOut = timeout;
            return watinIe;
        }
    	    	
        public static WatiN_IE      resetTimeout(this WatiN_IE watinIe)
        {
            Settings.WaitForCompleteTimeOut = 30;
            return watinIe;
        }
 
        public static WatiN_IE      wait(this WatiN_IE watinIe)
        {
            return watinIe.wait(1000);
        }
 
        public static WatiN_IE      wait(this WatiN_IE watinIe, int miliseconds)
        {
            if (WatiN_IE.WaitingEnabled && miliseconds > 0)
                watinIe.sleep(miliseconds);
            return watinIe;
        }
 
        public static WatiN_IE      waitNSeconds(this WatiN_IE watinIe, int seconds)
        {
            if (seconds > 0)
                watinIe.sleep(seconds* 1000);
            return watinIe;
        }
 
 
        public static T wait<T>(this T element, int miliseconds)
            where T : Element
        {
            if (miliseconds > 0)
                element.sleep(miliseconds);
            return element;
        }
 
        public static WatiN_IE waitForComplete(this WatiN_IE watinIe)
        {
            watinIe.IE.WaitForComplete(); 
            return watinIe;
        }
        public static WatiN_IE parentForm_WaitForClose(this WatiN_IE watinIe)
        {
            watinIe.parentForm().waitForClose();
            return watinIe;
        }        
   
    public static HTMLDocumentClass     htmlDocument (this WatiN_IE ie)
        {
             try
            {
                if (ie.IE.InternetExplorer.notNull() && ie.IE.InternetExplorer is IWebBrowser2)
                {
                    var webBrowser = (IWebBrowser2)ie.IE.InternetExplorer;
                    if (webBrowser.Document.notNull() && webBrowser.Document is HTMLDocumentClass)
                    {
                        return (HTMLDocumentClass)webBrowser.Document;                        
                    }    			
                }    		
            }
            catch(Exception ex)
            {
                ex.log("in WatiN_IE htmlDocument()");
            }
            return null;
        }
        public static DispHTMLHtmlElement   htmlDocumentElement (this WatiN_IE ie)
        {
            try
            {                
                var htmlDocument = ie.htmlDocument();
                if (htmlDocument.notNull())
                {
                    var htmlDocumentElement  = htmlDocument.documentElement;
                    var comTypeName = htmlDocumentElement.comObject_TypeName();
                    if (htmlDocumentElement.notNull())
                    {
                        /* in an previous version it was HTMLHtmlElementClass
                         * 
                        if (htmlDocumentElement is HTMLHtmlElementClass)
                        return (HTMLHtmlElementClass)htmlDocumentElement;                                            
                         */
                        if (htmlDocumentElement is DispHTMLHtmlElement)
                            return (DispHTMLHtmlElement)htmlDocumentElement;                    
                            
                    }                    
                }    		
            }
            catch(Exception ex)
            {
                ex.log("in WatiN_IE htmlDocumentElement()");
            }
            return null;
        } 		
    }
}