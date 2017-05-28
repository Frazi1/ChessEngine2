using System;
using System.Reflection;

namespace chessengine.Extensions.EnumExtensions {
    public static class EnumExtender {
        public static string ToText(this Enum enumeration) {
            MemberInfo[] memberInfo = enumeration.GetType().GetMember(enumeration.ToString());

            if (memberInfo.Length > 0) {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(TextAttribute), false);
                if (attributes.Length > 0) {
                    return ((TextAttribute) attributes[0]).Text;
                }
                return enumeration.ToString();
            }
            return enumeration.ToString();
        }
    }
}