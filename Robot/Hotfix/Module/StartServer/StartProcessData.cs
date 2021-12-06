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
        public Cfg.StartServer.StartProcess Meta { get; }
        public IPEndPoint InnerIPPort { get; }

        public long SceneId { get; }

        public string InnerIP { get; }

        public string OuterIP { get; }

        public Cfg.StartServer.StartMachine StartMachine { get; }
        public StartProcessData(Cfg.StartServer.StartProcess meta)
        {
            this.Meta = meta;
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct((int)meta.Id, 0);
            this.SceneId = instanceIdStruct.ToLong();
            StartMachine = ConfigUtil.Tables.TbStartMachine.Get(Meta.MachineId);
            this.OuterIP = this.StartMachine.OuterIp;
            this.InnerIP = this.StartMachine.InnerIp;
            this.InnerIPPort = NetworkHelper.ToIPEndPoint($"{this.InnerIP}:{Meta.InnerPort}");
            Log.Info($"StartProcess info: {meta.MachineId} {meta.Id} {this.SceneId}");
        }
    }
}
