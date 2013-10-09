// -----------------------------------------------------------------------
// <copyright file="ValidationError.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;

    public class ValidationError
    {
        public object ErrorContent
        {
            get;
            private set;
        }

        public Exception Exception
        {
            get;
            private set;
        }

        internal ValidationError(object errorContent, Exception exception)
        {
            this.ErrorContent = errorContent;
            this.Exception = exception;

            if (Exception != null)
            {
                ErrorContent = ErrorContent ?? Exception.Message;
            }
        }
    }
}
