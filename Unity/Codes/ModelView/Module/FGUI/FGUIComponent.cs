using Cfg;
using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public enum FGUILayer
    {
        Background,
        Fixed,
        Window,
        Tips,
        Loading,
    }
    public class FGUIComponent : Entity
    {
        public static FGUIComponent Instance;
        public bool InitializedBasePackages = false;
        public Dictionary<FGUIType, FGUIEntity> UIDict = new Dictionary<FGUIType, FGUIEntity>();
        public FGUIEventComponent Event => Parent.GetComponent<FGUIEventComponent>();
    }
}
