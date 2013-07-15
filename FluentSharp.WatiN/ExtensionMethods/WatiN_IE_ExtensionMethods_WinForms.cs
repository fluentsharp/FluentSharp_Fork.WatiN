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
 
        public static WatiN_IE add_IE(this Control control)
        {
            try
            {
                return WatiN_IE.window(control);
            }
            catch (Exception ex)
            {
                ex.log();
                return null;
            }
        }
    	
        public static WatiN_IE add_IE_with_NavigationBar(this Control control)
        {            
            var watinIe = control.add_IE();
            watinIe.add_NavigationBar(control);
            return watinIe;
        }
    	
    	
        public static WatiN_IE add_NavigationBar(this WatiN_IE watinIe, Control control)
        {
            if (watinIe.isNull())
                return watinIe;
            var urlTextBox = control.insert_Above(20)
                                    .add_TextBox("Url:","")
                                    .onEnter((text)=> watinIe.open_ASync(text));
            watinIe.onNavigate((url)=> urlTextBox.set_Text(url));
            return watinIe;
        }
 
        public static WatiN_IE minimized(this WatiN_IE watinIe)
        {
            watinIe.HostControl.minimized();
            return watinIe;
        }
 		
        public static WatiN_IE maximized(this WatiN_IE watinIe)
        {
            watinIe.HostControl.maximized();
            return watinIe;
        }
    }
}