using System;
using System.Reflection;

namespace chessengine.Extensions.EnumExtensions {
    public static class EnumExtender {
        public static string ToText(this Enum enumeration) {
            MemberInfo[] memberInfo = enumeration.GetType().GetMember(enumeration.ToString());

            if (memberInfo.Length <= 0) return enumeration.ToString();
            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(TextAttribute), false);
            return attributes.Length > 0 
                ? ((TextAttribute) attributes[0]).Text
                : enumeration.ToString();
        }
    }
}