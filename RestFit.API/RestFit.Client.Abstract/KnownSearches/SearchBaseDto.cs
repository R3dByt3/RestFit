namespace RestFit.Client.Abstract.KnownSearches
{
    public abstract class SearchBaseDto<TKey> : Dictionary<TKey, string[]> where TKey : Enum
    {
        public virtual IList<KeyValuePair<TKey, string[]>> ToKeyValuePairs() => this.Select(x => KeyValuePair.Create(x.Key, x.Value)).ToList();

        protected void SetSingle(TKey id, string? value)
        {
            if (value == null)
                return;

            this[id] = new string[] { value };
        }

        protected string? GetFirst(TKey id)
        {
            if (!ContainsKey(id)) return null;
            return this[id].First();
        }

        protected string[] GetAll(TKey id)
        {
            if (!ContainsKey(id)) return Array.Empty<string>();
            return this[id].ToArray();
        }

        protected void SetAll(TKey id, string[] values)
        {
            if (values.Length == 0)
                return;

            this[id] = values.ToArray();
        }
    }
}
