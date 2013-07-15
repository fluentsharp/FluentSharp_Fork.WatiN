using System;
using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Link
    { 	
 
        public static Link link(this WatiN_IE watinIe, string name)
        {
            foreach(var link in watinIe.links())
                if (link.id() == name || link.text() == name)
                    return link;
            "in WatiN_IE could not find Link with name:{0}".error(name ?? "[null value]");
            return null;    				
        }
 
        public static List<Link> links(this WatiN_IE watinIe)
        {
            if (watinIe.notNull() && watinIe.IE.notNull())
                return (from link in watinIe.IE.Links
                        select link).toList();
            return new List<Link>();
        }
 
        public static string url(this Link link)
        {
            return (link != null)
                       ? link.Url
                       : "";
        }     	
    	
        public static Link click(this Link link)
        {
            return link.click(0);    		
        }    	   	
 
        public static Link click(this Link link, int miliseconds)
        {
            if (link != null)
            {
                link.Click();
                miliseconds.sleep();    			
            }
            return link;
        }
 
 
        public static List<string> texts(this List<Link> links)
        {
            return (from link in links 
                    select link.text()).toList();
        }
 
        public static List<string> urls(this List<Link> links)
        {
            return (from link in links 
                    select link.url()).toList();
        } 
    	
        public static List<Uri> uris(this List<Link> links)
        {
            return links.urls().uris();
        }
    	
        public static List<string> ids(this List<Link> links)
        {
            return (from link in links
                    where (link.Id != null)
                    select link.Id).toList();
        }
 
        public static bool hasLink(this WatiN_IE watinIe, string nameOrId)
        {			
            foreach(var link in watinIe.links())
                if (link.id() == nameOrId || link.text() == nameOrId)
                    return true;
            return false;
            //return watinIe.links().ids().Contains(id);
        }
		
        public static Link waitForLink(this WatiN_IE watinIe, string nameOrId)
        {
            return watinIe.waitForLink(nameOrId, 500, 10);
        }
		
        public static Link waitForLink(this WatiN_IE watinIe, string nameOrId, int sleepMiliseconds, int maxSleepTimes)
        {
		
            var count = 0;
            while(watinIe.hasLink(nameOrId).isFalse())
            {
                if (count++ >=maxSleepTimes)
                    break;
                watinIe.sleep(500, false);
            }
            return watinIe.link(nameOrId);
        }

    }
}