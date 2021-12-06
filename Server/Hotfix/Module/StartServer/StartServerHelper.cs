using Cfg.StartServer;

namespace ET
{
    public static class StartServerHelper
    {
        public static StartSceneData CreateStartSceneData(Cfg.StartServer.StartScene meta)
        {
            StartSceneData data = new StartSceneData();
            data.Meta = meta;
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(meta.Process, (uint)meta.Id);
            data.InstanceId = instanceIdStruct.ToLong();
            data.StartProcessData = StartServerComponent.Instance.GetProcessDataById(meta.Process);
            data.StartZone = ConfigUtil.Tables.TbStartZone.Get(meta.Zone);
            data.InnerIPOutPort = NetworkHelper.ToIPEndPoint($"{data.StartProcessData.InnerIP}:{meta.OuterPort}");
            data.OuterIPPort = NetworkHelper.ToIPEndPoint($"{data.StartProcessData.OuterIP}:{meta.OuterPort}");
            return data;
        }
        public static StartProcessData CreateStartProcessData(Cfg.StartServer.StartProcess meta)
        {
            StartProcessData data = new StartProcessData();
            data.Meta = meta;
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct((int)meta.Id, 0);
            data.SceneId = instanceIdStruct.ToLong();
            data.StartMachine = ConfigUtil.Tables.TbStartMachine.Get(meta.MachineId);
            data.OuterIP = data.StartMachine.OuterIp;
            data.InnerIP = data.StartMachine.InnerIp;
            data.InnerIPPort = NetworkHelper.ToIPEndPoint($"{data.InnerIP}:{meta.InnerPort}");
            Log.Info($"StartProcess info: {meta.MachineId} {meta.Id} {data.SceneId}");
            return data;
        }
    }
}
