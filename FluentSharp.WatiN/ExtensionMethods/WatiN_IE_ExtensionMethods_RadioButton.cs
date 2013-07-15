using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_RadioButton
    {
 
        /*public static WatiN.Core.RadioButton radioButton(this WatiN_IE watinIe, string id)
    	{
    		//watinIe.textFields();   // after some events 
    		foreach(var radioButton in watinIe.radioButtons())
    			if (radioButton.id() == id || radioButton.title() == id)
    				return radioButton;
    		"in WatiN_IE could not find RadioButton with id or name:{0}".error(name ?? "[null value]");
    		return null;    				
    	}*/
 
        public static List<WatiN.Core.RadioButton> radioButtons(this WatiN_IE watinIe)
        {
            return (from radioButton in watinIe.IE.RadioButtons
                    select radioButton).toList();
        }
    	
        public static List<WatiN.Core.RadioButton> radioButtons(this WatiN_IE watinIe, string name)
        {
            return (from radioButton in watinIe.IE.RadioButtons
                    where (radioButton.name() == name)
                    select radioButton    				
                   ).toList();
        }
    	    	
 
        public static string id(this WatiN.Core.RadioButton radioButton)
        {
            return (radioButton != null)
                       ? radioButton.Id
                       : "";
        }
 
        public static List<string> ids(this List<WatiN.Core.RadioButton> radioButtons)
        {
            return (from radioButton in radioButtons 
                    select radioButton.id()).toList();
        }
    	
        public static string name(this WatiN.Core.RadioButton radioButton)
        {
            return (radioButton != null)
                       ? radioButton.attribute("name")
                       : "";
        }
 
        public static List<string> names(this List<WatiN.Core.RadioButton> radioButtons)
        {
            return (from radioButton in radioButtons 
                    select radioButton.name()).Distinct().toList();
        }
 
        public static List<WatiN.Core.RadioButton> withName(this List<WatiN.Core.RadioButton> radioButtons, string name)
        {
            return (from radioButton in radioButtons 
                    where (radioButton.name() == name)
                    select radioButton ).toList();
        }
    	
        public static WatiN.Core.RadioButton withValue(this List<WatiN.Core.RadioButton> radioButtons, string value)
        {
            foreach(var radioButton in radioButtons)
                if (radioButton.value().trim()==value)
                    return radioButton;
            return null;
        }
    	
        public static string value(this WatiN.Core.RadioButton radioButton)
        {    		
            return (radioButton != null)
                       ? radioButton.TextAfter
                       : null;
        }
    	
    	
 
        public static List<string> values(this List<WatiN.Core.RadioButton> radioButtons)
        {
            return (from radioButton in radioButtons 
                    select radioButton.value()).toList();
        }
 		 		
 		
        public static WatiN.Core.RadioButton check(this WatiN.Core.RadioButton radioButton, bool value)
        {
            if (radioButton!= null)    
                try
                {
                    radioButton.Checked = value;    	
                }
                catch//(Exception ex)
                {    			
                    //ex.log("in WatiN.Core.RadioButton value::");  // there is an public WatiN exception that occurs after the value is set
                }
            return radioButton;
        }
 
        public static bool @checked(this WatiN.Core.RadioButton radioButton)
        {
            return radioButton.Checked;
        }
    	
        public static WatiN.Core.RadioButton @checked(this WatiN.Core.RadioButton radioButton, bool value)
        {
            return radioButton.check(value);
        }
    	
        /*public static WatiN.Core.RadioButton check(this WatiN.Core.RadioButton radioButton)
    	{    		
    		return radioButton.value(true);
    	}
 
    	public static WatiN.Core.RadioButton uncheck(this WatiN.Core.RadioButton radioButton)
    	{    		
    		return radioButton.value(false);
    	}*/
 
    }
}