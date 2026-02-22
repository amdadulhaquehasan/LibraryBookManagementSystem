using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class InvalidPublicationYearException : Exception
    {
        public InvalidPublicationYearException(int Year)
            : base($"The publication year '{Year}' is invalid.")
        {
        }
    }
}
