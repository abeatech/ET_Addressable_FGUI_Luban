using Bright.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public class AppStart_Init : AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();

            // 加载配置
            Game.Scene.AddComponent<AddressableComponent>();
            Game.Scene.AddComponent<ConfigComponent>();
            await ConfigComponent.Instance.LoadAsync(ByteBufLoader);

            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();

            Game.Scene.AddComponent<NetThreadComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();

            Game.Scene.AddComponent<GlobalComponent>();

            Game.Scene.AddComponent<AIDispatcherComponent>();
            Scene zoneScene = await SceneFactory.CreateZoneScene(1, "Game", Game.Scene);
            await Game.EventSystem.Publish(new EventType.AppStartInitFinish() { ZoneScene = zoneScene });
        }
        private ByteBuf ByteBufLoader(string file)
        {
            TextAsset config = AddressableComponent.Instance.LoadAssetByPath<TextAsset>(file);
            return new ByteBuf(config.bytes);
        }
    }
}
