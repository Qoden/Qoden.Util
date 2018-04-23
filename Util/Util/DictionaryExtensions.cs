using System.Collections.Generic;

namespace Qoden.Util
{
    public static class DictionaryExtensions
    {
        public static V GetValue<K, V>(this IDictionary<K, V> dict, K key, V defaultValue = default(V))
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}
