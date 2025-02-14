
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;

namespace Cfg.StartServer
{
   
public sealed class TbStartScene
{
    private readonly Dictionary<int, StartServer.StartScene> _dataMap;
    private readonly List<StartServer.StartScene> _dataList;
    
    public TbStartScene(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, StartServer.StartScene>();
        _dataList = new List<StartServer.StartScene>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            StartServer.StartScene _v;
            _v = StartServer.StartScene.DeserializeStartScene(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public Dictionary<int, StartServer.StartScene> DataMap => _dataMap;
    public List<StartServer.StartScene> DataList => _dataList;

    public StartServer.StartScene GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public StartServer.StartScene Get(int key) => _dataMap[key];
    public StartServer.StartScene this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
}

}