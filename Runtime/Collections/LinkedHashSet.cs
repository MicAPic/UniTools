using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniTools.Collections
{
    public class LinkedHashSet<T> : ISet<T>
    {
        private readonly IDictionary<T, LinkedListNode<T>> dict;
        private readonly LinkedList<T> list;

        public LinkedHashSet(int initialCapacity)
        {
            dict = new Dictionary<T, LinkedListNode<T>>(initialCapacity);
            list = new LinkedList<T>();
        }

        public LinkedHashSet()
        {
            dict = new Dictionary<T, LinkedListNode<T>>();
            list = new LinkedList<T>();
        }

        public LinkedHashSet(IEnumerable<T> e) : this()
        {
            AddEnumerable(e);
        }

        public LinkedHashSet(int initialCapacity, IEnumerable<T> e) : this(initialCapacity)
        {
            AddEnumerable(e);
        }

        private void AddEnumerable(IEnumerable<T> e)
        {
            foreach (var t in e) Add(t);
        }

        // ISet implementation

        public bool Add(T item)
        {
            if (dict.ContainsKey(item)) return false;

            var node = list.AddLast(item);
            dict[item] = node;
            return true;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            foreach (var t in other) Remove(t);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            var ts = new T[Count];
            CopyTo(ts, 0);
            foreach (var t in ts)
                if (!other.Contains(t)) Remove(t);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            var contains = 0;
            var noContains = 0;
            foreach (var t in other)
                if (Contains(t))
                    contains++;
                else
                    noContains++;

            return contains == Count && noContains > 0;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            var otherCount = other.Count();
            if (Count <= otherCount) return false;

            var contains = 0;
            var noContains = 0;
            foreach (var t in this)
                if (other.Contains(t))
                    contains++;
                else
                    noContains++;

            return contains == otherCount && noContains > 0;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            return this.All(other.Contains);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            return other.All(Contains);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            return other.Any(Contains);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            var otherCount = other.Count();
            if (Count != otherCount) return false;

            return IsSupersetOf(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            var ts = new T[Count];
            CopyTo(ts, 0);
            var otherList = new HashSet<T>(other);
            foreach (var t in ts)
                if (otherList.Contains(t))
                {
                    Remove(t);
                    otherList.Remove(t);
                }

            foreach (var t in otherList) Add(t);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            foreach (var t in other) Add(t);
        }

        // ICollection<T> implementation

        public int Count => dict.Count;

        public bool IsReadOnly => dict.IsReadOnly;

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            dict.Clear();
            list.Clear();
        }

        public bool Contains(T item)
        {
            return dict.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (!dict.TryGetValue(item, out var node)) return false;

            dict.Remove(item);
            list.Remove(node);
            return true;
        }

        // IEnumerable<T> implementation

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        // IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        
        // Etc.

        public T First()
        {
            return list.First.Value;
        }
        
        public T Last()
        {
            return list.Last.Value;
        }
    }
}