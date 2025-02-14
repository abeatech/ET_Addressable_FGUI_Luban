using System;
using System.Collections;
using System.Diagnostics;

namespace ET
{
    public static class WatcherHelper
    {
        public static Cfg.StartServer.StartMachine GetThisMachineConfig()
        {
            string[] localIP = NetworkHelper.GetAddressIPs();
            Cfg.StartServer.StartMachine startMachineConfig = null;
            foreach (Cfg.StartServer.StartMachine config in ConfigUtil.Tables.TbStartMachine.DataList)
            {
                if (!WatcherHelper.IsThisMachine(config.InnerIp, localIP))
                {
                    continue;
                }
                startMachineConfig = config;
                break;
            }

            if (startMachineConfig == null)
            {
                throw new Exception("not found this machine ip config!");
            }

            return startMachineConfig;
        }

        public static bool IsThisMachine(string ip, string[] localIPs)
        {
            if (ip != "127.0.0.1" && ip != "0.0.0.0" && !((IList)localIPs).Contains(ip))
            {
                return false;
            }
            return true;
        }

        public static Process StartProcess(int processId, int createScenes = 0)
        {
            StartProcessData startProcessData = StartServerComponent.Instance.GetProcessDataById(processId);
            const string exe = "dotnet";
            string arguments = $"{startProcessData.Meta.AppName}.dll" +
                    $" --Process={startProcessData.Meta.Id}" +
                    $" --AppType={startProcessData.Meta.AppName}" +
                    $" --Develop={Game.Options.Develop}" +
                    $" --CreateScenes={createScenes}" +
                    $" --LogLevel={Game.Options.LogLevel}";
            Log.Debug($"{exe} {arguments}");
            Process process = ProcessHelper.Run(exe, arguments);
            return process;
        }
    }
}