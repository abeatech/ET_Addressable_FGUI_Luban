using Cfg.StartServer;
using System.Collections.Generic;

namespace ET
{
    public class StartServerComponentAwakeSystem : AwakeSystem<StartServerComponent>
    {
        public override void Awake(StartServerComponent self)
        {
            StartServerComponent.Instance = self;
        }
    }
    public static class StartServerComponentSystem
    {
        public static void Initialize(this StartServerComponent self)
        {
            foreach (var startSceneC in ConfigUtil.Tables.TbStartScene.DataList)
            {
                StartSceneData sceneData = StartServerHelper.CreateStartSceneData(startSceneC);
                self.ProcessScenes.Add(sceneData.Meta.Process, sceneData);

                if (!self.ZoneScenesByName.ContainsKey(sceneData.Meta.Zone))
                {
                    self.ZoneScenesByName.Add(sceneData.Meta.Zone, new Dictionary<string, StartSceneData>());
                }
                self.ZoneScenesByName[startSceneC.Zone].Add(startSceneC.Name, sceneData);

                switch (startSceneC.SceneType)
                {
                    case SceneType.Gate:
                        self.Gates.Add(startSceneC.Zone, sceneData);
                        break;
                    case SceneType.Location:
                        self.LocationConfig = sceneData;
                        break;
                    case SceneType.Robot:
                        self.Robots.Add(sceneData);
                        break;
                }
            }
        }
        public static List<StartSceneData> GetByProcess(this StartServerComponent self, int process)
        {
            return self.ProcessScenes[process];
        }

        public static StartSceneData GetBySceneName(this StartServerComponent self, int zone, string name)
        {
            return self.ZoneScenesByName[zone][name];
        }

        public static StartProcessData GetProcessDataById(this StartServerComponent self, int id)
        {
            self.ProcessDatas.TryGetValue(id, out var data);
            if(data == null)
            {
                data = StartServerHelper.CreateStartProcessData(ConfigUtil.Tables.TbStartProcess.Get(id));
                self.ProcessDatas[id] = data;
            }
            return data;
        }
        public static StartSceneData GetSceneDataById(this StartServerComponent self, int id)
        {
            self.SceneDatas.TryGetValue(id, out var data);
            if(data == null)
            {
                data = StartServerHelper.CreateStartSceneData(ConfigUtil.Tables.TbStartScene.Get(id));
                self.SceneDatas[id] = data;
            }
            return data;
        }
    }
}
