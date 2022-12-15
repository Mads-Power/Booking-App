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
            var pattern = "^((\\d{4}-\\d{2}-\\d{2})T\\d{2}:\\d{2}:\\d{2}(.\\d{1,5})?(Z|\\+\\d{2}(:\\d{2})?))$";
            Match m = Regex.Match(date, pattern);

            if (!m.Success) return new ValidationResult(false, "Invalid date string, date must follow ISO 8601 format and be UTC");

            return new ValidationResult(true);
        }

        public override string ToString()
        {
            return $"Result: {Result}, Reason: {RejectionReason}";
        }
    }
}

