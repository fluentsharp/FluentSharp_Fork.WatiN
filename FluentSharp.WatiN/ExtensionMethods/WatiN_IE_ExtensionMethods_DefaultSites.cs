namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_DefaultSites
    {
        public static WatiN_IE noPage(this WatiN_IE watinIe)
        {
            return watinIe.about_blank();
        }
        public static WatiN_IE about_blank(this WatiN_IE watinIe)
        {
            return watinIe.open("about:blank");
        }
        public static WatiN_IE google(this WatiN_IE watinIe)
        {
            return watinIe.open("http://google.com");
        }	
        public static WatiN_IE owasp(this WatiN_IE watinIe)
        {
            return watinIe.open("http://owasp.org");
        }   	
        public static WatiN_IE bbc(this WatiN_IE watinIe)
        {
            return watinIe.open("http://news.bbc.co.uk");
        }  	    	
    }
}