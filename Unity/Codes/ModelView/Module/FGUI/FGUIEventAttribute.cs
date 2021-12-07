using Cfg;
using System;

namespace ET
{
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
}