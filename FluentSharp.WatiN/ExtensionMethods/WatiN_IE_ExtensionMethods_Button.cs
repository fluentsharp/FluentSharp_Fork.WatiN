using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Button
    {  	    	
        /// <summary>
        /// Finds a button based on the provided searchText (which is normalized to lowercase and trimmed), 
        /// using the folowing search sequence (of html attributes):
        ///    - name
        ///    - id 
        ///    - value
        ///    - className
        /// </summary>
        /// <param name="watinIe"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static WatiN.Core.Button button(this WatiN_IE watinIe, string searchText)
        {
            searchText = searchText.trim().lower();
            if (searchText.valid())
            {                
                foreach(var button in watinIe.buttons())
                    if ((button.name     ().lower() == searchText) || 
                        (button.id       ().lower() == searchText) || 
                        (button.value    ().lower() == searchText) || 
                        (button.className().lower() == searchText))
                    { 
                        return button;
                    }
                "in WatiN_IE could not find Button with provided searchText (searched on name, id, value and classname ):{0}".error(searchText);
            }    				            
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
            return (from button in buttons 
                    select button.name()).toList();
        }
 
        public static string value(this WatiN.Core.Button button)
        {    		
            return (button != null)
                       ? button.Value.trim()
                       : "";
        }
        public static string name(this WatiN.Core.Button button)
        {    		
            return (button != null)
                       ? button.Name.trim()
                       : "";
        }
        public static string outerText(this WatiN.Core.Button button)
        {    		
            return (button != null)
                       ? button.OuterText.trim()
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