using System;
using UnityEngine;

public static partial class StringExtension
{
    /// <summary>
    /// 字符串转Vector2
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static Vector2 ToVector2(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FormatException(nameof(value));
        }

        var vectors = value.RemoveSubChar(LeftParenthesisChar).RemoveSubChar(RightParenthesisChar).Split(EnglishCommaChar);
        if (vectors.Length != 2)
        {
            throw new FormatException(nameof(vectors));
        }

        return new Vector2(vectors[0].ToSingle(), vectors[1].ToSingle());
    }

    /// <summary>
    /// 字符串转Vector3
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static Vector3 ToVector3(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FormatException(nameof(value));
        }

        var vectors = value.RemoveSubChar(LeftParenthesisChar).RemoveSubChar(RightParenthesisChar).Split(EnglishCommaChar);
        if (vectors.Length != 3)
        {
            throw new FormatException(nameof(vectors));
        }

        return new Vector3(vectors[0].ToSingle(), vectors[1].ToSingle(), vectors[2].ToSingle());
    }
}