using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class StartProcessData
    {
        public Cfg.StartServer.StartProcess Meta { get; set; }
        public IPEndPoint InnerIPPort { get; set; }

        public long SceneId { get; set; }

        public string InnerIP { get; set; }

        public string OuterIP { get; set; }

        public Cfg.StartServer.StartMachine StartMachine { get; set; }
    }
}
