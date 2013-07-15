using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Button
    {  	    	
        public static WatiN.Core.Button button(this WatiN_IE watinIe, string identifier)
        {
            identifier = identifier.trim();
            if (identifier.valid())
            {
                identifier = identifier.trim();
                foreach(var button in watinIe.buttons())
                    if ((button.id().notNull() && button.id().trim() == identifier) || 
                        (button.value().notNull() && button.value().trim() == identifier) ||
                        (button.className().notNull() && button.className().trim() == identifier) ||
                        (button.outerText().notNull() && button.outerText().trim() == identifier) )
                        return button;
            }    				
            "in WatiN_IE could not find Button with identifier (searched on id,name, classname and outerText):{0}".error(identifier ?? "[null value]");
            return null;    				
        }
 
        public static List< WatiN.Core.Button> buttons(this WatiN_IE watinIe)
        {
            return (from button in watinIe.IE.Buttons
                    select button).toList();
        }
 
 
        public static List<string> texts(this List<WatiN.Core.Button> buttons)
        {
            return (from button in buttons 
                    select button.text()).toList();
        }
 
        public static List<string> values(this List<WatiN.Core.Button> buttons)
        {
            return (from button in buttons 
                    select button.value()).toList();
        }
 
        public static List<string> ids(this List<WatiN.Core.Button> buttons)
        {
            return (from button in buttons 
                    select button.id()).toList();
        }
 
        public static List<string> names(this List<WatiN.Core.Button> buttons)
        {
            return buttons.ids();
        }
 
        public static string value(this WatiN.Core.Button button)
        {    		
            return (button != null)
                       ? button.Value
                       : "";
        }
        public static string outerText(this WatiN.Core.Button button)
        {    		
            return (button != null)
                       ? button.OuterText
                       : "";
        }
 		 
        public static WatiN.Core.Button click(this WatiN.Core.Button button)
        {    		
            if (button != null)
                button.Click();
            return button;
        }
 
        public static bool hasButton(this WatiN_IE watinIe, string nameOrId)
        {	
            foreach(var button in watinIe.buttons())
                if (button.id() == nameOrId || button.value() == nameOrId)
                    return true;
            return false;
            //return watinIe.buttons().ids().Contains(id);						
        }
 
        public static WatiN.Core.Button waitForButton(this WatiN_IE watinIe, string nameOrId)
        {
            return watinIe.waitForButton(nameOrId, 500, 10);
        }
		
        public static WatiN.Core.Button waitForButton(this WatiN_IE watinIe, string nameOrId, int sleepMiliseconds, int maxSleepTimes)
        {
		
            var count = 0;
            while(watinIe.hasButton(nameOrId).isFalse())
            {
                if (count++ >=maxSleepTimes)
                    break;
                watinIe.sleep(500, false);
            }
            return watinIe.button(nameOrId);
        }
 
        public static WatiN_IE click(this WatiN_IE watinIe, string id)
        {
            if (watinIe.hasButton(id))
            {
                var button = watinIe.button(id);			
                button.click();
            }
            else if (watinIe.hasLink(id))
            {
                var link = watinIe.link(id);			
                link.click();
            }
            else
                "in WatiN_IE click, could not find button or link with id: {0}".error(id);
            return watinIe;
 
        }

    }
}