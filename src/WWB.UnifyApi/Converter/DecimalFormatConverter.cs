using Newtonsoft.Json;
using System;

namespace WWB.UnifyApi.Converter
{
    public class DecimalFormatConverter : JsonConverter
    {
        private readonly string _format;

        public DecimalFormatConverter(string format)
        {
            _format = format;
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
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
            if (decimal.TryParse(value.ToString(), out decimal res))
            {
                writer.WriteValue(res.ToString(_format));
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}