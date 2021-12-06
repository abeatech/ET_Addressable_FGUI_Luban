using Bright.Serialization;
using System.IO;

namespace ET
{
    public class AppStart_Init: AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();

            // 加载配置
            Game.Scene.AddComponent<ConfigComponent>();
            await ConfigComponent.Instance.LoadAsync((file) =>
            {
                return new ByteBuf(File.ReadAllBytes($"../Server/ConfigBin/{file}.bin"));
            });
            
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<NetThreadComponent>();
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            Game.Scene.AddComponent<AIDispatcherComponent>();
            Game.Scene.AddComponent<RobotCaseDispatcherComponent>();
            Game.Scene.AddComponent<RobotCaseComponent>();
            
            var processScenes = StartServerComponent.Instance.GetByProcess(Game.Options.Process);
            foreach (StartSceneData startConfig in processScenes)
            {
                await RobotSceneFactory.Create(Game.Scene, startConfig.Meta.Id, startConfig.InstanceId, startConfig.Meta.Zone, startConfig.Meta.Name, startConfig.Meta.SceneType, startConfig);
            }
            
            if (Game.Options.Console == 1)
            {
                Game.Scene.AddComponent<ConsoleComponent>();
            }
        }
    }
}
