using Newtonsoft.Json;
using System;

namespace WWB.UnifyApi.Converter
{
    public class DistanceFormatConverter : JsonConverter
    {
        /// <summary>
        /// 是否开启自定义反序列化，值为true时，反序列化时会走ReadJson方法，值为false时，不走ReadJson方法，而是默认的反序列化
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// 是否开启自定义序列化，值为true时，序列化时会走WriteJson方法，值为false时，不走WriteJson方法，而是默认的序列化
        /// 类型等于long时才转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long) || objectType == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false.The type will skip the converter.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            if (long.TryParse(value.ToString(), out long res))
            {
                if (res < 1000)
                {
                    writer.WriteValue($"{res}m");
                }
                else
                {
                    var sValue = (res / 1000).ToString("0.00");

                    writer.WriteValue($"{sValue}km");
                }
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}