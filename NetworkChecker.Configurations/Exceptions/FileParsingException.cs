using System;

namespace NetworkChecker.Configurations.Exceptions
{
    public class FileParsingException : Exception
    {
        public FileParsingException()
        {
        }

        public FileParsingException(string message) : base(message)
        {
        }

        public FileParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
