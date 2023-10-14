using Newtonsoft.Json.Converters;

namespace WWB.UnifyApi.Converter
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}