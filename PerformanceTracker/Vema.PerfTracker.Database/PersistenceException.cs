using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch, 15.02.2012 00:10
    /// Custom exception class for any handling persistence problems.
    /// </summary>
    [Serializable]
    public class PersistenceException : Exception
    {
        /// <summary>
        /// Gets the fully qualified class name, that triggered the <see cref="PersistenceException"/>.
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        internal PersistenceException()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal PersistenceException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        internal PersistenceException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        ///   
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        internal PersistenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="className">The fully qualified class name triggering the <see cref="PersistenceException"/>.</param>
        internal PersistenceException(string message, string className)
        {
            ClassName = className;
        }
    }
}
