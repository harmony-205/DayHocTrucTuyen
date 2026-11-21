using System.ComponentModel.DataAnnotations;
using DayHocTrucTuyen.Models;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class LoginModelTests
    {
        [Fact]
        public void Validate_ValidEmailAndPassword_PassesValidation()
        {
            var model = new LoginModel
            {
                Email = "user@example.com",
                Password = "SecretPassword123"
            };

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_NullOrEmptyEmail_FailsValidationWithEmailRequired(string? email)
        {
            var model = new LoginModel
            {
                Email = email,
                Password = "ValidPassword123"
            };

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(LoginModel.Email)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_NullOrEmptyPassword_FailsValidationWithPasswordRequired(string? password)
        {
            var model = new LoginModel
            {
                Email = "user@example.com",
                Password = password
            };

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(LoginModel.Password)));
        }
    }
}
