using System;
using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Image
    {
 
        public static WatiN.Core.Image image(this WatiN_IE watinIe, string name)
        {
            foreach(var image in watinIe.images())
                if (image.id() == name)//|| link.text() == name)
                    return image;
            "in WatiN_IE could not find Image with name:{0}".error(name ?? "[null value]");
            return null;    				
        }
 
        public static List<WatiN.Core.Image> images(this WatiN_IE watinIe)
        {
            return (from image in watinIe.IE.Images
                    select image).toList();
        }
 
        public static Uri uri(this WatiN.Core.Image image)
        {
            return (image != null)
                       ? image.Uri
                       : null;
        }
 
        public static string url(this WatiN.Core.Image image)
        {
            return (image != null)
                       ? image.Uri.str()
                       : "";
        }
 
        public static string src(this WatiN.Core.Image image)
        {
            return (image != null)
                       ? image.Src
                       : "";
        }
    	
        public static List<Uri> uris(this List<WatiN.Core.Image> images)
        {
            return (from image in images
                    select image.Uri).toList();
        }
    	    	
        public static List<string> urls(this List<WatiN.Core.Image> images)
        {
            return (from image in images
                    select image.Uri.str()).toList();
        }
    	
        public static List<string> srcs(this List<WatiN.Core.Image> images)
        {
            return (from image in images
                    select image.Src).toList();
        }
 
    }
}