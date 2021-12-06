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
    public class FGUIComponentDestroySystem : DestroySystem<FGUIComponent>
    {
        public override void Destroy(FGUIComponent self)
        {
            if(self.Root != null)
            {
                GRoot.inst.RemoveChild(self.Root);
            }
        }
    }

    public static class FGUIComponentSystem
    {
        public static void BindRoot<T>(this T self) where T : FGUIComponent
        {
            Type type = typeof(T);
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                var attribute = fieldInfo.GetCustomAttributes(typeof(FGUIObjectAttribute), false).FirstOrDefault();
                if(attribute == null)
                {
                    continue;
                }
                if(fieldInfo.FieldType == typeof(Controller))
                {
                    Controller ctrl = self.Root.GetController(fieldInfo.Name);
                    fieldInfo.SetValue(self, ctrl);
                }
                else if(fieldInfo.FieldType == typeof(Transition))
                {
                    Transition tran = self.Root.GetTransition(fieldInfo.Name);
                    fieldInfo.SetValue(self, tran);
                }
                else
                {
                    GObject gObj = self.Root.GetChild(fieldInfo.Name);
                    if (gObj != null)
                    {
                        if (gObj.GetType() != fieldInfo.FieldType)
                        {
                            Debug.LogError($"{type.Name}的{fieldInfo.Name}绑定失败,字段类型:{fieldInfo.FieldType.Name},组件类型{gObj.GetType().Name}。");
                        }
                        else
                        {
                            fieldInfo.SetValue(self, gObj);
                        }
                    }
                }
            }
        }
        public static void AddButtonListener(this FGUIComponent self,GButton button,Action action)
        {
            button.onClick.Add(()=>action?.Invoke());
        }
    }
}
