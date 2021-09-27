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
        public static (int, string) HandleCode(this Enum errorCode)
        {
            var name = errorCode.ToString();
            var type = errorCode.GetType();
            var fieldInfo = type.GetField(name);
            var value = ((int)type.InvokeMember(name, BindingFlags.GetField, null, null, null));

            object[] objs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
                return (value, name);

            var descriptionAttr = (DescriptionAttribute)objs[0];

            return (value, descriptionAttr.Description);
        }
    }
}