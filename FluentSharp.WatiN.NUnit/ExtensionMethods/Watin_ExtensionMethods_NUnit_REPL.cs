using FluentSharp.CoreLib;
using FluentSharp.REPL;
using FluentSharp.REPL.Controls;
using FluentSharp.Watin;
using FluentSharp.WinForms;

namespace FluentSharp.WatiN.NUnit
{
    public static class Watin_ExtensionMethods_NUnit_REPL
    {
        public static WatiN_IE script_IE_WaitForClose(this WatiN_IE ie)
        {
            ie.script_IE().waitForClose();
            return ie;
        }
        public static ascx_Simple_Script_Editor script_IE(this WatiN_IE ie)
        {            
            ie.parentForm().show();                                     // in case it is hidden
            var codeToAppend = "//using FluentSharp.Watin".line() +     // required refereces to allow easy scripting
                               "//using WatiN.Core       ".line() +
                               "//O2Ref:WatiN.Core.dll   ";

            var scriptEditor = ie.script_Me("ie")                       // set varaible name to 'ie' instead of 'watiN_IE'         
                                 .code_Append(codeToAppend);

            return scriptEditor;
        }
    }
}
