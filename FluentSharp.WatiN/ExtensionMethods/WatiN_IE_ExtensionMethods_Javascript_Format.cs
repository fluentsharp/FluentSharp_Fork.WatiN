using System.Windows.Forms;
using FluentSharp.WinForms;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Javascript_Format
    {
    
        //using System.Threading
        //var sync = new AutoResetEvent(false);
        //var result = "...";
        //var control = "temp".popupWindow(0,0);
        //O2Thread.staThread(
        ///	()=>{ 
        //var control = new Panel();									
        //control.parentForm().sendToBack();									
        //sync.Set();
        //control.parentForm().close();
        //});												
        //sync.WaitOne(2000);
        //"after waitOne()".debug();
        public static string jsFormat(this Control tempHostControl, string codeToFormat)									
        {
            return tempHostControl.formatJsCode(codeToFormat);			
        }
		
        public static string formatJsCode(this Control tempHostControl, string codeToFormat)
        {			
            var ie = tempHostControl.add_IE().silent(true);
            var result = ie.formatJsCode(codeToFormat);
            O2Thread.mtaThread(()=>ie.close());						
            return result;
        }
		
        public static bool js_FunctionExists(this WatiN_IE ie, string functionName)
        {
            ie.injectJavascriptFunctions();
            return (bool)ie.invokeEval("return (typeof {0} == \"function\");".format(functionName));
        }
		
        public static string formatJsCode(this WatiN_IE ie, string codeToFormat)
        {			
            if (ie.url().neq("about:blank"))
            {
                "opening ABOUT:Blank".info();
                ie.open("about:blank");
            }
            else
                "already in ABOUT:Blank".info();
			
            if (ie.js_FunctionExists("js_beautify").isFalse())
            {				
                var jsBeautify = @"beautify.js".local();
                ie.eval(jsBeautify.fileContents()); 	
                if (ie.js_FunctionExists("js_beautify"))
                    "Injected beautify.js into about:blank".info();
                else
                    "Failed to Inject js_beautify code".error();
            }
            "formating Javascript with size: {0}".info(codeToFormat.size()); 						
            ie.setJsObject(codeToFormat);						
            ie.eval("window.external.setJsObject(js_beautify(_jsObject))"); 
            var result = ie.getJsObject().str().fix_CRLF();					
//			"formated Javascript has size: {0}".info(result.size()); 			
            return result;
        }						
		
        public static WatiN_IE show_Formated_Javascript(this string codeToFormat)
        {
            var ie = "Formated Javascript".popupWindow()
                                          .add_IE()
                                          .show_Formated_Javascript(codeToFormat);
            return ie;
        }
		
        public static WatiN_IE show_Formated_Javascript(this WatiN_IE ie,string codeToFormat)
        {
            return ie.show_Formated_Javascript(null, codeToFormat);
        }
		
        public static WatiN_IE show_Formated_Javascript(this WatiN_IE ie, WatiN_IE temp_ie, string codeToFormat)
        {		
            var prettifyHtml = @"prettify.htm".local();
            if (prettifyHtml.fileExists().isFalse())
                return ie;			
			
            if (ie.url().isNull() || ie.url().contains("prettify.htm").isFalse())
                ie.open(prettifyHtml);  						
            var formatedJsCode = (temp_ie.isNull()) 
                                     ? ie.HostControl.formatJsCode(codeToFormat)
                                     : temp_ie.formatJsCode(codeToFormat);			
						
            var codeDiv = ie.div("codeDiv");
            codeDiv.innerHtml("<pre id=\"code\" class=\"prettyprint\">{0}</pre>".format(formatedJsCode));			
            ie.invokeScript("prettyPrint"); 			
            return ie;
        }
    }
}