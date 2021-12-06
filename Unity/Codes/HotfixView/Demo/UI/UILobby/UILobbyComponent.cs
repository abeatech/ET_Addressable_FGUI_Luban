
using FairyGUI;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public class UILobbyComponent : FGUIComponent
	{
		[FGUIObject]
		public GButton Btn_EnterMap;
		[FGUIObject]
		public GTextField Txt_Title;
	}

	[FGUIEvent(FGUIType.Lobby)]
	public class UILobbyEvent : FGUIEvent
    {
		public override void OnCreate(FGUIComponent self)
		{
			UILobbyComponent component = self as UILobbyComponent;
			component.BindRoot();
			component.AddButtonListener(component.Btn_EnterMap, () => component.OnEnterMap());
		}
		public override void OnShow(FGUIComponent self)
		{
			UILobbyComponent component = self as UILobbyComponent;
		}
	}


	public static class UILobbyComponentSystem
	{
		public static void OnEnterMap(this UILobbyComponent self)
		{
			EnterMapHelper.EnterMapAsync(self.ZoneScene()).Coroutine();
		}
	}
}
