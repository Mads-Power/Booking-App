using System;
namespace BookingApp.Helpers
{
	public interface IDateTimeProvider
	{
		DateTime Today();
		DateTime Parse(string date);
		int Compare(DateTime t1, DateTime t2);
	}
}

