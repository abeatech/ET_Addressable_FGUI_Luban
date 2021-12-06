using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class StartSceneData
    {
        public Cfg.StartServer.StartScene Meta { get; set; }
        public IPEndPoint InnerIPOutPort { get; set; }
        public IPEndPoint OuterIPPort { get; set; }
        public long InstanceId { get; set; }

        public StartProcessData StartProcessData { get; set; }
        public Cfg.StartServer.StartZone StartZone { get; set; }
    }
}
