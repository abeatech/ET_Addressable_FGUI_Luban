using Cfg.StartServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class StartServerComponent : Entity
    {
        public static StartServerComponent Instance;

        public MultiMap<int, StartSceneData> Gates = new MultiMap<int, StartSceneData>();
        
        public MultiMap<int, StartSceneData> ProcessScenes = new MultiMap<int, StartSceneData>();
        
        public Dictionary<long, Dictionary<string, StartSceneData>> ZoneScenesByName = new Dictionary<long, Dictionary<string, StartSceneData>>();

        public StartSceneData LocationConfig;
        
        public List<StartSceneData> Robots = new List<StartSceneData>();

        public Dictionary<int,StartProcessData> ProcessDatas = new Dictionary<int, StartProcessData>();
        public Dictionary<int,StartSceneData> SceneDatas = new Dictionary<int, StartSceneData>();
    }
}
