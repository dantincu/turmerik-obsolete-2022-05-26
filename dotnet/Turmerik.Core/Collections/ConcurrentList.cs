using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Threading;

namespace Turmerik.Core.Collections
{
    public class ConcurrentList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, IList
    {
        private readonly List<T> list;
        private readonly ISynchronizerComponent synchronizer;

        public ConcurrentList(ISynchronizerFactory synchronizerFactory)
        {
            list = new List<T>();
            var syncRoot = ((ICollection)list).SyncRoot;

            synchronizer = synchronizerFactory.GetSynchronizer(syncRoot);
            SyncRoot = synchronizer.SyncRoot;
        }

        public ConcurrentList(IEnumerable<T> collection)
        {
            list = collection.ToList();
        }

        public ConcurrentList(int capacity)
        {
            list = new List<T>(capacity);
        }

        public T this[int index]
        {
            get => synchronizer.Invoke(() => list[index]);
            set => synchronizer.Invoke(() => list[index] = value);
        }

        object IList.this[int index]
        {
            get => synchronizer.Invoke(() => list[index]);
            set => synchronizer.Invoke(() => ((IList)list)[index] = value);
        }

        public int Count => synchronizer.Invoke(() => list.Count);
        public bool IsReadOnly => false;
        public bool IsSynchronized => true;
        public object SyncRoot { get; }
        public bool IsFixedSize => false;

        public void Add(T item) => synchronizer.Invoke(
            () => list.Add(item));

        public int Add(object value) => synchronizer.Invoke(
            () => ((IList)list).Add(value));

        public void Clear() => synchronizer.Invoke(
            () => list.Clear());

        public bool Contains(T item) => synchronizer.Invoke(
            () => list.Contains(item));

        public bool Contains(object value) => synchronizer.Invoke(
            () => ((IList)list).Contains(value));

        public void CopyTo(T[] array, int arrayIndex) => synchronizer.Invoke(
            () => list.CopyTo(array, arrayIndex));

        public void CopyTo(Array array, int index) => synchronizer.Invoke(
            () => ((ICollection)list).CopyTo(array, index));

        public IEnumerator<T> GetEnumerator() => synchronizer.Invoke(
            () => list.GetEnumerator());

        public int IndexOf(T item) => synchronizer.Invoke(
            () => list.IndexOf(item));

        public int IndexOf(object value) => synchronizer.Invoke(
            () => ((IList)list).IndexOf(value));

        public void Insert(int index, T item) => synchronizer.Invoke(
            () => list.Insert(index, item));

        public void Insert(int index, object value) => synchronizer.Invoke(
            () => ((IList)list).Insert(index, value));

        public bool Remove(T item) => synchronizer.Invoke(
            () => list.Remove(item));

        public void Remove(object value) => synchronizer.Invoke(
            () => ((IList)list).Remove(value));

        public void RemoveAt(int index) => synchronizer.Invoke(
            () => list.RemoveAt(index));

        IEnumerator IEnumerable.GetEnumerator() => synchronizer.Invoke(
            () => ((IEnumerable)list).GetEnumerator());
    }
}
