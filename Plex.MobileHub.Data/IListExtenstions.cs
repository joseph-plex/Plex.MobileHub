using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public static class IListExtenstions
    {
        public static int FindIndex<T>(this IList<T> source, Predicate<T> match)
        {
            return source.FindIndex(0, match);
        }

        public static int FindIndex<T>(this IList<T> source, Int32 startIndex, Predicate<T> match)
        {
            return source.FindIndex(startIndex, source.Count - startIndex, match);
        }

        public static int FindIndex<T>(this IList<T> source, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            for (int i = startIndex, end = startIndex + count; i < end; i++)
                if (match(source[i]))
                    return i;
            return -1;
        }

        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (T item in source)
                destination.Add(item);
        }
    }
}
