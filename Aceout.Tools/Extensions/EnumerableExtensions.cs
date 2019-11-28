using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Tools.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static IEnumerable<IEnumerable<T>> SplitBy<T>(this IEnumerable<T> source, int count)
        {
            for (var i = 0; i < source.Count(); i += count)
            {
                yield return source.Skip(i).Take(count);
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int pieces)
        {
            var elements = source.Count() / pieces;

            for (var i = 0; i < source.Count(); i += elements)
            {
                yield return source.Skip(i).Take(elements);
            }
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null) return new List<T>();

            return source;
        }

        public static bool HasElements<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static IEnumerable<T> DistinctBy<T,K>(this IEnumerable<T> source, Func<T,K> keySelector)
        {
            var keys = new HashSet<K>();

            foreach (var item in source)
            {
                if (keys.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
