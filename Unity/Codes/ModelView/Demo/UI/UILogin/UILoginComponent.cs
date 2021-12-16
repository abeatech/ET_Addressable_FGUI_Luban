using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [FGUIComponent(Cfg.FGUIType.Login)]
    public class UILoginComponent : Entity
    {
        [FGUIObject]
        public GButton Btn_Login;
        [FGUIObject]
        public GButton Btn_ToRegister;
        [FGUIObject]
        public GTextInput Input_Account;
        [FGUIObject]
        public GTextInput Input_Password;
        [FGUIObject]
        public GTextField Txt_Tips;
        [FGUIObject]
        public GButton Btn_Register;
        [FGUIObject]
        public GButton Btn_ToLogin;
        [FGUIObject]
        public GTextInput Input_RegAccount;
        [FGUIObject]
        public GTextInput Input_RegPassword;
        [FGUIObject]
        public GTextInput Input_RegPassword2;
        [FGUIObject]
        public GTextField Txt_RegTips;
        [FGUIObject]
        public Controller Ctrl_Page;

        public Dictionary<GTextInput, GTextInput> inputTabSkipDict;
    }
}
