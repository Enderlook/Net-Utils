using System;
using System.Collections.Generic;

namespace Enderlook.Utils
{
    // https://stackoverflow.com/questions/716552/can-you-create-a-simple-equalitycomparert-using-a-lambda-expression

    /// <summary>
    /// Helper class to create <see cref="IEqualityComparer{T}"/> based on a <see cref="Func{T, TResult}"/>.
    /// </summary>
    public static class CustomEqualityComparer
    {
        /// <summary>
        /// Creates a custom <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of element to compare.</typeparam>
        /// <typeparam name="TKey">Type of element of projector.</typeparam>
        /// <param name="projector">Project the value that will be compared using the default comparers of that <typeparamref name="TKey"/>.</param>
        /// <returns><see cref="IEqualityComparer{T}"/> for <typeparamref name="TSource"/> type.</returns>
        public static IEqualityComparer<TSource> Create<TSource, TKey>(Func<TSource, TKey> projector)
            => new CustomComparer<TSource, TKey>(projector);
        
        /// <summary>
        /// Creates a custom <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of element to compare.</typeparam>
        /// <typeparam name="TKey">Type of element of projector.</typeparam>
        /// <param name="projector">Project the value that will be compared using the default comparers of that <typeparamref name="TKey"/>.</param>
        /// <param name="comparer">Comparer used to compare the value returned by the <paramref name="projector"/>.</param>
        /// <returns><see cref="IEqualityComparer{T}"/> for <typeparamref name="TSource"/> type.</returns>
        public static IEqualityComparer<TSource> Create<TSource, TKey>(Func<TSource, TKey> projector, IEqualityComparer<TKey> comparer)
            => new CustomComparer<TSource, TKey>(projector, comparer);

        private class CustomComparer<TSource, TKey> : IEqualityComparer<TSource>
        {
            private readonly Func<TSource, TKey> projector;
            private readonly IEqualityComparer<TKey> comparer;

            public CustomComparer(Func<TSource, TKey> projector) : this(projector, null) { }

            public CustomComparer(Func<TSource, TKey> projector, IEqualityComparer<TKey> comparer)
            {
                if (projector is null) throw new ArgumentNullException(nameof(projector));
                this.comparer = comparer ?? EqualityComparer<TKey>.Default;
                this.projector = projector;
            }

            public bool Equals(TSource x, TSource y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                return comparer.Equals(projector(x), projector(y));
            }

            public int GetHashCode(TSource obj)
            {
                // Don't use is pattern because it won't work with Unity3D
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                return comparer.GetHashCode(projector(obj));
            }
        }
    }
}