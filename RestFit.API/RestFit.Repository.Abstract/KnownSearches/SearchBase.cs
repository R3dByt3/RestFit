namespace RestFit.Repository.Abstract.KnownSearches
{
    public abstract class SearchBase<TKey> : Dictionary<TKey, string[]> where TKey : notnull
    {
        public virtual IList<KeyValuePair<TKey, string[]>> ToKeyValuePairs() => this.Select(x => KeyValuePair.Create(x.Key, x.Value)).ToList();

        protected void SetSingle(TKey id, string value)
        {
            this[id] = new string[] { value };
        }

        protected string GetFirst(TKey id)
        {
            if (!ContainsKey(id)) throw new ArgumentOutOfRangeException(nameof(id));
            return this[id].First();
        }
    }
}
