using System;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace BookingApp.Helpers
{
	public class DateTimeProvider : IDateTimeProvider
	{
        public int Compare(DateTime t1, DateTime t2) => DateTime.Compare(t1, t2);

        // Automatically parse dates to be in UTC if not already as that is a requirement of DB.
        public DateTime Parse(string date)
        {
            return DateTime.Parse(date).ToUniversalTime();
        }

        public DateTime Today() => DateTime.Today.ToUniversalTime();
    }
}

