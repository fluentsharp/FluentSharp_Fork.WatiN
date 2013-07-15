namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_DialogHandlers
    {
        //todo: fix:  DialogWatcher.GetDialogWatcherForProcess and others were not found
        /*
 		public static WatiN_IE setDialogWatcher(this WatiN_IE watinIe)
 		{ 			
 			if (watinIe.IE.DialogWatcher == null)
			{				
				var dialogWatcher = DialogWatcher.GetDialogWatcherForProcess(Processes.getCurrentProcessID());
				dialogWatcher.CloseUnhandledDialogs = false; 
				//set the value of _dialogWatcher with the current inproc IE dialogWatcher
				var field = PublicDI.reflection.getField(typeof(DomContainer),"_dialogWatcher"); 
				PublicDI.reflection.setField(field, watinIe.IE, dialogWatcher);
			}
			return watinIe;
 		}
 		public static DialogWatcher getDialogWatcher(this WatiN_IE watinIe)
 		{
 			setDialogWatcher(watinIe);
 			return watinIe.IE.DialogWatcher;
 		}
 		
 		public static WatiN_IE clear_DialogWatchers(this WatiN_IE watinIe)
 		{
 			watinIe.IE.DialogWatcher.clear();
 			return watinIe;
 		}
 		public static DialogWatcher clear(this DialogWatcher dialogWatcher)
 		{
 			if (dialogWatcher.notNull())
 				dialogWatcher.Clear();
 			return dialogWatcher;
 		}
 		
 		public static List<BaseDialogHandler> dialogHandlers(this WatiN_IE watinIe)
 		{
 			watinIe.setDialogWatcher();			// make sure this is set 			
 			var dialogHandlers = new List<BaseDialogHandler>();
 			foreach(BaseDialogHandler handler in (ArrayList)watinIe.IE.DialogWatcher.field("handlers"))
 				dialogHandlers.Add(handler);
 			return dialogHandlers;
 			//return (ArrayList)watinIe.IE.DialogWatcher.field("handlers");
 		}
 		
 		public static T dialogHandler<T>(this WatiN_IE watinIe)
 			where T : BaseDialogHandler 
 		{
 			foreach(var dialogHandler in watinIe.dialogHandlers())
 				if (dialogHandler is T)
 					return (T)dialogHandler;
 			return null;
 		}
 		
 		public static AlertAndConfirmDialogHandler getAlertsHandler(this WatiN_IE watinIe)
 		{
 			var alertHandler = watinIe.dialogHandler<AlertAndConfirmDialogHandler>();
 			if (alertHandler.isNull())
 			{
 				alertHandler = new AlertAndConfirmDialogHandler();
				watinIe.IE.AddDialogHandler(alertHandler); 
			}
			return alertHandler;
 		}
 		
 		public static AlertAndConfirmDialogHandler reset(this AlertAndConfirmDialogHandler alertHandler)
 		{
 			alertHandler.Clear();
 			return alertHandler;
 		}
 		
 		public static List<string> alerts(this AlertAndConfirmDialogHandler alertHandler)
 		{
 			return alertHandler.Alerts.toList(); 			
 		}
 		
 		public static string lastAlert(this AlertAndConfirmDialogHandler alertHandler)
 		{
 			if (alertHandler.notNull() && 
 				alertHandler.alerts().notNull() && 
 				alertHandler.alerts().size()>0) 				
 			{
	 			return alertHandler.alerts().Last();
	 		}
 			return "";
 		}
 		
 		public static string open_and_HandleFileDownload(this WatiN_IE watinIe , string url, string fileName)
 		{
			var tmpFile = fileName.tempFile();
			var waitUntilHandled = 20;
			var waitUntilDownload = 300;
			
			var fileDownloadHandler = watinIe.dialogHandler<FileDownloadHandler>();

			if (fileDownloadHandler.notNull())
			{
				watinIe.IE.RemoveDialogHandler(fileDownloadHandler); 
			}
			
			fileDownloadHandler = new FileDownloadHandler(tmpFile); 
			watinIe.IE.AddDialogHandler(fileDownloadHandler); 
			
			 
			fileDownloadHandler.field("saveAsFilename",tmpFile);
			fileDownloadHandler.field("hasHandledFileDownloadDialog",false);
			
			watinIe.open_ASync(url);
			try
			{
				fileDownloadHandler.WaitUntilFileDownloadDialogIsHandled(waitUntilHandled);			
				"after: {0}".info("WaitUntilFileDownloadDialogIsHandled");
				fileDownloadHandler.WaitUntilDownloadCompleted(waitUntilDownload);
				"after: {0}".info("WaitUntilDownloadCompleted");
			}
			catch(Exception ex)
			{
				"[WatiN_IE][open_and_HandleFileDownload] {0}".error(ex.Message);
			}
				
			if (fileDownloadHandler.SaveAsFilename.fileExists())
			{
				"[WatiN_IE] downloaded ok '{0}' into '{1}'".info(url, fileDownloadHandler.SaveAsFilename);
				watinIe.IE.RemoveDialogHandler(fileDownloadHandler); 
				return fileDownloadHandler.SaveAsFilename;
			}
			"[WatiN_IE] failed to download '{0}' ".info(url);
			return null;
		}
        */
    }
}