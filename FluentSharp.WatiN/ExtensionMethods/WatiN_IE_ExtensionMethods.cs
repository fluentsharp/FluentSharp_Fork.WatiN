// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.WinForms.Utils;

//O2File:DotNet_Viewstate.cs
 
namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods
    {        	
    	//global conts
    	public static int WAITFORJSVARIABLE_MAXSLEEPTIMES = 10;
    	
    	//WatIN ExtensionMethods
 
    	public static WatiN_IE ie(this string url)
    	{
    		int top = 0;
    		int left = 0;    		
    		return url.ie(top, left);
    	}
 
    	public static WatiN_IE ie(this string url, int top, int left)
    	{
    		int width = 800;
    		int height = 600;
    		return url.ie(top, left, width, height);
    	}
 
    	public static WatiN_IE ie(this string url, int top, int left, int width, int height)
    	{    		
    		var ie = new WatiN_IE();
    		ie.createIEObject(url, top, left, width, height);
			return ie;						
    	}
 
    	/*public static WatiN_IE ie(this O2.External.IE.Wrapper.O2BrowserIE o2BrowserIE)
 		{ 			
			return (o2BrowserIE as System.Windows.Forms.WebBrowser).ie();
		}*/
 
		public static WatiN_IE ie(this System.Windows.Forms.WebBrowser webBrowser)
 		{ 			
			return new WatiN_IE(webBrowser);						
		}
 	}
}
