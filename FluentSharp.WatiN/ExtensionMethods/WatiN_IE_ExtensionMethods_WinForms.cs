using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluentSharp.CoreLib;
using FluentSharp.WinForms;
using FluentSharp.WinForms.Controls;
using WatiN.Core;
using Control = System.Windows.Forms.Control;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_WinForms
    {
        public static string DEFAULT_IE_MESSAGE = "Welcome to FluentSharp.WatiN's embedded IE object";
        public static Panel showElementsInTreeView(this WatiN_IE watinIe)
        {
            var hostPanel = O2Gui.open<Panel>("WatiN element details",400,400);
            var controls = hostPanel.add_1x1("Html elements", "Propeties");
            var propertyGrid = controls[1].add_PropertyGrid();
            controls[0].add_TreeView()
                       .add_Nodes(watinIe.elements().indexedByTagName())
                       .sort()
                       .showSelection()
                       .beforeExpand<List<Element>>(
                           (treeNode, elements) => 
                               {
                                   try { treeNode.add_Nodes(elements);}
                                   catch(Exception ex) { ex.log("in beforeExpand<List<Element>>");}
                               })
                       .afterSelect<Element>((element)=> propertyGrid.show(element))
                       .afterSelect<List<Element>>((elements)=> propertyGrid.show(elements[0]));
            return hostPanel;
        }
        // Control Extensionmethods
         /// <summary>
         /// Adds an embeded WatiN_IE control to the provided WinForms Control object
         /// </summary>
         /// <param name="control">This should be a Panel or another similar Control that can host controls</param>
         /// <returns>Watin_IE object</returns>
        public static WatiN_IE add_IE(this Control control)
        {
            try
            {
                var ie = WatiN_IE.window(control);
                ie.showMessage(DEFAULT_IE_MESSAGE);        // show an message so that the IE object is left in a stable condition
                return ie;
            }
            catch (Exception ex)
            {
                ex.log();
                return null;
            }
        }
        /// <summary>
        /// Creates a popupWindow (i.e. new WinForms Form) with the provided title.
        /// 
        /// This is the same as calling <code>"{title}".popupWindow().add_IE</code>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static WatiN_IE popupWindow_With_IE(this string title)
        {
            return title.popupWindow().add_IE();            
        }
        /// <summary>
        /// Same as: popupWindow_Hidden_With_IE(this string title)
        /// </summary>
        /// <param name="title"></param>
        /// <returns>WatiN_IE </returns>
        public static WatiN_IE add_IE_Hidden_PopupWindow(this string title)
        {
            return title.popupWindow_Hidden_With_IE();
        }
        /// <summary>
        /// Creates a hidden popupWindow (i.e. new WinForms Form) with the provided title.
        /// 
        /// This is the same as calling <code>"{title}".popupWindow_Hidden().add_IE</code>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>        
        public static WatiN_IE popupWindow_Hidden_With_IE(this string title)
        {
            return title.popupWindow_Hidden().add_IE();            
        }
    	/// <summary>
    	/// Adds an embeded IE WatiN to the provied object and adds an navigation bar at the top (which is Synced with the browser)
    	/// </summary>
    	/// <param name="control"></param>
    	/// <returns></returns>
        public static WatiN_IE add_IE_with_NavigationBar(this Control control)
        {            
            var watinIe = control.add_IE();
            watinIe.add_NavigationBar(control);
            return watinIe;
        }
    	
    	/// <summary>
    	/// Adds a navigation bar to the provided control (synced with the provided WatiN_IE object)
    	/// </summary>
    	/// <param name="watinIe"></param>
    	/// <param name="control"></param>
    	/// <returns></returns>
        public static WatiN_IE add_NavigationBar(this WatiN_IE watinIe, Control control)
        {
            if (watinIe.isNull())
                return watinIe;
            var urlTextBox = control.insert_Above(20)
                                    .add_TextBox("Url:","")
                                    .onEnter((text)=> watinIe.open_ASync(text))
                                    .onTextChange((text)=>
                                        {
                                            if (text.contains("".line()))
                                                watinIe.open_ASync(text);
                                        });

            watinIe.onNavigate((url)=> urlTextBox.set_Text(url));
            return watinIe;
        }
 
        public static WatiN_IE minimized(this WatiN_IE watinIe)
        {
            watinIe.HostControl.minimize();
            return watinIe;
        }
 		
        public static WatiN_IE maximized(this WatiN_IE watinIe)
        {
            watinIe.HostControl.maximize();
            return watinIe;
        }
        /// <summary>
        /// Returns the parent WindForm Form object (note that this is NOT a WatiN.Core.Form object)
        /// 
        /// This is the same as calling <code>watinIe.HostControl.parentForm()</code>
        /// </summary>
        /// <param name="watinIe"></param>
        /// <returns>System.Windows.Forms.Form</returns>
        public static System.Windows.Forms.Form parentForm(this WatiN_IE watinIe)
        {
            if (watinIe.notNull() && watinIe.HostControl.notNull())
                return watinIe.HostControl.parentForm();
            return null;
        }
    }
}