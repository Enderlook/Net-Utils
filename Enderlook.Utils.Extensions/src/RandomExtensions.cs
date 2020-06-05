using System;
using System.Runtime.CompilerServices;

namespace Enderlook.Utils.Extensions
{
    /// <summary>
    /// An extension class for <see cref="Random"/>.
    /// </summary>
    public static class RandomExtensions
    {
        /// <inheritdoc cref="Random.Next(int, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Range(this Random source, int min, int max) => source.Next(min, max);

        /// <inheritdoc cref="Random.Next(int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Range(this Random source, int max) => source.Next(max);

        /// <summary>
        /// Returns a random double that is without the specified range.
        /// </summary>
        /// <param name="source">Seed.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random number between <paramref name="min"/> and <paramref name="max"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Range(this Random source, double min, double max) => (source.NextDouble() * (max - min)) + min;

        /// <summary>
        /// Returns a random double that is less than the specified <paramref name="max"/>.
        /// </summary>
        /// <param name="source">Seed.</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random number less than<paramref name="max"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Range(this Random source, double max) => source.NextDouble() * max;

        /// <summary>
        /// Returns a random double that is without the specified range.
        /// </summary>
        /// <param name="source">Seed.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random number between <paramref name="min"/> and <paramref name="max"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Range(this Random source, float min, float max) => ((float)source.NextDouble() * (max - min)) + min;

        /// <summary>
        /// Returns a random double that is less than the specified <paramref name="max"/>.
        /// </summary>
        /// <param name="source">Seed.</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random number less than<paramref name="max"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Range(this Random source, float max) => (float)source.NextDouble() * max;

        /// <summary>
        /// Produces a random <see cref="byte"/>. 
        /// </summary>
        /// <param name="source"><see cref="Random"/> generator.</param>
        /// <returns>Random <see cref="byte"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte NextByte(this Random source) => (byte)source.Next(byte.MinValue, byte.MaxValue);

        /// <summary>
        /// Produces a random <see cref="char"/>. 
        /// </summary>
        /// <param name="source"><see cref="Random"/> generator.</param>
        /// <returns>Random <see cref="char"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char NextChar(this Random source) => (char)source.Next(char.MinValue, char.MaxValue);
    }
}