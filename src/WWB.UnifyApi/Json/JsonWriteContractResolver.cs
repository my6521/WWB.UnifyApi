using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WWB.UnifyApi.Json
{
    /// <summary>
    /// 反序列化
    /// </summary>
    public class JsonWriteResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var attr = member.GetCustomAttribute(typeof(JsonWriteAttribute)) as JsonWriteAttribute;
            if (attr != null)
            {
                property.PropertyName = attr.Name ?? property.PropertyName;
                property.Writable = attr.Writable;
                property.DefaultValue = attr.DefaultValue;
            }

            return property;
        }
    }
}