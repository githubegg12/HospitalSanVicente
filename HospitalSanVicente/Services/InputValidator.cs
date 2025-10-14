using System.Text.RegularExpressions;

namespace ClinicaSalud.Services;

public class InputValidator
{    
    // Constant keyword used to cancel input
    private const string CancelKeyword = "cancel";
    // Checks if the input is the cancel keyword and throws an exception to stop the process

    private static void CheckCancel(string? input)
    {
        if (input?.Trim().Equals(CancelKeyword, StringComparison.OrdinalIgnoreCase) == true)
            throw new OperationCanceledException("Input was cancelled by the user.");
    }
    // Reads a non-empty string input, ensuring it contains only letters or spaces
    public static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input is non-empty and contains only letters or white spaces
            if (!string.IsNullOrWhiteSpace(input) && input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                return input;

            Console.WriteLine("Input cannot be empty or a number. Please try again.");
        }
    }
    // Reads a non-negative integer input and ensures it is greater than zero
    public static int ReadNonNegativeInt(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input is a valid integer and greater than zero
            if (int.TryParse(input, out int value) && value > 0)
                return value;

            Console.WriteLine("Invalid input. Please enter a non-negative integer.");
        }
    }
    // Reads a GUID input and ensures the format is valid
    public static Guid ReadGuid(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input can be parsed as a valid GUID
            if (Guid.TryParse(input, out Guid result))
                return result;

            Console.WriteLine("Invalid GUID format. Please enter a valid GUID.");
        }
    }
    // Reads an alphanumeric string input (letters and digits only)
    public static string ReadAlphanumericString(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input is non-empty and contains letters or digits
            if (!string.IsNullOrWhiteSpace(input) && input.Any(char.IsLetterOrDigit))
                return input;

            Console.WriteLine("Input must not be empty and should contain letters or numbers.");
        }
    }
    // Reads an email input and ensures the format is valid
    public static string ReadEmail(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input matches the regular expression for a valid email format
            if (!string.IsNullOrWhiteSpace(input) &&
                Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return input;

            Console.WriteLine("Invalid email format. Try again.");
        }
    }
    // Reads a yes or no input, ensuring the response is valid (Y/N)
    public static bool ReadYesOrNo(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (Y/N, type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input is 'Y' or 'N', case-insensitive
            if (!string.IsNullOrWhiteSpace(input) &&
                (input.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                 input.Equals("N", StringComparison.OrdinalIgnoreCase)))
            {
                return input.Equals("Y", StringComparison.OrdinalIgnoreCase);
            }

            Console.WriteLine("Please enter 'Y' or 'N'.");
        }
    }
    // Reads an optional validated string input, allowing the user to skip it (press Enter)
    public static string? ReadOptionalValidatedString(string prompt, Func<string, bool> validateFunc, string errorMessage)
    {
        Console.Write($"{prompt} (press Enter to skip, or type '{CancelKeyword}' to cancel): ");
        string? input = Console.ReadLine();
        CheckCancel(input);
        // If the input is empty or whitespace, it is treated as a skipped entry
        if (string.IsNullOrWhiteSpace(input))
            return null;
        // If the input passes validation, return the input; otherwise, show an error message
        if (validateFunc(input))
            return input;

        Console.WriteLine(errorMessage);
        return null;
    }    
    
    // Reads a DateTime input and ensures the format is correct
    public static DateTime ReadDateTime(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (format: yyyy-MM-dd HH:mm, type '{CancelKeyword}' to cancel): ");
            string? input = Console.ReadLine();
            CheckCancel(input);
            // Validates if the input can be parsed into a valid DateTime
            if (DateTime.TryParse(input, out var result))
                return result;

            Console.WriteLine("Invalid date/time format. Please try again.");
        }
    }
}
