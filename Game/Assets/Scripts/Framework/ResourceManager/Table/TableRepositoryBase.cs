using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class TableRepositoryBase<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    protected Dictionary<TKey, TValue> TableDictionary = new Dictionary<TKey, TValue>();
    public string TableKey { get; protected set; } = string.Empty;

    public TValue this[TKey key] => TableDictionary.TryGetValue(key, out TValue value) ? value : default;

    public Dictionary<TKey, TValue>.ValueCollection Values => TableDictionary.Values;

    public Dictionary<TKey, TValue>.KeyCollection Keys => TableDictionary.Keys;
    public int Count => TableDictionary.Count;
    public IEqualityComparer<TKey> Comparer => TableDictionary.Comparer;

    public bool ContainsKey(TKey key)
    {
        return TableDictionary.ContainsKey(key);
    }

    public bool ContainsValue(TValue value)
    {
        return TableDictionary.ContainsValue(value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return TableDictionary.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return TableDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract AsyncOperationHandle<TextAsset> LoadTable(string tableKey);
}