
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;



namespace Cfg.Demo
{

public sealed class AIMetas :  Bright.Config.BeanBase 
{
    public AIMetas(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);Metas = new System.Collections.Generic.List<Demo.AIMeta>(n);for(var i = 0 ; i < n ; i++) { Demo.AIMeta _e;  _e = Demo.AIMeta.DeserializeAIMeta(_buf); Metas.Add(_e);}}
    }

    public static AIMetas DeserializeAIMetas(ByteBuf _buf)
    {
        return new Demo.AIMetas(_buf);
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// AIMeta的数据列表
    /// </summary>
    public System.Collections.Generic.List<Demo.AIMeta> Metas { get; private set; }

    public const int __ID__ = -1337632037;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var _e in Metas) { _e?.Resolve(_tables); }
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in Metas) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Metas:" + Bright.Common.StringUtil.CollectionToString(Metas) + ","
        + "}";
    }
    }

}
