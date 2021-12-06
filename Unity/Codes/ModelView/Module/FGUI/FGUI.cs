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
    }
}
