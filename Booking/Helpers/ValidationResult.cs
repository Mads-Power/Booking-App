using System;
using System.Text.RegularExpressions;

namespace BookingApp.Helpers
{
    /// <summary> Simple validator with results and reasoning. </summary>
    public class ValidationResult
    {
        /// <summary> Whether validation succeeded. </summary>
        public bool Result { get; set; }

        /// <summary> Reason for why validation failed. </summary>
        public string RejectionReason { get; set; }

        // Constructor.
        public ValidationResult(bool result, string rejectionReason = "")
        {
            Result = result;
            RejectionReason = rejectionReason;
        }

        /// <summary>
        /// Helper method to validate if date string input is safe to be parsed.
        /// </summary>
        /// <param name="date">Date string to be validated.</param>
        /// <returns>ValidationResult true if date is valid, false with rejection reason if invalid.</returns>
        public static ValidationResult ValidateDateString(string date)
        {
            // length/size validation
            if (date.Length < 1) return new ValidationResult(false, "Empty date string size");

            if (date.Length > 40) return new ValidationResult(false, "Unreasonable date string size");

            // lexical content validation
            return ValidateDateTime(date);
        }

        public override string ToString()
        {
            return $"Result: {Result}, Reason: {RejectionReason}";
        }

        private static ValidationResult ValidateDateTime(string date)
        {
            var pattern = "^(?<datetime>(?<date>\\d{4}-\\d{2}-\\d{2})T(?<time>\\d{2}:\\d{2}:\\d{2}(.\\d{1,5})?(?<timezone>Z|\\+\\d{2}(:\\d{2})?)))$";

            Match match = Regex.Match(date, pattern);

            if (!match.Success) return new ValidationResult(false, "Invalid date string, date must follow ISO 8601 and be at midnight CET/CEST");

            var isDaylightSavings = DateTime.Parse(date).IsDaylightSavingTime();

            return ValidateTimeZone(match, isDaylightSavings);
        }

        private static ValidationResult ValidateTimeZone(Match dateMatch, bool isDaylightSavings)
        {
            if (dateMatch.Groups["timezone"].Value.StartsWith("Z"))
            {
                var patternUTC = "23:00:00(.\\d{1,5})?Z";
                var patternUTCWithDaylightSavings = "22:00:00(.\\d{1,5})?Z";
                Match utcMatch;

                if (isDaylightSavings)
                {
                    utcMatch = Regex.Match(dateMatch.Groups["time"].Value, patternUTCWithDaylightSavings);
                    if (!utcMatch.Success) return new ValidationResult(false, "Invalid UTC time input, must follow YYYY-MM-DDT22:00:00Z (remember daylight savings time)");
                }
                else
                {
                    utcMatch = Regex.Match(dateMatch.Groups["time"].Value, patternUTC);
                    if (!utcMatch.Success) return new ValidationResult(false, "Invalid UTC time input, must follow YYYY-MM-DDT23:00:00Z");
                }
            }
            else
            {
                var patternLocale = "00:00:00(.\\d{1,5})?\\+01:00";
                var patternLocaleWithDaylightSavings = "00:00:00(.\\d{1,5})?\\+02:00";
                Match localeMatch;

                if (isDaylightSavings)
                {
                    localeMatch = Regex.Match(dateMatch.Groups["time"].Value, patternLocaleWithDaylightSavings);
                    if (!localeMatch.Success) return new ValidationResult(false, "Invalid Local time input, must follow YYYY-MM-DDT00:00:00+02:00 (remember daylight savings time)");
                }
                else
                {
                    localeMatch = Regex.Match(dateMatch.Groups["time"].Value, patternLocale);
                    if (!localeMatch.Success) return new ValidationResult(false, "Invalid Local time input, must follow YYYY-MM-DDT00:00:00+01:00");
                }
            }
            return new ValidationResult(true);
        }
    }
}

