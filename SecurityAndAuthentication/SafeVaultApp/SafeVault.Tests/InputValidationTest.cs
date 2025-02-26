using SafeVault.Utilities;

namespace SafeVault.Tests
{
    /// <summary>
    /// Contains unit tests for input validation.
    /// </summary>
    public class InputValidationTest
    {
        /// <summary>
        /// Tests the IsValidInput method with various input scenarios to ensure it validates input correctly.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="expectedResult">The expected result of the validation.</param>
        /// <param name="allowedSpecialCharacters">A string of additional allowed special characters (optional).</param>
        [Theory]
        [Trait("Category", "InputValidation")]
        [InlineData("ValidInput123", true)]
        [InlineData("<script>alert('XSS')</script>", false)]
        [InlineData("ValidInput123!", false)]
        [InlineData("ValidInput123!", true, "!@#$%^&*?")]
        [InlineData("", false)]
        [InlineData("   ", false)]
        public void IsValidInput_ShouldValidateInputCorrectly(string input, bool expectedResult, string allowedSpecialCharacters = "")
        {
            // Act
            bool result = ValidationHelpers.IsValidInput(input, allowedSpecialCharacters);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}