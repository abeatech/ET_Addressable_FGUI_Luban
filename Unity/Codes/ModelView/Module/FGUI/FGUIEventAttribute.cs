using Cfg;
using System;

namespace ET
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FGUIEventAttribute: BaseAttribute
    {
        public FGUIType UIType { get; }

        public FGUIEventAttribute(FGUIType uiType)
        {
            this.UIType = uiType;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FGUIObjectAttribute : BaseAttribute
    {

    }
    [AttributeUsage(AttributeTargets.Class)]
    public class FGUIComponentAttribute : BaseAttribute
    {
        public FGUIType UIType { get; }

        public FGUIComponentAttribute(FGUIType uiType)
        {
            this.UIType = uiType;
        }
    }
}