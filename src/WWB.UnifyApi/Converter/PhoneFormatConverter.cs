using Newtonsoft.Json;
using System;

namespace WWB.UnifyApi.Converter
{
    /// <summary>
    /// 手机号掩码转换
    /// </summary>
    public class PhoneFormatConverter : JsonConverter
    {
        public override bool CanRead { get; } = false;

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(value?.ToString().PhoneToAnonymous());
            }
        }
    }
}