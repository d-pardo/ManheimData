using System.Linq;

namespace ManheimData
{
    public static class FieldsValidation
    {
        public static bool IsAlphanumeric(string value)
        {
            var hasDigits = value.Where(char.IsDigit).Count() > 0;
            var hasLetters = value.Where(char.IsLetter).Count() > 0;

            if (hasDigits && hasLetters)
                return true;

            return false;
        }
    }
}