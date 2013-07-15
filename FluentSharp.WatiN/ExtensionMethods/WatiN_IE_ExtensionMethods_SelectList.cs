using System;
using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;
using WatiN.Core;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_SelectList
    {	
 
        public static SelectList selectList(this WatiN_IE watinIe, string name)
        {    		
            foreach(var selectList in watinIe.selectLists())
                if (selectList.id() == name)
                    return selectList;
            "in WatiN_IE could not find SelectList with name:{0}".error(name ?? "[null value]");
            return null;    				
        }
 
        public static List<SelectList> selectLists(this WatiN_IE watinIe)
        {
            return (from selectList in watinIe.IE.SelectLists
                    select selectList).toList();
        }
 
        public static string id(this SelectList selectList)
        {
            return (selectList != null)
                       ? selectList.Id
                       : "";
        }
 
        public static List<string> ids(this List<SelectList> selectLists)
        {
            return (from selectList in selectLists 
                    select selectList.id()).toList();
        }
 
        public static List<Option> options(this SelectList selectList)
        {
            return (from option in selectList.Options 
                    select option).toList();
        }
 
        public static Option select(this Option option)
        {
            try
            {
                if (option != null)
                    option.Select();
            }
            catch(Exception ex)
            {
                ex.log("in Option select");
            }
            return option;
        }
 
        public static SelectList select(this SelectList selectList, int index)
        {
            var options = selectList.options();
            if (index < options.size())
                options[index].select();
            return selectList;
        }
    }
}