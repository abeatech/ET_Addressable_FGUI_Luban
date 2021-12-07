

using Cfg;

namespace ET
{
	public class LoginFinish_CreateLobbyUI: AEvent<EventType.LoginFinish>
	{
		protected override async ETTask Run(EventType.LoginFinish args)
		{
            Scene zoneScene = args.ZoneScene;
			FGUIComponent.Instance.Close(FGUIType.Login);
			await FGUIComponent.Instance.OpenAysnc(FGUIType.Lobby);
		}
	}
}
