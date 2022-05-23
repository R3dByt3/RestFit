using System.Globalization;

namespace RestFit.DataAccess.Abstract.KnownSearches
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

        protected void SetSingle(TKey id, long? value)
        {
            if (value == null)
                return;

#pragma warning disable CS8601 // Possible null reference assignment.
            this[id] = new string[] { Convert.ToString(value) };
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        protected void SetSingle(TKey id, double? value)
        {
            if (value == null)
                return;

#pragma warning disable CS8601 // Possible null reference assignment.
            this[id] = new string[] { Convert.ToString(value) };
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        protected string GetFirst(TKey id)
        {
            if (!ContainsKey(id)) return string.Empty;
            return this[id].First();
        }

        protected long? GetFirstInt64(TKey id)
        {
            if (!ContainsKey(id)) return null;
            return Convert.ToInt64(this[id].First());
        }

        protected double? GetFirstDouble(TKey id)
        {
            if (!ContainsKey(id)) return null;
            return Convert.ToDouble(this[id].First());
        }

        protected void SetAll(TKey id, IEnumerable<string> value)
        {
            this[id] = value.ToArray();
        }

        protected string[] GetAll(TKey id)
        {
            return this[id].ToArray();
        }

        protected void SetSingle(TKey id, DateTime? value)
        {
            if (value == null || value == null)
                return;

            this[id] = new string[] { value?.ToString("O", CultureInfo.InvariantCulture)! };
        }

        protected DateTime? GetFirstDate(TKey id)
        {
            if (!ContainsKey(id)) return null;
            return DateTime.ParseExact(this[id].First(), "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        }
    }
}
