using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_TextField
    {	
        public static TextField field(this WatiN_IE watinIe, string name)
        {
            return watinIe.textField(name);
        }
 
        public static List<TextField> fields(this WatiN_IE watinIe)
        {
            return watinIe.textFields();
        }
 
        public static bool hasField(this WatiN_IE watinIe, string name)
        {
            return watinIe.textFieldExists(name);
        }
 
        public static TextField textField(this WatiN_IE watinIe, string name)
        {
            //watinIe.textFields();   // after some events     		
            foreach(var textField in watinIe.textFields())
                if (textField.name() == name || textField.title() == name || textField.id() == name)
                    return textField;
            "in WatiN_IE could not find TextField with name:{0}".error(name ?? "[null value]");
            return null;    				
        }
 
        public static bool textFieldExists(this WatiN_IE watinIe, string name)
        {
            return watinIe.textField(name).notNull();    		
        }
    	
    	
        public static TextField waitForField(this WatiN_IE watinIe, string nameOrId)
        {
            return watinIe.waitForField(nameOrId, 500, 10);
        }
		
        public static TextField waitForField(this WatiN_IE watinIe, string nameOrId, int sleepMiliseconds, int maxSleepTimes)
        {
		
            var count = 0;
            while(watinIe.hasField(nameOrId).isFalse())
            {
                if (count++ >=maxSleepTimes)
                    break;
                watinIe.sleep(500, false);
            }
            return watinIe.field(nameOrId);
        }
    	
        public static List<TextField> textFields(this WatiN_IE watinIe)
        {
            return (from textField in watinIe.IE.TextFields
                    select textField).toList();
        }
 
        public static string name(this TextField textField)
        {
            return (textField != null)
                       ? textField.Name
                       : "";
        }
 
        public static List<string> names(this List<TextField> textFields)
        {
            return (from textField in textFields 
                    select textField.name()).toList();
        }
 
        public static string value(this TextField textField)
        {    		
            return (textField != null)
                       ? textField.Value
                       : "";
        }
 
        public static List<string> values(this List<TextField> textFields)
        {
            return (from textField in textFields 
                    select textField.value()).toList();
        }
 		
        public static TextField equals(this TextField textField, string value)
        {
            if (textField!= null)    		
                textField.Value = value;    	
            return textField;
        }
  	
        public static TextField value(this TextField textField, string value)
        {
            if (textField!= null)    		
                textField.Value = value;    	
            return textField;
        }
 
        public static List<string> texts(this List<TextField> textFields)
        {
            return (from textField in textFields
                    select textField.text()).toList();
        }
 
        public static List<TextField> texts(this List<TextField> textFields, string text)
        {
            return (from textField in textFields
                    where textField.text() == text
                    select textField).toList();
        }
        public static TextField appendLine(this TextField textField, string textToAppend)
        {
            return textField.appendText(textToAppend.line());
        }
 
        public static TextField appendText(this TextField textField, string textToAppend)
        {
            if (textField!= null)
            {
                textField.value(textField.value() + textToAppend);
            }
            return textField; 
        }
 
        public static WatiN_IE set_Value(this WatiN_IE watinIe, string textFieldId, string text)
        {
            return watinIe.value(textFieldId, text);
        }
 
        public static WatiN_IE field(this WatiN_IE watinIe, string textFieldId, string text)
        {
            return watinIe.value(textFieldId, text);
        }
 		
 		
        public static WatiN_IE value(this WatiN_IE watinIe, string textFieldId, string text)
        {
            var textField = watinIe.textField(textFieldId);
            if (textField != null)
                textField.value(text);
            else
                "in WatiN_IE value, could not find textField with id: {0}".error(text);
            return watinIe;
 
        }
        public static bool enabled(this TextField field)
        {
            return !(bool)field.htmlElement().prop("disabled");
        }
        public static TextField enabled(this TextField field, bool value)
        {			
			
            Reflection_ExtensionMethods_Properties
                .prop(field.htmlElement(),
                      "disabled",
                      ! value);
            return field;			
        }		
    }
}