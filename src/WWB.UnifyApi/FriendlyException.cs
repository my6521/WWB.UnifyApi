using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWB.UnifyApi
{
    public class FriendlyException : Exception
    {
        public object ErrorCode { get; set; }

        public FriendlyException() : base()
        {
        }

        public FriendlyException(string message) : base(message)
        {
        }

        public FriendlyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FriendlyException(object errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public FriendlyException(object errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public static FriendlyException Of<T>(T errorCode, params string[] formatTexts) where T : Enum
        {
            var error = errorCode.HandleCode();
            var message = error.Item2;
            if (!string.IsNullOrEmpty(message) && formatTexts.Length > 0)
            {
                message = string.Format(message, formatTexts);
            }

            return new FriendlyException(error.Item1, message);
        }
    }
}