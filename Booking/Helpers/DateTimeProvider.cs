using System;
namespace BookingApp.Helpers
{
	public class DateTimeProvider : IDateTimeProvider
	{
        public int Compare(DateTime t1, DateTime t2) => DateTime.Compare(t1, t2);

        public DateTime Parse(string date) => DateTime.Parse(date).ToUniversalTime();

        public DateTime Today() => DateTime.Today;
    }
}

