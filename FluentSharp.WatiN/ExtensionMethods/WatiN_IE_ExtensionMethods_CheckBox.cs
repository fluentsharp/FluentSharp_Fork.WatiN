using System;
using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_CheckBox
    {
 
        public static WatiN.Core.CheckBox checkBox(this WatiN_IE watinIe, string name)
        {
            //watinIe.textFields();   // after some events 
            foreach(var checkBox in watinIe.checkBoxes())
                if (checkBox.id() == name) // || checkBox.title() == name)
                    return checkBox;
            "in WatiN_IE could not find CheckBox with name:{0}".error(name ?? "[null value]");
            return null;    				
        }
 
        public static List<WatiN.Core.CheckBox> checkBoxes(this WatiN_IE watinIe)
        {
            return (from checkBox in watinIe.IE.CheckBoxes
                    select checkBox).toList();
        }
 
        public static string id(this WatiN.Core.CheckBox checkBox)
        {
            return (checkBox != null)
                       ? checkBox.Id
                       : "";
        }
 
        public static List<string> ids(this List<WatiN.Core.CheckBox> checkBoxes)
        {
            return (from checkBox in checkBoxes 
                    select checkBox.id()).toList();
        }
 
        public static bool value(this WatiN.Core.CheckBox checkBox)
        {    		
            return (checkBox != null)
                       ? checkBox.Checked
                       : false;
        }
 
        public static List<bool> values(this List<WatiN.Core.CheckBox> checkBoxes)
        {
            return (from checkBox in checkBoxes 
                    select checkBox.value()).toList();
        }
 
        public static WatiN.Core.CheckBox value(this WatiN.Core.CheckBox checkBox, bool value)
        {
            if (checkBox!= null)    
                try
                {
                    checkBox.Checked = value;    	
                }
                catch(Exception ex)
                {
                    ex.log("in WatiN.Core.CheckBox value");
                }
            return checkBox;
        }
 
        public static WatiN.Core.CheckBox check(this WatiN.Core.CheckBox checkBox)
        {    		
            return checkBox.value(true);
        }
 
        public static WatiN.Core.CheckBox uncheck(this WatiN.Core.CheckBox checkBox)
        {    		
            return checkBox.value(false);
        }
 
    }
}