using Newtonsoft.Json.Converters;

namespace WWB.UnifyApi.Converter
{
    /// <summary>
    /// 日期格式化
    /// </summary>
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}