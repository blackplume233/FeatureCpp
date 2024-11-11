using System.Collections.Generic;

namespace Ludiq
{
    public class FlexibleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }
    }
}