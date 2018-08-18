using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NetworkChecker.CsvWorkerService.Exceptions
{
    public class CsvMapperException : Exception
    {
        public CsvMapperException()
        {
        }

        public CsvMapperException(string message) : base(message)
        {
        }

        public CsvMapperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
