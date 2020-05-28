using System;

namespace Enderlook.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the underlying primitive value of <paramref name="source"/>.
        /// </summary>
        /// <param name="source"><see cref="Enum"/> which value is going to get.</param>
        /// <returns>Primitive value of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static object GetUnderlyingValue(this Enum source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return Convert.ChangeType(source, Enum.GetUnderlyingType(source.GetType()));
        }
    }
}
