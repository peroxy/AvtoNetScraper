using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace AvtoNetScraper.Utilities
{
    public static class Extensions
    {
        public static string GetUntilOrEmpty(this string text, string stopAt)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

        public static string ToLongString(this TimeSpan span)
        {
            if (span.Days > 1)
            {
                return $"{span.Days : 0} days, {span.Hours : 0} hours, {span.Minutes : 0} minutes, {span.Seconds}";
            }
            if (span.TotalHours > 1)
            {
                return $"{span.Hours: 0} hours, {span.Minutes: 0} minutes, {span.Seconds: 0} seconds";
            }
            if (span.TotalMinutes > 1)
            {
                return $"{span.Minutes: 0} minutes, {span.Seconds: 0} seconds";
            }
                
            return $"{span.Seconds: 0} seconds";
        }

        /// <summary>
        /// Shuffles a list randomly by using Fisher-Yates/Knuth shuffle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rnd"></param>
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = list.Count; i > 0; i--)
                list.Swap(0, rnd.Next(0, i));
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("size", "Must be greater than zero.");
            }

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int i = 0;
                    // Batch is a local function closing over `i` and `enumerator` that
                    // executes the inner batch enumeration
                    IEnumerable<T> Batch()
                    {
                        do yield return enumerator.Current;
                        while (++i < size && enumerator.MoveNext());
                    }

                    yield return Batch();
                    while (++i < size && enumerator.MoveNext()) ; // discard skipped items
                }
            }
                
        }
    }
}
