using CommandLine;
using System;
using System.Collections.Generic;

namespace ET
{
    public enum AppType
    {
        Server,
        Robot,
        Watcher, // 每台物理机一个守护进程，用来启动该物理机上的所有进程
        GameTool,
        Proto2CS
    }
    
    public class Options
    {
        public static Options Instance { get; set; }
        
        [Option("AppType", Required = false, Default = AppType.Server, HelpText = "serverType enum")]
        public AppType AppType { get; set; }

        [Option("Process", Required = false, Default = 1)]
        public int Process { get; set; } = 1;
        
        [Option("Develop", Required = false, Default = 0, HelpText = "develop mode, 0正式 1开发 2压测")]
        public int Develop { get; set; } = 0;

        [Option("LogLevel", Required = false, Default = 2)]
        public int LogLevel { get; set; } = 2;
        
        [Option("Console", Required = false, Default = 0)]
        public int Console { get; set; } = 0;
        
        // 进程启动是否创建该进程的scenes
        [Option("CreateScenes", Required = false, Default = 1)]
        public int CreateScenes { get; set; } = 1;
    }
}