﻿using Cfg;

namespace ET
{
	public class EnterMapFinish_RemoveLobbyUI: AEvent<EventType.EnterMapFinish>
	{
		protected override async ETTask Run(EventType.EnterMapFinish args)
		{
			// 切换到map场景
			SceneChangeComponent sceneChangeComponent = null;
            try
            {
				sceneChangeComponent = Game.Scene.AddComponent<SceneChangeComponent>();
				await sceneChangeComponent.ChangeSceneAsync("Map");
			}
            finally
            {
				sceneChangeComponent.Dispose();
			}
            args.ZoneScene.AddComponent<OperaComponent>();
			FGUIComponent.Instance.Close(FGUIType.Lobby);
			FGUIComponent.Instance.Close(FGUIType.Background);
		}
	}
}
