using Bright.Serialization;
using System;
using System.IO;
using System.Net;

namespace ET
{
    public class AppStart_Init : AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            Game.Scene.AddComponent<ConfigComponent>();
            await ConfigComponent.Instance.LoadAsync((file) =>
            {
                return new ByteBuf(File.ReadAllBytes($"../Server/ConfigBin/{file}.bin"));
            });
            Game.Scene.AddComponent<StartServerComponent>();
            StartServerComponent.Instance.Initialize();
            StartProcessData processConfig = StartServerComponent.Instance.GetProcessDataById(Game.Options.Process);

            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<CoroutineLockComponent>();
            // 发送普通actor消息
            Game.Scene.AddComponent<ActorMessageSenderComponent>();
            // 发送location actor消息
            Game.Scene.AddComponent<ActorLocationSenderComponent>();
            // 访问location server的组件
            Game.Scene.AddComponent<LocationProxyComponent>();
            Game.Scene.AddComponent<ActorMessageDispatcherComponent>();
            // 数值订阅组件
            Game.Scene.AddComponent<NumericWatcherComponent>();

            Game.Scene.AddComponent<NetThreadComponent>();

            switch (Game.Options.AppType)
            {
                case AppType.Server:
                    {
                        Game.Scene.AddComponent<NetInnerComponent, IPEndPoint, int>(processConfig.InnerIPPort, SessionStreamDispatcherType.SessionStreamDispatcherServerInner);

                        var processScenes = StartServerComponent.Instance.GetByProcess(Game.Options.Process);
                        foreach (StartSceneData startConfig in processScenes)
                        {
                            await SceneFactory.Create(Game.Scene, startConfig.Meta.Id, startConfig.InstanceId, startConfig.Meta.Zone, startConfig.Meta.Name,
                                startConfig.Meta.SceneType, startConfig);
                        }

                        break;
                    }
                case AppType.Watcher:
                    {
                        Cfg.StartServer.StartMachine startMachineConfig = WatcherHelper.GetThisMachineConfig();
                        WatcherComponent watcherComponent = Game.Scene.AddComponent<WatcherComponent>();
                        watcherComponent.Start(Game.Options.CreateScenes);
                        Game.Scene.AddComponent<NetInnerComponent, IPEndPoint, int>(NetworkHelper.ToIPEndPoint($"{startMachineConfig.InnerIp}:{startMachineConfig.WatcherPort}"), SessionStreamDispatcherType.SessionStreamDispatcherServerInner);
                        break;
                    }
                case AppType.GameTool:
                    break;
            }

            if (Game.Options.Console == 1)
            {
                Game.Scene.AddComponent<ConsoleComponent>();
            }
        }
    }
}