
using FairyGUI;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public class UILobbyComponent : UIDefaultComponent
	{
		[FGUIObject]
		public GButton Btn_EnterMap;
		[FGUIObject]
		public GTextField Txt_Title;
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
			//TODO
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
