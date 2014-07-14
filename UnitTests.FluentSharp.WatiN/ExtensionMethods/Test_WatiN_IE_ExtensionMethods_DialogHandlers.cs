﻿using System.Threading;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.NUnit;
using FluentSharp.Watin;
using FluentSharp.Web35;
using FluentSharp.WinForms;
using NUnit.Framework;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Interfaces;

namespace UnitTests.FluentSharp.WatiN
{
    [TestFixture]
    public class Test_Extra_WatiN_IE_ExtensionMethods_DialogHandlers
    {
        [Test] public void setDialogWatcher()
        {
            var watinIe = "setDialogWatcher".add_IE_Hidden_PopupWindow();
            watinIe.IE.DialogWatcher.assert_Null();
            watinIe.setDialogWatcher();
            watinIe.IE.DialogWatcher.assert_Not_Null();
        }

        [Test] public void getDialogWatcher()
        {
            var watinIe = "getDialogWatcher".add_IE_Hidden_PopupWindow();
            watinIe.getDialogWatcher().assert_Not_Null()
                                      .assert_Instance_Of<DialogWatcher>()
                                      .assert_Equal_To(watinIe.IE.DialogWatcher);
        }
        [Test] public void dialogHandlers()
        {            
            var watinIe = "dialogHandlers".add_IE_Hidden_PopupWindow();
            watinIe.clear_DialogHandlers()
                   .dialogHandlers().assert_Size_Is(0);                                    
        }

        
        [Test] public void getAlertsHandler()
        {
            var watinIe = "getAlertsHandler".add_IE_Hidden_PopupWindow();
            var alertHandler = watinIe.getAlertsHandler().assert_Not_Null()
                                                         .assert_Instance_Of<IDialogHandler>();
            watinIe.dialogHandlers().assert_Size_Is(1)
                                    .assert_Item_Is_Equal(0, alertHandler);
        }

        [Test] public void alerts()
        {
            var before = O2Thread.ThreadsCreated.size();
            var watinIe = "alerts".add_IE_PopupWindow();

           var alertHandler = watinIe.getAlertsHandler().assert_Not_Null()
                                                         .assert_Are_Equal(handler =>handler.Alerts.size(), 0)
                                                         .assert_Are_Equal(handler =>handler.alerts().size(), 0);
            watinIe.eval("alert(12);alert('2nd alert');");            
            alertHandler.alerts().assert_Size_Is(2)
                                 .assert_Item_Is_Equal(0,"12")
                                 .assert_Item_Is_Equal(1,"2nd alert");
        }
        [Test] [Ignore("Doesn't work reliable when other alerts have been set")]
        public void open_and_HandleFileDownload()
        {
            if("".offline())
                "Skipped because we're offline".assert_Ignore();
            
            var teamMentorJson = "https://teammentor.net/whoami";

            var watinIe = "open_and_HandleFileDownload".add_IE_PopupWindow();

            teamMentorJson.uri().HEAD().assert_True();

            watinIe.open_and_HandleFileDownload(teamMentorJson, "whoami.json")
                   .assert_Not_Null()
                   .assert_File_Exists()
                   .assert_File_Contains("{", "}");
            
            watinIe.parentForm().close();
        }
    }    
}
