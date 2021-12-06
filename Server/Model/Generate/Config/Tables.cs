
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;


namespace Cfg
{
   
public sealed class Tables
{
    public Global.TbGlobal TbGlobal {get; }
    public StartServer.TbStartMachine TbStartMachine {get; }
    public StartServer.TbStartProcess TbStartProcess {get; }
    public StartServer.TbStartScene TbStartScene {get; }
    public StartServer.TbStartZone TbStartZone {get; }
    public Demo.TbAIMetas TbAIMetas {get; }
    public Demo.TbUnitMeta TbUnitMeta {get; }

    public Tables(System.Func<string, ByteBuf> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        TbGlobal = new Global.TbGlobal(loader("global_tbglobal")); 
        tables.Add("Global.TbGlobal", TbGlobal);
        TbStartMachine = new StartServer.TbStartMachine(loader("startserver_tbstartmachine")); 
        tables.Add("StartServer.TbStartMachine", TbStartMachine);
        TbStartProcess = new StartServer.TbStartProcess(loader("startserver_tbstartprocess")); 
        tables.Add("StartServer.TbStartProcess", TbStartProcess);
        TbStartScene = new StartServer.TbStartScene(loader("startserver_tbstartscene")); 
        tables.Add("StartServer.TbStartScene", TbStartScene);
        TbStartZone = new StartServer.TbStartZone(loader("startserver_tbstartzone")); 
        tables.Add("StartServer.TbStartZone", TbStartZone);
        TbAIMetas = new Demo.TbAIMetas(loader("demo_tbaimetas")); 
        tables.Add("Demo.TbAIMetas", TbAIMetas);
        TbUnitMeta = new Demo.TbUnitMeta(loader("demo_tbunitmeta")); 
        tables.Add("Demo.TbUnitMeta", TbUnitMeta);

        TbGlobal.Resolve(tables); 
        TbStartMachine.Resolve(tables); 
        TbStartProcess.Resolve(tables); 
        TbStartScene.Resolve(tables); 
        TbStartZone.Resolve(tables); 
        TbAIMetas.Resolve(tables); 
        TbUnitMeta.Resolve(tables); 
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        TbGlobal.TranslateText(translator); 
        TbStartMachine.TranslateText(translator); 
        TbStartProcess.TranslateText(translator); 
        TbStartScene.TranslateText(translator); 
        TbStartZone.TranslateText(translator); 
        TbAIMetas.TranslateText(translator); 
        TbUnitMeta.TranslateText(translator); 
    }
}

}