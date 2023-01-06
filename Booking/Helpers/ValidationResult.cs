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
            var pattern = "^(?<datetime>(?<date>\\d{4}-\\d{2}-\\d{2})T(?<time>\\d{2}:\\d{2}:\\d{2}(.\\d{1,5})?(?<timezone>Z|\\+\\d{2}(:\\d{2})?)))$";
            var patternUTC = "23:00:00(.\\d{1,5})?Z";
            var patternLocale = "00:00:00+00:0(1|2):00";
            Match m = Regex.Match(date, pattern);

            if (!m.Success) return new ValidationResult(false, "Invalid date string, date must follow ISO 8601 and be at midnight Central European Time");

            if (m.Groups["timezone"].Value.StartsWith("Z"))
            {
                Match utcMatch = Regex.Match(m.Groups["time"].Value, patternUTC);
                if (!utcMatch.Success) return new ValidationResult(false, "Invalid UTC time input, must follow YYYY-MM-DDT23:00:00Z");
            }
            else
            {
                Match localeMatch = Regex.Match(m.Groups["time"].Value, patternLocale);
                if (!localeMatch.Success) return new ValidationResult(false, "Invalid Local time input, must follow YYYY-MM-DDT00:00:00+01:00 or YYYY-MM-DDT00:00:00+02:00");
            }

            return new ValidationResult(true);
        }

        public override string ToString()
        {
            return $"Result: {Result}, Reason: {RejectionReason}";
        }
    }
}

