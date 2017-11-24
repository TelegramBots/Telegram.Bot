using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Telegram.Bot.Types.Responses
{
    [JsonArray]
    public class JsonArrayResponse<T> : ApiResponse<IEnumerable<T>>, ICollection<T>
    {
        private readonly List<T> _list = new List<T>();

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) => _list.Add(item);

        public void Clear() => _list.Clear();

        public bool Contains(T item) => _list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public bool Remove(T item) => _list.Remove(item);

        public int Count => _list.Count;

        public bool IsReadOnly => false;
    }
}
