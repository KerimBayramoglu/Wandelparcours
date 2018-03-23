﻿using System;
using System.Runtime.Serialization;

namespace WebService.Helpers.Exceptions
{
    public class WebArgumentNullException : WebArgumentException
    {
        /// <inheritdoc cref="ArgumentException()" />
        /// <summary>
        /// Initializes a new instance of the <see cref="WebArgumentException"></see> class.
        /// </summary>
        public WebArgumentNullException()
        {
        }

        /// <inheritdoc cref="ArgumentException(string)"/>
        /// <summary>
        /// Initializes a new instance of the <see cref="WebArgumentException"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public WebArgumentNullException(string message) : base(message)
        {
        }

        /// <inheritdoc cref="ArgumentException(string, Exception)"/>
        /// <summary>
        /// Initializes a new instance of the <see cref="WebArgumentException"></see> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public WebArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc cref="ArgumentException(string, string)"/>
        /// <summary>
        /// Initializes a new instance of the <see cref="WebArgumentException"></see> class with a specified error
        /// message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        public WebArgumentNullException(string message, string paramName) : base(message, paramName)
        {
        }

        /// <inheritdoc cref="ArgumentException(string, string, Exception)"/>
        /// <summary>
        /// Initializes a new instance of the <see cref="WebArgumentException"></see> class with a specified error message, 
        /// the parameter name, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception.</param>
        public WebArgumentNullException(string message, string paramName, Exception innerException) : base(message,
            paramName, innerException)
        {
        }

        /// <inheritdoc cref="ArgumentException(SerializationInfo, StreamingContext)"/>
        /// <summary>
        /// Initializes a new instance of the <see cref="WebArgumentException"></see> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected WebArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}