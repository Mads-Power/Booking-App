using System;
using BookingApp.Helpers;

namespace BookingAppUnitTests
{
    public class FixedDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _fixedDateTime;

        public FixedDateTimeProvider(DateTime fixedDateTime)
            => _fixedDateTime = fixedDateTime;

        public FixedDateTimeProvider() { }

        public int Compare(DateTime t1, DateTime t2) => DateTime.Compare(t1, t2);

        public DateTime Today() => _fixedDateTime.Date;

        public DateTime Parse(string date) => DateTime.Parse(date);
    }
}

