using System;
using System.Collections.Generic;
using System.Linq;

namespace Enderlook.Utils.code.Linq
{
    /// <summary>
    /// Helper extensions for <see cref="IEnumerable{T}"/> related to transposition.
    /// </summary>
    public static class TransposeExtensions
    {
        private const string ALL_ARRAYS_MUST_HAVE_SAME_LENGTH = "This array an all its elements must have the same length.";

        /// <summary>
        /// Determines how non-uniform enumerations will be handled.
        /// </summary>
        public enum TransposeMode
        {
            /// <summary>
            /// It strip rows that have missing elements.
            /// </summary>
            StripOnMissing,

            /// <summary>
            /// Replace missing elements with default valuess.
            /// </summary>
            DefaultOnMissing,

            /// <summary>
            /// Raise <see cref="ArgumentOutOfRangeException"/> when there is a missing value.
            /// </summary>
            ErrorOnMissing,
        }

        /// <summary>
        /// Transpose <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of element of the nested <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">Sequence which will be transposed.</param>
        /// <param name="mode">How non-uniform sequences are handled.</param>
        /// <returns>Transposed <paramref name="source"/>.</returns>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source, TransposeMode mode = TransposeMode.ErrorOnMissing)
        {
            // https://stackoverflow.com/a/10555037/7655838 from https://stackoverflow.com/questions/10554866/how-do-you-transpose-dimensions-in-a-2d-collection-using-linq
            if (source is null) throw new ArgumentNullException(nameof(source));

            return Work();

            IEnumerable<IEnumerable<T>> Work()
            {
                if (!source.Any())
                    yield break;

                IEnumerator<T>[] enumerators = source.Select(t => t.GetEnumerator()).ToArray();
                try
                {
                    T[] line = new T[enumerators.Length];
                    while (true)
                    {
                        bool atLeastOneTrue = false;
                        bool allTrue = true;
                        for (int i = 0; i < enumerators.Length; i++)
                        {
                            bool value = enumerators[i].MoveNext();
                            atLeastOneTrue |= value;
                            allTrue &= value;
                            if (value)
                                line[i] = enumerators[i].Current;
                            else
                            {
                                switch (mode)
                                {
                                    case TransposeMode.StripOnMissing:
                                        yield break;
                                    case TransposeMode.DefaultOnMissing:
                                        line[i] = default;
                                        break;
                                }
                            }
                        }
                        if (atLeastOneTrue)
                        {
                            if (mode == TransposeMode.ErrorOnMissing && !allTrue)
                                throw new ArgumentOutOfRangeException("All nested enumerators must have same count.", nameof(source));
                            else
                                yield return line;
                        }
                        else
                            break;
                    }
                }
                finally
                {
                    for (int i = 0; i < enumerators.Length; i++)
                        enumerators[i].Dispose();
                }
            }
        }

        /// <summary>
        /// Transpose <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of element of the nested <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">Sequence which will be transposed.</param>
        /// <param name="mode">How non-uniform sequences are handled.</param>
        /// <returns>Transposed <paramref name="source"/>.</returns>
        public static List<T[]> Transpose<T>(this IList<IList<T>> source, TransposeMode mode = TransposeMode.ErrorOnMissing)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0)
                return new List<T[]>();

            List<T[]> lines = new List<T[]>();

            for (int j = 0; ; j++)
            {
                T[] line = new T[source.Count];
                bool flag = false;
                for (int i = 0; i < source.Count; i++)
                {
                    bool value = j < source[i].Count;
                    flag |= value;
                    if (value)
                        line[i] = source[i][j];
                    else
                    {
                        switch (mode)
                        {
                            case TransposeMode.StripOnMissing:
                                return lines;
                            case TransposeMode.DefaultOnMissing:
                                line[i] = default;
                                break;
                        }
                    }
                }
                if (flag)
                {
                    if (mode == TransposeMode.ErrorOnMissing)
                        throw new ArgumentOutOfRangeException("All nested enumerators must have same count.", nameof(source));
                    lines.Add(line);
                }
                else
                    break;
            }

            return lines;
        }

        /// <summary>
        /// Transpose the values of <paramref name="source"/> in place.<br/>
        /// It can only be used in square 2D multidimensional arrays.
        /// </summary>
        /// <typeparam name="T">Type of element in <paramref name="source"/>.</typeparam>
        /// <param name="source">Array to be transposed in place.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> width and height aren't the same.</exception>
        public static void TransposeInPlace<T>(this T[,] source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            int height = source.GetLength(0);
            int width = source.GetLength(1);
            if (height != width) throw new ArgumentException("Must be square.", nameof(source));

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    T tmp = source[i, j];
                    source[i, j] = source[j, i];
                    source[j, i] = tmp;
                }
            }
        }

        /// <summary>
        /// Transpose the values of <paramref name="source"/> in place.<br/>
        /// It can only be used in square 2D multidimensional arrays.
        /// </summary>
        /// <typeparam name="T">Type of element in <paramref name="source"/>.</typeparam>
        /// <param name="source">Array to be transposed in place.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> width and height aren't the same.</exception>
        public static void TransposeInPlace<T>(this T[][] source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            int height = source.Length;
            if (height == 0)
                return;
            int width = source.Length;
            if (height != width) throw new ArgumentException(ALL_ARRAYS_MUST_HAVE_SAME_LENGTH, nameof(source));
            for (int i = 1; i < height; i++)
                if (source[i].Length != height)
                    throw new ArgumentException(ALL_ARRAYS_MUST_HAVE_SAME_LENGTH, nameof(source));

            for (int i = 0; i < height; i++)
            {
                T[] iLine = source[i];
                for (int j = 0; j < i; j++)
                {
                    T tmp = iLine[j];
                    T[] jLine = source[j];
                    iLine[j] = jLine[i];
                    jLine[i] = tmp;
                }
            }
        }

        /// <summary>
        /// Transpose the values of <paramref name="source"/> in place.<br/>
        /// It can only be used in square 2D multidimensional arrays.
        /// </summary>
        /// <typeparam name="T">Type of element in <paramref name="source"/>.</typeparam>
        /// <param name="source">Array to be transposed in place.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> width and height aren't the same.</exception>
        public static void TransposeInPlace<T>(this IList<IList<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            int height = source.Count;
            if (height == 0)
                return;
            int width = source.Count;
            if (height != width) throw new ArgumentException(ALL_ARRAYS_MUST_HAVE_SAME_LENGTH, nameof(source));
            for (int i = 1; i < height; i++)
                if (source[i].Count != height)
                    throw new ArgumentException(ALL_ARRAYS_MUST_HAVE_SAME_LENGTH, nameof(source));

            for (int i = 0; i < height; i++)
            {
                IList<T> iLine = source[i];
                for (int j = 0; j < i; j++)
                {
                    T tmp = iLine[j];
                    IList<T> jLine = source[j];
                    iLine[j] = jLine[i];
                    jLine[i] = tmp;
                }
            }
        }

        /// <summary>
        /// Transpose elements of <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of element in <paramref name="source"/>.</typeparam>
        /// <param name="source">Array values to be transposed.</param>
        /// <returns>Transposed version of <paramref name="source"/>.</returns>
        public static T[,] Transpose<T>(this T[,] source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            int height = source.GetLength(0);
            int width = source.GetLength(1);
            T[,] copy = new T[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    copy[j, i] = source[i, j];
            return copy;
        }
    }
}
