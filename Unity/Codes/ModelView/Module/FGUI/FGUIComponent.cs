﻿using Cfg;
using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class FGUIComponent : Entity
    {
        public static FGUIComponent Instance;
        public bool InitializedBasePackages = false;
        public Dictionary<FGUIType,Type> TypeDict = new Dictionary<FGUIType, Type>();
        public Dictionary<FGUIType, FGUIEntity> UIDict = new Dictionary<FGUIType, FGUIEntity>();
        public FGUIEventComponent Event => Parent.GetComponent<FGUIEventComponent>();
    }
}
