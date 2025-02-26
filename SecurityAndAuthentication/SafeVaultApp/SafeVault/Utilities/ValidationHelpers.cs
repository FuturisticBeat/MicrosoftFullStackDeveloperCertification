using Microsoft.Security.Application;

namespace SafeVault.Utilities
{
    /// <summary>
    /// Provides helper methods for input validation.
    /// </summary>
    public static class ValidationHelpers
    {
        /// <summary>
        /// Validates the input string by sanitizing it and ensuring it contains only
        /// alphanumeric characters or allowed special characters.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="allowedSpecialCharacters">A string of additional allowed special characters. Optional.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        public static bool IsValidInput(string input, string allowedSpecialCharacters = "")
        {
            // Sanitize the input string to remove unsafe HTML content.
            string sanitizedInput = Sanitizer.GetSafeHtmlFragment(input);

            // Return false if the sanitized input is null or empty.
            if (string.IsNullOrEmpty(sanitizedInput))
            {
                return false;
            }

            // Create a set of allowed characters, including additional special characters.
            HashSet<char> validCharacters = allowedSpecialCharacters.ToHashSet();

            // Check if all characters in the input string are valid.
            return input.All(c => char.IsLetterOrDigit(c) || validCharacters.Contains(c));
        }
    }
}