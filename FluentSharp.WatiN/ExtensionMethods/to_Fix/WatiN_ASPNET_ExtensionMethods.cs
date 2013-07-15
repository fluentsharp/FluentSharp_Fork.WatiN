namespace FluentSharp.Watin
{
    public static class WatiN_ASPNET_ExtensionMethods
    {    
        //todo: fix missing DotNet_ViewState
        /*
    	public static DotNet_ViewState viewState(this WatiN_IE ie)
    	{
    		return new DotNet_ViewState(ie.viewStateRaw());
    	}
    	public static string viewStateRaw(this WatiN_IE ie)
    	{
    		return ie.field("__VIEWSTATE").value();
    	}
    	
    	public static T showViewState<T>(this T control, WatiN_IE ie, bool showDetailedView)
    		where T : System.Windows.Forms.Control
    	{
    		if (showDetailedView)
    			control.showViewState(ie);
    		else
    			control.showViewStateValues(ie);
    		return control;
    	}
    	
    	public static T showViewState<T>(this T control, WatiN_IE ie)
    		where T : System.Windows.Forms.Control
    	{    		    		    		
    		return ie.viewState().show(control);    		
    	}


		public static T showViewStateValues<T>(this T control, WatiN_IE ie)
    		where T : System.Windows.Forms.Control
    	{    		    		    		
    		return ie.viewState().showValues(control);    		
    	} */   	    	   	
    }
}