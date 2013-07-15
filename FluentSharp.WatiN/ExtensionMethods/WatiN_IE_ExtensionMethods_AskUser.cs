using System.Windows.Forms;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.Utils;
using FluentSharp.WinForms.Controls;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_AskUser
    {
        public static string askUserQuestion(this WatiN_IE watinIe, string question, string title, string defaultValue)
        {
            var assembly =  "Microsoft.VisualBasic".assembly();
            var intercation = assembly.type("Interaction");
 
            var parameters = new object[] {question,title,defaultValue,-1,-1}; 
            return intercation.invokeStatic("InputBox",parameters).str(); 
        }
 
        // user interaction 
 
        public static WatiN_IE askUserToContinue(this WatiN_IE watinIe)
        {
            MessageBox.Show("Click OK to Continue the WatiN IE workflow", "O2 Message",MessageBoxButtons.OK, MessageBoxIcon.Question); 
            return watinIe;
        }
        //todo: fix missing ascx_AskUserForLoginDetails
        
        public static Credential askUserForUsernameAndPassword(this WatiN_IE watinIe)
        {
            return watinIe.askUserForUsernameAndPassword("");
        }
        public static Credential askUserForUsernameAndPassword(this WatiN_IE watinIe, string loginType)
        {
            var credential = ascx_AskUserForLoginDetails.ask();
            if (loginType.valid())
                credential.CredentialType = loginType;
            return credential;
        }
    
    }
}