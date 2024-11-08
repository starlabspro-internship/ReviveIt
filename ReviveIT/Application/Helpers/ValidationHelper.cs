using System.Text.RegularExpressions;
using Application.DTO;

namespace Application.Helpers
{
    public static class ValidationHelper
    {
        private static readonly string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private static readonly string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

        public static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, EmailPattern);

        public static bool IsValidPassword(string password) =>
            Regex.IsMatch(password, PasswordPattern);

        public static bool ValidateRegisterDto(RegisterDto dto, out string message)
        {
            if (!IsValidEmailInput(dto.Email, out message)) return false;
            if (!IsValidPasswordInput(dto.Password, dto.ConfirmPassword, out message)) return false;

            message = string.Empty;
            return true;
        }

        private static bool IsValidEmailInput(string email, out string message)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                message = "Invalid email format.";
                return false;
            }
            message = string.Empty;
            return true;
        }

        private static bool IsValidPasswordInput(string password, string confirmPassword, out string message)
        {
            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
            {
                message = "Password must contain at least 8 characters, including uppercase, lowercase, at least one number and an non alphanumeric character.";
                return false;
            }

            if (password != confirmPassword)
            {
                message = "Passwords do not match.";
                return false;
            }
            message = string.Empty;
            return true;
        }
    }
}
