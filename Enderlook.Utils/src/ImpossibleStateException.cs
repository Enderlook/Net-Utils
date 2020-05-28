using System;

namespace Enderlook.Utils.Exceptions
{
    /// <summary>
    /// Represents a code state that in theory it should never happen.<br/>
    /// Used when the compiler produces error because it can't infer that all code path returns a value.
    /// </summary>
    public class ImpossibleStateException : InvalidOperationException
    {
        /// <summary>
        /// Creates an instance of error that represents a code state that in theory it should never happen.
        /// </summary>
        public ImpossibleStateException() { }

        /// <summary>
        /// Creates an instance of error that represents a code state that in theory it should never happen.
        /// </summary>
        /// <param name="message">Specified error mensage.</param>
        public ImpossibleStateException(string message) : base(message) { }

        /// <summary>
        /// Creates an instance of error that represents a code state that in theory it should never happen.
        /// </summary>
        /// <param name="message">Specified error mensage.</param>
        /// <param name="innerException">Reference to a inner exception that is the cause of this exception.</param>
        public ImpossibleStateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
