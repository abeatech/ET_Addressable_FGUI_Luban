

namespace ET
{
	public class LoginFinish_CreateLobbyUI: AEvent<EventType.LoginFinish>
	{
		protected override async ETTask Run(EventType.LoginFinish args)
		{
			FGUI.Instance.Close(FGUIType.Login);
			await FGUI.Instance.OpenAysnc(FGUIType.Lobby);
		}
	}
}
