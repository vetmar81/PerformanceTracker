using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Vema.PerfTracker.Database
{
    [Serializable]
    public class DaoException : Exception
    {
        /// <summary>
        /// Gets the type, that triggered the <see cref="DaoException"/>.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DaoException"/> class.
        /// </summary>
        internal DaoException()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DaoException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal DaoException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DaoException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        internal DaoException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DaoException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        ///   
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        internal DaoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DaoException"/> class.
        /// </summary>
        /// <param name="type">The type triggering the <see cref="DaoException"/>.</param>
        /// <param name="message">The message.</param>
        internal DaoException(string type, string message)
        {
            Type = type;
        }
    }
}
