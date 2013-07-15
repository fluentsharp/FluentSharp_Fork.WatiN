using System;
using System.Windows.Forms;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using SHDocVw;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Events
    {
        public static WatiN_IE onNavigate(this WatiN_IE ie, MethodInvoker callback, string expectedPage)
        {
            (ie.IE.InternetExplorer as DWebBrowserEvents2_Event).NavigateComplete2 += 
                (object pDisp, ref object url)=>
                    {
                        if (url.str() == expectedPage) 						
                            O2Thread.mtaThread(()=>callback());
                    };
            return ie;
        }
   
        public static WatiN_IE onNavigate(this WatiN_IE ie, MethodInvoker callback)
        {
            (ie.IE.InternetExplorer as DWebBrowserEvents2_Event).NavigateComplete2 += 
                (object pDisp, ref object url)=> O2Thread.mtaThread(()=>callback());
            return ie;
        }
    	
        public static WatiN_IE onNavigate(this WatiN_IE ie, Action<string> callback)
        {
            (ie.IE.InternetExplorer as DWebBrowserEvents2_Event).NavigateComplete2 += 
                (object pDisp, ref object url)=> 
                    {
                        var pageUrl = url.str(); // need to pin down this value
                        O2Thread.mtaThread(()=>callback(pageUrl));
                    };
            return ie;
        }
    	
        public static WatiN_IE onNavigate(this WatiN_IE ie, Action<IWebBrowser2, string> callback)
        {
            (ie.IE.InternetExplorer as DWebBrowserEvents2_Event).NavigateComplete2 += 
                (object pDisp, ref object url)=>
                    { 						
                        if (pDisp is IWebBrowser2 && url is string)
                        {	
                            var pageUrl = url.str(); // need to pin down this value
                            O2Thread.mtaThread(
                                ()=> callback(pDisp as IWebBrowser2, pageUrl));	
                        }
                    };
            return ie;
        }
    	    
        public static WatiN_IE beforeNavigate(this WatiN_IE ie, Func<string,bool> callback)
        {
            (ie.IE.InternetExplorer as DWebBrowserEvents2_Event).BeforeNavigate2 += 
                //(object pDisp, ref object url)
                (object pDisp,  ref object URL, ref object Flags,  ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)=>
                    { 						
                        //"in beforeNavigate of url:{0} -> h: {1}".info(URL, Headers);
                        Cancel = callback(URL.str()); 						
                    };
            return ie;
        }
    }
}