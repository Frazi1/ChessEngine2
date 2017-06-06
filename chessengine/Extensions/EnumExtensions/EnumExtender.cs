using System;
using System.Reflection;

namespace chessengine.Extensions.EnumExtensions {
    public static class EnumExtender {
        public static string ToText(this Enum enumeration) {
            MemberInfo[] memberInfo = enumeration.GetType().GetMember(enumeration.ToString());

            if (memberInfo.Length <= 0) return enumeration.ToString();
            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(TextAttribute), false);
            return attributes.Length > 0
                ? ((TextAttribute)attributes[0]).Text
                : enumeration.ToString();
        }

        public static int GetValue(this Enum enumeration) {
            MemberInfo[] memberInfo = enumeration.GetType().GetMember(enumeration.ToString());
            if (memberInfo.Length <= 0) throw new Exception(
                string.Format("{0} не содержит значения {1}", enumeration, "ValueAttribute"));
            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(ValueAttribute), false);
            if (attributes.Length <= 0) throw new Exception(
                string.Format("{0} не содержит значения {1}", enumeration, "ValueAttribute"));
            return ((ValueAttribute)attributes[0]).Value;
        }
    }
}