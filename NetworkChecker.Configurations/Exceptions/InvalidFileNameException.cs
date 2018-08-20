using System;

namespace NetworkChecker.Configurations.Exceptions
{
    public class InvalidFileNameException : Exception
    {
        public InvalidFileNameException()
        {
        }

        public InvalidFileNameException(string message) : base(message)
        {
        }

        public InvalidFileNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
