namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Cookies
    {
        //todo: fix lack of IE.HtmlDocument object in 2.1 version of WatiN
        /*
 		public static string cookiesRaw(this WatiN_IE ie)
 		{
 			try
 			{
 				if (ie.IE.HtmlDocument.notNull())
 					return ie.IE.HtmlDocument.cookie;
 				return "";
 			}
 			catch(Exception ex)
 			{
 				ex.log("in WatiN_IE.cookiesRaw");
 				return "";
 			}
 		}
 		
 		public static List<string> cookies(this WatiN_IE ie)
 		{
 			if (ie.cookiesRaw().valid())
 				return (from cookie in ie.cookiesRaw().split(";")
 						select cookie.trim()).toList();
 						
 			return new List<string>(); 			
 		}
 		
 		public static Dictionary<string,string> cookies_asDictionary(this WatiN_IE ie)
 		{ 			
 			var cookies_asDictionary = new Dictionary<string,string>();
 			foreach(var cookie in ie.cookies())
 			{
 				var splittedCookie = cookie.split("=");
 				if (splittedCookie.size().neq(2))
 					"[Watin_IE] in cookies_asDictionary, there was an error splitting cookie: {0}".error(cookie);
 				else
 					cookies_asDictionary.add(splittedCookie[0], splittedCookie[1]); 					
 			}
 			return cookies_asDictionary;
 		}
 		
 		public static string cookiesRaw(this Dictionary<string,string> cookies_asDictionary)
 		{
 			var cookieRaw = "";
 			foreach(var item in cookies_asDictionary)
 				cookieRaw+= "{0}={1}&".format(item.Key, item.Value);
 			return cookieRaw;
 		}
 		
 		//Need to find a better way to do this  (check what happens if the cookie name has a space between name & =
 		public static string cookie(this WatiN_IE ie, string cookieName)
 		{
 			var stringToFind = cookieName + "=";
 			foreach(var cookie in ie.cookies()) 			
 				if (cookie.starts(stringToFind))
 					return cookie.Substring(stringToFind.size());
			return "";
 		}
 		
 		public static WatiN_IE cookies_Clear(this WatiN_IE ie)
 		{
 			ie.IE.ClearCache();
 			ie.IE.ClearCookies();
 			return ie;
 		}
 		
 		public static WatiN_IE cookies_Clear(this WatiN_IE ie, string url)
 		{
 			ie.IE.ClearCache();
 			ie.IE.ClearCookies(url);
 			return ie;
 		}*/
    }
}