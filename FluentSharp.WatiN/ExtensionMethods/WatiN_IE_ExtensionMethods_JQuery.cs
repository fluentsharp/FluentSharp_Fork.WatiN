using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_JQuery
    {	
        public static WatiN_IE inject_jQuery(this WatiN_IE ie)
        {
            var jQueryFile = "jquery-1.6.2.min.js";
            var jQueryHtml = jQueryFile.local().fileContents();
            if (jQueryHtml.valid())
            {				
                ie.eval(jQueryHtml); 
                "[Injected jQuery]".info();// (jQuery script {0} size: {1}".info(jQueryFile, jQueryHtml.size());
            }
            else
                "[Injecting jQuery] could find local jQuery file: {0}".error(jQueryFile);
            return ie;
        }
			
        public static string jQuery_Append_Body(this string htmlToAppend, WatiN_IE ie)
        {    		
            ie.jQuery_Append_Body(htmlToAppend);
            return htmlToAppend;
        }
    	
        public static WatiN_IE jQuery_Append_Body(this WatiN_IE ie, string htmlToAppend)
        {
            ie.eval("$('body').append('<div>{0}<div>')".format(htmlToAppend));
            return ie;
        }    
    }
}