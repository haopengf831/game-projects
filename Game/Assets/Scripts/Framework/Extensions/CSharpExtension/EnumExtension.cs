using System;
using System.ComponentModel;

public static class EnumExtension
{
    /// <summary>
    /// 获取枚举类型的描述
    /// </summary>
    /// <param name="enumeration"></param>
    /// <returns></returns>
    public static string ToDescription(this Enum enumeration)
    {
        var type = enumeration.GetType();
        var memInfo = type.GetMember(enumeration.ToString());
        if (memInfo.Length > 0)
        {
            var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute) attrs[0]).Description;
            }
        }

        return enumeration.ToString();
    }
}