using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.REPL;
using FluentSharp.REPL.Controls;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace FluentSharp.NUnit
{
    public static class Extra_Watin_ExtensionMethods
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
