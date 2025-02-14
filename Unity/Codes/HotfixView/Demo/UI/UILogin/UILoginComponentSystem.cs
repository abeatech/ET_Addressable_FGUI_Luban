﻿using Cfg;
using FairyGUI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ET
{
    public static class UILoginComponentSystem
    {
        public static void BtnLoginOnClick(this UILoginComponent self)
        {
            LoginHelper.Login(
                self.DomainScene(),
                ConstValue.LoginAddress,
                self.Input_Account.text,
                self.Input_Password.text).Coroutine();
        }
        public static void BtnRegisterOnClick(this UILoginComponent self)
        {
            //TODO 注册账号
        }
        public static void InputPasswordOnChange(this UILoginComponent self, EventContext context)
        {
            GTextInput input = context.sender as GTextInput;
            input.text = self.AdjustStr(input.text);
        }
        public static GTextInput GetFocus(this UILoginComponent self)
        {
            string page = self.Ctrl_Page.selectedPage;
            if (page == "Login")
            {
                if (self.Input_Account.focused) return self.Input_Account;
                if (self.Input_Password.focused) return self.Input_Password;
                return null;
            }
            else
            {
                if (self.Input_RegAccount.focused) return self.Input_RegAccount;
                if (self.Input_RegPassword.focused) return self.Input_RegPassword;
                if (self.Input_RegPassword2.focused) return self.Input_RegPassword2;
                return null;
            }
        }
        public static string AdjustStr(this UILoginComponent self, string str)
        {
            string result = str.Trim();
            if(!Regex.IsMatch(result, "(?=.*([a-zA-Z].*))(?=.*[0-9].*)[a-zA-Z0-9-*/+.~!@#$%^&*()]{6,20}$"))
            {
                self.Txt_Tips.text = "[color=#FF0000]不符合规则[/color]";
            }
            else
            {
                self.Txt_Tips.text = "";
            }
            return result;
        }
    }

    [FGUIEvent(FGUIType.Login)]
    public class UILoginEvent : FGUIEvent<UILoginComponent>
    {
        public override void OnCreate(UILoginComponent component)
        {
            FGUIHelper.AddButtonListener(component.Btn_Login, () => component.BtnLoginOnClick());
            FGUIHelper.AddButtonListener(component.Btn_ToRegister, () => { component.Ctrl_Page.selectedPage = "Register"; });
            FGUIHelper.AddButtonListener(component.Btn_Register, () => component.BtnRegisterOnClick());
            FGUIHelper.AddButtonListener(component.Btn_ToLogin, () => { component.Ctrl_Page.selectedPage = "Login"; });

            component.inputTabSkipDict = new Dictionary<GTextInput, GTextInput>();
            component.inputTabSkipDict.Add(component.Input_Account, component.Input_Password);
            component.inputTabSkipDict.Add(component.Input_Password, component.Input_Account);
            component.inputTabSkipDict.Add(component.Input_RegAccount, component.Input_RegPassword);
            component.inputTabSkipDict.Add(component.Input_RegPassword, component.Input_RegPassword2);
            component.inputTabSkipDict.Add(component.Input_RegPassword2, component.Input_RegAccount);
            component.Input_Account.onChanged.Add((on) => component.InputPasswordOnChange(on));
            component.Input_Password.onChanged.Add((on) => component.InputPasswordOnChange(on));
            component.Input_RegAccount.onChanged.Add((on) => component.InputPasswordOnChange(on));
            component.Input_RegPassword.onChanged.Add((on) => component.InputPasswordOnChange(on));
            component.Input_RegPassword2.onChanged.Add((on) => component.InputPasswordOnChange(on));
        }
        public override void OnShow(UILoginComponent component)
        {
            component.Txt_Tips.text = "鹅鹅鹅鹅鹅鹅饿";
        }
        public override void OnRefresh(UILoginComponent self)
        {
        }
        public override void OnHide(UILoginComponent self)
        {
        }
        public override void OnDestroy(UILoginComponent self)
        {
        }
    }

    public class UILoginComponentUpdateSystem : UpdateSystem<UILoginComponent>
    {
        public override void Update(UILoginComponent self)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                string page = self.Ctrl_Page.selectedPage;
                GTextInput curFocus = self.GetFocus();
                GTextInput nextFocus = null;
                if (curFocus == null)
                {
                    nextFocus = page == "Login" ? self.Input_Account : self.Input_RegAccount;
                }
                else
                {
                    self.inputTabSkipDict.TryGetValue(curFocus, out nextFocus);
                }
                if (nextFocus != null)
                {
                    nextFocus.RequestFocus();
                }
            }
            self.Txt_Tips.text = "测试重重载";
        }
    }
}
