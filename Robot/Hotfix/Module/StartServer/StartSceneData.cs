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
        public Cfg.StartServer.StartScene Meta { get; }
        public IPEndPoint InnerIPOutPort { get; }
        public IPEndPoint OuterIPPort { get; }
        public long InstanceId { get; }

        public StartProcessData StartProcessData { get; }
        public Cfg.StartServer.StartZone StartZone { get; }
        public StartSceneData(Cfg.StartServer.StartScene meta)
        {
            this.Meta = meta;
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(meta.Process, (uint)meta.Id);
            this.InstanceId = instanceIdStruct.ToLong();
            this.StartProcessData = StartServerComponent.Instance.GetProcessDataById(meta.Process);
            this.StartZone = ConfigUtil.Tables.TbStartZone.Get(meta.Zone);
            this.InnerIPOutPort  = NetworkHelper.ToIPEndPoint($"{this.StartProcessData.InnerIP}:{meta.OuterPort}");
            this.OuterIPPort  = NetworkHelper.ToIPEndPoint($"{this.StartProcessData.OuterIP}:{meta.OuterPort}");
        }
    }
}
