using System;
using System.Collections;
using System.Collections.Generic;

public static partial class DictionaryExtension
{
    /// <summary>
    /// 返回指定Dictionary的新示例
    /// </summary>
    /// <param name="dictionary"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        return new Dictionary<TKey, TValue>(dictionary);
    }

    /// <summary>
    /// 新增或重写(如果没有该Key值,则新增;否则重写该值)
    /// </summary>
    /// <typeparam name="TKey">Key类型</typeparam>
    /// <typeparam name="TValue">Value类型</typeparam>
    /// <param name="dictionary">字典</param>
    /// <param name="key">Key值</param>
    /// <param name="value">Value新值</param>
    /// <returns>字典</returns>
    public static IDictionary<TKey, TValue> AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
        TValue value)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        return dictionary;
    }

    /// <summary>
    /// 获取与指定的值相关联的键，如果没有则返回输入的默认值
    /// </summary>
    public static bool TryGetKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value,
        out TKey defaultKey)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        defaultKey = default;

        if (value == null)
        {
            return false;
        }

        if (!dictionary.Values.Contains(value))
        {
            return false;
        }

        foreach (var pair in dictionary)
        {
            if (pair.Value.Equals(value))
            {
                defaultKey = pair.Key;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 向字典中批量添加键值对
    /// </summary>
    /// <param name="dictionary">字典</param>
    /// /// <param name="values">要添加的键值对</param>
    /// <param name="replaceExisted">如果已存在，是否替换</param>
    public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
        IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        foreach (var item in values)
        {
            if (dictionary.ContainsKey(item.Key) == false || replaceExisted)
            {
                dictionary[item.Key] = item.Value;
            }
        }

        return dictionary;
    }

    /// <summary>
    /// 移除字典中带有指定值的元素
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        return dictionary.TryGetKey(value, out var key) && dictionary.Remove(key);
    }

    /// <summary>
    /// 移除字典中带有指定值的元素并返回移除后的字典
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> DirectRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
    {
        dictionary.Remove(value);
        return dictionary;
    }
}