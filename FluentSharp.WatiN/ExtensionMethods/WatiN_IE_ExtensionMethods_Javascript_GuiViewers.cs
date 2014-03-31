using System;
using System.Collections;
using System.Windows.Forms;
using FluentSharp.WinForms;
using FluentSharp.CoreLib;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_Javascript_GuiViewers
    {
        public static WatiN_IE view_JavaScriptVariable_AsTreeView(this WatiN_IE ie, string rootVariableName)
        {
            var treeView = "Javascript variable: {0}".format(rootVariableName).popupWindow(500,400).add_TreeView();
    		
            Action<TreeNode,string> add_Object =
                (treeNode, objRef)=>{
                                        var _jsObject = ie.getJsObject(objRef);							
                                        if (_jsObject is IEnumerable)
                                            foreach(var item in _jsObject as IEnumerable)
                                                treeNode.add_Node(item.comObject_TypeName(), item, true); 
                                        else
                                            treeNode.add_Node(_jsObject); 								
                }; 
			 
            treeView.beforeExpand<object>(
                (treeNode, _object) => {
                                           if (_object is IEnumerable)
                                               foreach(var item in _object as IEnumerable)
                                                   treeNode.add_Node(item.comObject_TypeName(), item, true); 
                                           else
                                           {
                                               ie.setJsObject(_object);
                                               foreach(var variableName in ie.javascript_ObjectItems("_jsObject"))
                                               {
                                                   var variableValue = ie.javascript_VariableValue( "_jsObject.{0}".format(variableName));
                                                   if (variableValue.typeFullName() == "System.__ComObject") 										
                                                       treeNode.add_Node(variableName, variableValue,true);  										
                                                   else
                                                   {
                                                       var nodeText = "{0}: {1}".format(variableName, variableValue);
                                                       //add_Object(treeNode, "_jsObject.{0}".format(item));
                                                       treeNode.add_Node(nodeText);
                                                   }
                                               }
                                           }
                });
						
            add_Object(treeView.rootNode(),rootVariableName);
            return ie;
        }
    }
}