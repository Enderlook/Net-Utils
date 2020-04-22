using System;
using System.Collections.Generic;
using System.Linq;

namespace Enderlook.Utils
{
    /// <summary>
    /// Helper class to manipulate <see cref="string"/>s.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Get the common substring preffix in all <paramref name="strings"/>.
        /// </summary>
        /// <param name="strings"><see cref="string"/>s to search for common preffix.</param>
        /// <returns>Common preffix of all <paramref name="strings"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="strings"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="strings"/> is empty.</exception>
        public static string GetCommonPreffix(params string[] strings)
        {
            // https://stackoverflow.com/questions/2070356/find-common-preffix-of-strings

            if (strings is null) throw new ArgumentNullException(nameof(strings));
            int stringsCount = strings.Length;
            if (stringsCount == 0) throw new ArgumentException("Must have at least one element.", nameof(strings));
            if (stringsCount == 1)
                return strings[0];

            int index = 0;
            int length = int.MaxValue;
            for (int i = 0; i < stringsCount; i++)
            {
                if (strings[i].Length < length)
                {
                    length = strings[i].Length;
                    index = i;
                }
            }

            string shortest = strings[index];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < stringsCount; j++)
                {
                    if (strings[j][i] != shortest[i])
                        return shortest.Substring(0, i);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get the common substring preffix in all <paramref name="strings"/>.
        /// </summary>
        /// <param name="str">First <see cref="string"/>s to search for common preffix.</param>
        /// <param name="strings">Rest of <see cref="string"/>s to search for common preffix.</param>
        /// <returns>Common preffix of all <paramref name="strings"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="strings"/> or <paramref name="str"/> are <see langword="null"/>.</exception>
        public static string GetCommonPreffix(string str, params string[] strings)
        {
            // https://stackoverflow.com/questions/2070356/find-common-preffix-of-strings

            if (strings is null) throw new ArgumentNullException(nameof(strings));
            int stringsCount = strings.Length;
            if (stringsCount == 0)
                return str;

            int index = -1;
            int length = str.Length;
            for (int i = 0; i < stringsCount; i++)
            {
                if (strings[i].Length < length)
                {
                    length = strings[i].Length;
                    index = i;
                }
            }

            string shortest = index == -1 ? str : strings[index];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < stringsCount; j++)
                {
                    if (strings[j][i] != shortest[i])
                        return shortest.Substring(0, i);
                }
                if (str[i] != shortest[i])
                    return shortest.Substring(0, i);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get the common substring preffix in all <paramref name="strings"/>.
        /// </summary>
        /// <param name="strings"><see cref="string"/>s to search for common preffix.</param>
        /// <returns>Common preffix of all <paramref name="strings"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="strings"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="strings"/> is empty.</exception>
        public static string GetCommonPreffix(IEnumerable<string> strings)
        {
            if (strings is null) throw new ArgumentNullException(nameof(strings));
            return GetCommonPreffix(strings.ToArray());
        }

        /// <summary>
        /// Get the common substring suffix in all <paramref name="strings"/>.
        /// </summary>
        /// <param name="strings"><see cref="string"/>s to search for common suffix.</param>
        /// <returns>Common suffix of all <paramref name="strings"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="strings"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="strings"/> is empty.</exception>
        public static string GetCommonSuffix(params string[] strings)
        {
            // TODO: this can be improved

            if (strings is null) throw new ArgumentNullException(nameof(strings));
            int stringsCount = strings.Length;
            if (stringsCount == 0) throw new ArgumentException("Must have at least one element.", nameof(strings));
            if (stringsCount == 1)
                return strings[0];

            string[] array = new string[strings.Length];
            for (int i = 0; i < strings.Length; i++)
                array[i] = string.Concat(strings[i].Reverse());
            return string.Concat(GetCommonPreffix(array).Reverse());
        }

        /// <summary>
        /// Get the common substring suffix in all <paramref name="strings"/>.
        /// </summary>
        /// <param name="str">First <see cref="string"/>s to search for common suffix.</param>
        /// <param name="strings"><see cref="string"/>s to search for common suffix.</param>
        /// <returns>Common suffix of all <paramref name="strings"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="strings"/> or <paramref name="str"/> are <see langword="null"/>.</exception
        public static string GetCommonSuffix(string str, params string[] strings)
        {
            // TODO: this can be improved

            if (strings is null) throw new ArgumentNullException(nameof(strings));
            int stringsCount = strings.Length;
            if (stringsCount == 0)
                return str;

            string[] array = new string[strings.Length];
            for (int i = 0; i < strings.Length; i++)
                array[i] = string.Concat(strings[i].Reverse());
            return string.Concat(GetCommonPreffix(string.Concat(str.Reverse()), array).Reverse());
        }

        /// <summary>
        /// Get the common substring suffix in all <paramref name="strings"/>.
        /// </summary>
        /// <param name="strings"><see cref="string"/>s to search for common suffix.</param>
        /// <returns>Common suffix of all <paramref name="strings"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="strings"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="strings"/> is empty.</exception>
        public static string GetCommonSuffix(IEnumerable<string> strings)
        {
            // TODO: this can be improved

            if (strings is null) throw new ArgumentNullException(nameof(strings));
            return GetCommonSuffix(strings.ToArray());
        }
    }
}
