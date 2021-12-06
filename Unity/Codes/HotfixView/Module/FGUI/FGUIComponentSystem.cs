using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public static class FGUIHelper
    {
        public static void BindRoot(Type type,object bindObj,GComponent gComponent)
        {
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                var attribute = fieldInfo.GetCustomAttributes(typeof(FGUIObjectAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    continue;
                }
                if (fieldInfo.FieldType == typeof(Controller))
                {
                    Controller ctrl = gComponent.GetController(fieldInfo.Name);
                    fieldInfo.SetValue(bindObj, ctrl);
                }
                else if (fieldInfo.FieldType == typeof(Transition))
                {
                    Transition tran = gComponent.GetTransition(fieldInfo.Name);
                    fieldInfo.SetValue(bindObj, tran);
                }
                else
                {
                    GObject gObj = gComponent.GetChild(fieldInfo.Name);
                    if (gObj != null)
                    {
                        if (gObj.GetType() != fieldInfo.FieldType)
                        {
                            Debug.LogError($"{type.Name}的{fieldInfo.Name}绑定失败,字段类型:{fieldInfo.FieldType.Name},组件类型{gObj.GetType().Name}。");
                        }
                        else
                        {
                            fieldInfo.SetValue(bindObj, gObj);
                        }
                    }
                }
            }
        }
        public static void BindRoot<T>(T bindObj,GComponent gComponent)
        {
            Type type = typeof(T);
            BindRoot(type, bindObj, gComponent);
        }
        public static void AddButtonListener(GButton button,Action action)
        {
            button.onClick.Add(()=>action?.Invoke());
        }
    }
}
