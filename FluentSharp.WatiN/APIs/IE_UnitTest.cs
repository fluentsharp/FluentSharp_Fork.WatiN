using System;
using System.Windows.Forms;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.WinForms;

namespace FluentSharp.Watin
{
    public class IE_UnitTest
    {
        public static IE_UnitTest Current        { get; set; }

        public        String      FormTitle       { get; set;}        
        public        String      TargetServer   { get; set;}                
        public        WatiN_IE    ie;          

        public IE_UnitTest()
        {
            FormTitle = "IE_UnitTest".add_5_RandomLetters();
            this.open_IE(); 
        }        
        
        public string html()
        {
            return ie.html();
        }
        public IE_UnitTest waitForClose()
        {
            ie.parentForm().waitForClose();
            return this;
        }
        /*public IE_UnitTest script_IE_WaitForClose()
        {
            script_IE();
            return waitForClose();
        }
        public ascx_Simple_Script_Editor script_IE()
        {
            var code = 
@"return ie.url();

//O2Ref:FluentSharp.WatiN.dll
//using FluentSharp.Watin;
//O2Ref:WatiN.Core.dll";

            return ie.script_Me("ie")
              .set_Code(code);            
        }*/
    }

    public static class IE_TBot_ExtensionMethods
    {        
        public static T close_IE<T>(this T ieUnitTest) where T : IE_UnitTest
        {
            ieUnitTest.ie.parentForm().close();
            IE_UnitTest.Current = null;
            return ieUnitTest;
        }
        public static T open_IE<T>(this T ieUnitTest) where T : IE_UnitTest
        {
            if (IE_UnitTest.Current.isNull())
            {
                IE_UnitTest.Current    = ieUnitTest;            
                ieUnitTest.ie          = ieUnitTest.FormTitle.add_IE_Hidden_PopupWindow();                 
                ieUnitTest.ie.silent(true);
            }
            else
            {
                ieUnitTest.ie          = IE_UnitTest.Current.ie;                
                ieUnitTest.FormTitle   = ieUnitTest.ie.parentForm().title();
                
            }
            return ieUnitTest;
        }

        public static IE_UnitTest open(this IE_UnitTest ieUnitTest, string virtualPath)     // can't use generics here in order to have simpler method calls: tbot.open("....");  vs tbot.open<API_IE_TBot>("...");
        {
            return ieUnitTest.open_Page(virtualPath);
        }

        public static IE_UnitTest open_Page(this IE_UnitTest tbot, string virtualPath)
        {
            tbot.ie.open(tbot.fullPath(virtualPath));
            return tbot;
        }
        public static string fullPath(this IE_UnitTest tbot, string virtualPath)
        {
            return tbot.TargetServer.uri().append(virtualPath).str();
        }

        public static IE_UnitTest open_ASync(this IE_UnitTest tbot, string virtualPath)
        {
            O2Thread.mtaThread(()=> tbot.open_Page(virtualPath));
            return tbot;
        }
    }
}
