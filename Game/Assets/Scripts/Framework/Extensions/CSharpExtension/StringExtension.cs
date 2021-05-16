using System;
using System.Text;
using System.Text.RegularExpressions;
using log4net.Util.TypeConverters;

public static partial class StringExtension
{
    #region Constant

    public const char EnglishCommaChar = ',';
    public const string EnglishCommaString = ",";
    public const char SpaceChar = ' ';
    public const string SpaceString = " ";
    public const char LeftParenthesisChar = '(';
    public const string LeftParenthesisString = "(";
    public const char RightParenthesisChar = ')';
    public const string RightParenthesisString = ")";
    public const char LeftBracketChar = '[';
    public const string LeftBracketString = "[";
    public const char RightBracketChar = ']';
    public const string RightBracketString = "]";
    public const char LeftBraceChar = '{';
    public const string LeftBraceString = "{";
    public const char RightBraceChar = '}';
    public const string RightBraceString = "}";

    #endregion

    /// <summary>
    /// 去除字符串中任何空白字符，包括空格，制表符，换页符等，与[\f\n\t\r\v]等效
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string TrimAll(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return Regex.Replace(value, @"\s", "");
    }

    /// <summary>
    /// 移除指定字符
    /// </summary>
    /// <param name="value"></param>
    /// <param name="needBeRemovedChar"></param>
    /// <returns></returns>
    public static string RemoveSubChar(this string value, char needBeRemovedChar)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        value = value.Replace(needBeRemovedChar, SpaceChar);
        value = value.TrimAll();
        return value;
    }

    /// <summary>
    /// 移除指定字符串
    /// </summary>
    /// <param name="value"></param>
    /// <param name="needBeRemovedString"></param>
    /// <returns></returns>
    public static string RemoveSubString(this string value, string needBeRemovedString)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        value = value.Replace(needBeRemovedString, SpaceString);
        value = value.TrimAll();
        return value;
    }

    /// <summary>
    /// 在字符串最后追加指定的字符
    /// </summary>
    /// <param name="value"></param>
    /// <param name="appendValue"></param>
    /// <returns></returns>
    public static string Append(this string value, char appendValue)
    {
        return value?.Insert(value.Length, appendValue.ToString());
    }

    /// <summary>
    /// 在字符串最后追加指定的字符串
    /// </summary>
    /// <param name="value"></param>
    /// <param name="appendValue"></param>
    /// <returns></returns>
    public static string Append(this string value, string appendValue)
    {
        return value?.Insert(value.Length, appendValue);
    }

    /// <summary>
    /// 将字符串编码为64位ASCII码
    /// </summary>
    /// <param name="value">String to encode.</param>
    /// <returns>Encoded string.</returns>
    public static string EncodeTo64(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var toEncodeAsBytes = Encoding.ASCII.GetBytes(value);
        return Convert.ToBase64String(toEncodeAsBytes);
    }

    /// <summary>
    /// 解码来自64位ASCII码的字符串
    /// </summary>
    /// <param name="value">String to decode.</param>
    /// <returns>Decoded string.</returns>
    public static string DecodeFrom64(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var encodedDataAsBytes = Convert.FromBase64String(value);
        return Encoding.ASCII.GetString(encodedDataAsBytes);
    }

    /// <summary>
    /// 大写第一个字符，并在每个大写字母前添加一个空格(第一个字符除外)
    /// </summary>
    public static string ToProperCase(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length < 2)
        {
            return value.ToUpper();
        }

        // If there's already spaces in the string, return.
        if (value.Contains(" "))
        {
            return value;
        }

        // Start with the first character.
        string result = value.Substring(0, 1).ToUpper();

        // Add the remaining characters.
        for (int i = 1; i < value.Length; i++)
        {
            if (char.IsLetter(value[i]) &&
                char.IsUpper(value[i]))
            {
                // Add a space if the previous character is not upper-case.
                // e.g. "LeftHand" -> "Left Hand"
                if (i != 1 && // First character is upper-case in result.
                    (!char.IsLetter(value[i - 1]) || char.IsLower(value[i - 1])))
                {
                    result += " ";
                }
                // If previous character is upper-case, only add space if the next
                // character is lower-case. Otherwise assume this character to be inside
                // an acronym.
                // e.g. "OpenVRLeftHand" -> "Open VR Left Hand"
                else
                {
                    if (i < value.Length - 1 &&
                        char.IsLetter(value[i + 1]) && char.IsLower(value[i + 1]))
                    {
                        result += " ";
                    }
                }
            }

            result += value[i];
        }

        return result;
    }

    /// <summary>
    /// 字符串转Char16(char)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static char ToChar16(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!char.TryParse(value, out var charValue))
        {
            throw new ConversionNotSupportedException();
        }

        return charValue;
    }

    /// <summary>
    /// 字符串转UInt8(byte)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static byte ToUInt8(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!byte.TryParse(value, out var byteValue))
        {
            throw new ConversionNotSupportedException();
        }

        return byteValue;
    }

    /// <summary>
    /// 字符串转UInt16(ushort)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static ushort ToUInt16(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!ushort.TryParse(value, out var ushortValue))
        {
            throw new ConversionNotSupportedException();
        }

        return ushortValue;
    }

    /// <summary>
    /// 字符串转UInt32(uint)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static uint ToUInt32(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!uint.TryParse(value, out var uintValue))
        {
            throw new ConversionNotSupportedException();
        }

        return uintValue;
    }

    /// <summary>
    /// 字符串转UInt64(ulong)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static ulong ToUInt64(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!ulong.TryParse(value, out var ulongValue))
        {
            throw new ConversionNotSupportedException();
        }

        return ulongValue;
    }

    /// <summary>
    /// 字符串转Int8(sbyte)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static sbyte ToInt8(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!sbyte.TryParse(value, out var sbyteValue))
        {
            throw new ConversionNotSupportedException();
        }

        return sbyteValue;
    }

    /// <summary>
    /// 字符串转Int16(short)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static short ToInt16(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!short.TryParse(value, out var shortValue))
        {
            throw new ConversionNotSupportedException();
        }

        return shortValue;
    }

    /// <summary>
    /// 字符串转Int32(int)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static int ToInt32(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!int.TryParse(value, out var intValue))
        {
            throw new ConversionNotSupportedException();
        }

        return intValue;
    }

    /// <summary>
    /// 字符串转Int64(long)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static long ToInt64(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!long.TryParse(value, out var longValue))
        {
            throw new ConversionNotSupportedException();
        }

        return longValue;
    }

    /// <summary>
    /// 字符串转Single(float)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static float ToSingle(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!float.TryParse(value, out var floatValue))
        {
            throw new ConversionNotSupportedException();
        }

        return floatValue;
    }

    /// <summary>
    /// 字符串转Double(double)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static double ToDouble(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!double.TryParse(value, out var doubleValue))
        {
            throw new ConversionNotSupportedException();
        }

        return doubleValue;
    }

    /// <summary>
    /// 字符串转byte[]
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static byte[] ToBytes(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return Encoding.Default.GetBytes(value);
    }
}