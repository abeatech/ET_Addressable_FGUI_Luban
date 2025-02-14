
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;

namespace Cfg.Fgui
{
   
public sealed class TbFguiConfig
{
    private readonly Dictionary<FGUIType, Fgui.FguiConfig> _dataMap;
    private readonly List<Fgui.FguiConfig> _dataList;
    
    public TbFguiConfig(ByteBuf _buf)
    {
        _dataMap = new Dictionary<FGUIType, Fgui.FguiConfig>();
        _dataList = new List<Fgui.FguiConfig>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            Fgui.FguiConfig _v;
            _v = Fgui.FguiConfig.DeserializeFguiConfig(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public Dictionary<FGUIType, Fgui.FguiConfig> DataMap => _dataMap;
    public List<Fgui.FguiConfig> DataList => _dataList;

    public Fgui.FguiConfig GetOrDefault(FGUIType key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Fgui.FguiConfig Get(FGUIType key) => _dataMap[key];
    public Fgui.FguiConfig this[FGUIType key] => _dataMap[key];

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