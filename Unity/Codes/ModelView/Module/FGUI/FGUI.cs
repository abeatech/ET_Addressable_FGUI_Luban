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
    public class FGUI : Entity
    {
        public static FGUI Instance;
        public bool InitializedBasePackages = false;
        public Dictionary<FGUIType, FGUIEntity> UIDict = new Dictionary<FGUIType, FGUIEntity>();
        public FGUIEventComponent Event => GetComponent<FGUIEventComponent>();
    }
}
