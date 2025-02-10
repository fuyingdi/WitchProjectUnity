using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;
using System.Linq;

public class BiDictionary<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> _forward = new();
    private readonly Dictionary<TValue, TKey> _reverse = new();

    public void Add(TKey key, TValue value)
    {
        if (_forward.ContainsKey(key) || _reverse.ContainsKey(value))
            throw new ArgumentException("键值对已存在");

        _forward.Add(key, value);
        _reverse.Add(value, key);
    }

    public bool TryGetValue(TKey key, out TValue value) => _forward.TryGetValue(key, out value);
    public bool TryGetKey(TValue value, out TKey key) => _reverse.TryGetValue(value, out key);

    public TValue this[TKey key] => _forward[key];
    public TKey this[TValue val] => _reverse[val];
}
