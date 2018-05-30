using System;
using System.Linq;
using System.Runtime.Serialization;

namespace TeslaLib
{
    public static class Extensions
    {
        public static string GetEnumValue(this Enum enumValue)
        {
            var attr = enumValue.GetType().GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(EnumMemberAttribute), false);

            return attr.Length > 0 
                ? ((EnumMemberAttribute)attr[0]).Value 
                : enumValue.ToString();
        }

        public static T ToEnum<T>(string str)
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();

                if (enumMemberAttribute.Value == str)
                {
                    return (T)Enum.Parse(enumType, name);
                }
            }

            return default(T);
        }
    }
}
