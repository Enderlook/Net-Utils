using System.Collections.Generic;
using System.Numerics;

namespace Enderlook.Utils
{
    /// <summary>
    /// Set of inbuilt iterables.
    /// </summary>
    public static partial class Samples
    {
        /// <summary>
        /// Iterate from <see cref="char.MinValue"/> to <see cref="char.MaxValue"/>.
        /// </summary>
        public static IEnumerable<char> Characters {
            get {
                for (int i = char.MinValue; i <= char.MaxValue; i++)
                    yield return (char)i;
            }
        }

        /// <summary>
        /// Iterate from 'A' to 'Z', then from 'z' to 'A', then from '0' to '9'.
        /// </summary>
        public static IEnumerable<char> AlphabetAndNumbers {
            get {
                for (int i = 'A'; i <= 'z'; i++)
                    yield return (char)i;
                for (int i = '0'; i <= '9'; i++)
                    yield return (char)i;
            }
        }

        /// <summary>
        /// Iterate from '0' to '9'.
        /// </summary>
        public static IEnumerable<char> Numbers {
            get {
                for (int i = '0'; i <= '9'; i++)
                    yield return (char)i;
            }
        }

        /// <summary>
        /// Iterate from 'a' to 'z'.
        /// </summary>
        public static IEnumerable<char> AlphabetLower {
            get {
                for (int i = 'a'; i <= 'z'; i++)
                    yield return (char)i;
            }
        }

        /// <summary>
        /// Iterate from 'A' to 'Z'.
        /// </summary>
        public static IEnumerable<char> AlphabetUpper {
            get {
                for (int i = 'A'; i <= 'Z'; i++)
                    yield return (char)i;
            }
        }

        /// <summary>
        /// Iterate from 'A' to 'Z', then from 'a' to 'z'.
        /// </summary>
        public static IEnumerable<char> Alphabet {
            get {
                for (int i = 'A'; i <= 'z'; i++)
                    yield return (char)i;
            }
        }

        /// <summary>
        /// Iterate from 1 to infinity.
        /// </summary>
        public static IEnumerable<BigInteger> BigIntegerPositives {
            get {
                BigInteger i = 1;
                while (true)
                    yield return i++;
            }
        }

        /// <summary>
        /// Iterate from 0 to infinity.
        /// </summary>
        public static IEnumerable<BigInteger> BigIntegerPositivesWithZero {
            get {
                BigInteger i = 0;
                while (true)
                    yield return i++;
            }
        }

        /// <summary>
        /// Iterate from -1 to -infinity.
        /// </summary>
        public static IEnumerable<BigInteger> BigIntegerNegatives {
            get {
                BigInteger i = -1;
                while (true)
                    yield return i--;
            }
        }

        /// <summary>
        /// Iterate from 0 to -infinity.
        /// </summary>
        public static IEnumerable<BigInteger> BigIntegerNegativesWithZero {
            get {
                BigInteger i = 0;
                while (true)
                    yield return i--;
            }
        }
    }
}
