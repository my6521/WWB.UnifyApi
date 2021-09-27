using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WWB.UnifyApi
{
    public static class Extensions
    {
        //缓存
        private static Dictionary<Enum, Tuple<int, string>> caches = new Dictionary<Enum, Tuple<int, string>>();

        public static Tuple<int, string> HandleCode(this Enum errorCode)
        {
            if (caches.ContainsKey(errorCode))
            {
                return caches[errorCode];
            }

            var name = errorCode.ToString();
            var type = errorCode.GetType();
            var fieldInfo = type.GetField(name);
            var value = ((int)type.InvokeMember(name, BindingFlags.GetField, null, null, null));

            object[] objs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
                return new Tuple<int, string>(value, name);

            var descriptionAttr = (DescriptionAttribute)objs[0];

            caches[errorCode] = new Tuple<int, string>(value, descriptionAttr.Description);

            return caches[errorCode];
        }
    }
}