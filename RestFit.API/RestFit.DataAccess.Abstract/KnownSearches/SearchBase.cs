﻿namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public abstract class SearchBase<TKey> : Dictionary<TKey, string[]> where TKey : Enum
    {
        public virtual IList<KeyValuePair<TKey, string[]>> ToKeyValuePairs() => this.Select(x => KeyValuePair.Create(x.Key, x.Value)).ToList();

        protected void SetSingle(TKey id, string? value)
        {
            if (value == null)
                return;

            this[id] = new string[] { value };
        }

        protected string GetFirst(TKey id)
        {
            if (!ContainsKey(id)) return string.Empty;
            return this[id].First();
        }
    }
}
