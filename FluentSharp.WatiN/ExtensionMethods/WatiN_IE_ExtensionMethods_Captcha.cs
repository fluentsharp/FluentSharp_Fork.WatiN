using FluentSharp.CoreLib;
using FluentSharp.WinForms.Controls;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Captcha
    {                
        public static string resolveCaptcha(this WatiN_IE watinIe, string captchaImageUrl)
        {
            return ascx_CaptchaQuestion.askQuestion(captchaImageUrl);
        }
 
        public static string resolveCaptcha(this WatiN_IE watinIe, TextField textField)
        {
            return watinIe.resolveCaptcha(textField.value());
        }
 
        public static WatiN_IE resolveCaptcha(this WatiN_IE watinIe, string questionField, string answerField)
        {
            var questionUrl = watinIe.textField(questionField).value();
            if (questionUrl.valid())
            {
                var captchaAnswer = watinIe.resolveCaptcha(questionUrl);
                watinIe.textField(answerField).value(captchaAnswer);
            }
            return watinIe;    		
        } 
    }
}