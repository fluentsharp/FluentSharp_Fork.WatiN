using System.Collections.Generic;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_JavaScript_Helpers
    {
        public static List<string> javascript_ObjectItems(this WatiN_IE ie, string targetObject)
        {
            return ie.invokeEval("var result = []; for(var item in " + targetObject + ") result.push(item);return result.toString();").str().split(",");
        }
    	
        public static T javascript_VariableValue<T>(this WatiN_IE ie, string variableName, string propertyName)
        {
            return ie.javascript_VariableValue<T>("{0}.{1}".format(variableName, propertyName));
        }
    	
        public static T javascript_VariableValue<T>(this WatiN_IE ie, string variableName)
        {
            var result = ie.javascript_VariableValue(variableName);
            if (result is T)
                return (T)result;
            return default(T);
        }
        public static object javascript_VariableValue(this WatiN_IE ie, string variableName, string propertyName)
        {
            return ie.javascript_VariableValue("{0}.{1}".format(variableName, propertyName));
        }
    	
        public static object javascript_VariableValue(this WatiN_IE ie, string variableName)
        {
            return ie.invokeEval("return {0}".format(variableName));
        }
    }
}