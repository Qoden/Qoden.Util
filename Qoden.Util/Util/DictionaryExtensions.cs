using System;
using System.Collections.Generic;

namespace Qoden.Util
{
    public static class DictionaryExtensions
    {
        public static V GetValue<K, V>(this IDictionary<K, V> dict, K key, V defaultValue = default(V))
        {
            V val = defaultValue;
            dict.TryGetValue(key, out val);
            return val;
        }
    }
}
