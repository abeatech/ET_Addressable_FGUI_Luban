﻿using Cfg;

namespace ET
{
    public static class UILobbyComponentSystem
	{
		public static void OnEnterMap(this UILobbyComponent self)
		{
			EnterMapHelper.EnterMapAsync(self.ZoneScene()).Coroutine();
		}
	}

	[FGUIEvent(FGUIType.Lobby)]
	public class UILobbyEvent : FGUIEvent<UILobbyComponent>
	{
		public override void OnCreate(UILobbyComponent component)
		{
			FGUIHelper.AddButtonListener(component.Btn_EnterMap, () => component.OnEnterMap());
		}
		public override void OnShow(UILobbyComponent self)
		{
		}
        public override void OnRefresh(UILobbyComponent self)
        {
		}
		public override void OnHide(UILobbyComponent self)
		{
		}
		public override void OnDestroy(UILobbyComponent self)
		{
		}

	}
}
