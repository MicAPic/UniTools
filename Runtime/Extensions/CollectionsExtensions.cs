using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniTools.Extensions
{
    public static class CollectionsExtensions
    {
        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> collection)
        {
            collection.ForEach(self.Add);
        }

        public static void ForEach<T>(this IEnumerable<T> self, Action<T, int> action)
        {
            var index = 0;
            ForEach(self, x =>
            {
                action.Invoke(x, index);
                index++;
            });
        }

        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action?.Invoke(item);
            }
        }

        public static bool ConsistsOfSameElements<T>(this IEnumerable<T> self, IEnumerable<T> other)
        {
            if (self.Except(other).Any()) return false;
            if (other.Except(self).Any()) return false;
            return true;
        }

        public static T[] Shuffle<T>(this T[] self)
        {
            var rand = new Random();

            for (var i = 0; i < self.Length - 1; i++)
            {
                var j = rand.Next(i, self.Length);
                (self[i], self[j]) = (self[j], self[i]);
            }

            return self;
        }
        
        public static IList<T> Shuffle<T>(this IList<T> self) 
        {
            var rand = new Random();

            for (var i = 0; i < self.Count - 1; i++)
            {
                var j = rand.Next(i, self.Count);
                (self[i], self[j]) = (self[j], self[i]);
            }
            
            return self;
        }

        public static T GetRandom<T>(this ICollection<T> self)
        {
            if (self.IsEmpty()) return default;

            var random = new Random();
            var index = random.Next(0, self.Count);

            return self.ElementAt(index);
        }

        public static T GetRandom<T>(this ICollection<T> self, Predicate<T> match)
        {
            var matches = self.Where(x => match(x));
            return matches.ToList().GetRandom();
        }
        
        public static bool IsEmpty(this ICollection self)
        {
            return self.Count == 0;
        }

        public static bool IsNotEmpty(this ICollection self)
        {
            return !self.IsEmpty();
        }

        public static bool IsNullOrEmpty(this ICollection self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool IsNotNullOrEmpty(this ICollection self)
        {
            return !self.IsNullOrEmpty();
        }
        
        public static bool IsEmpty<T>(this ICollection<T> self)
        {
            return self.Count == 0;
        }

        public static bool IsNotEmpty<T>(this ICollection<T> self)
        {
            return !self.IsEmpty();
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool IsNotNullOrEmpty<T>(this ICollection<T> self)
        {
            return !self.IsNullOrEmpty();
        }
        
        public static bool IsEmpty<T>(this T[] self)
        {
            return self.Length == 0;
        }

        public static bool IsNotEmpty<T>(this T[] self)
        {
            return !self.IsEmpty();
        }

        public static bool IsNullOrEmpty<T>(this T[] self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool IsNotNullOrEmpty<T>(this T[] self)
        {
            return !self.IsNullOrEmpty();
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        public static ISet<T> ToSet<T>(this IEnumerable<T> collection)
        {
            return collection.ToHashSet();
        }

        public static bool InRange(this int index, ICollection collection)
        {
            return index >= 0 && index < collection.Count;
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> e)
        {
            while (e.MoveNext())
            {
                yield return e.Current;
            }
        }
    }
}