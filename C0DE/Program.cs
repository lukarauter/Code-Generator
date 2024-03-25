using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Password Generator");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Instructions: ");
        Console.WriteLine("- Choose a length of your password. Then if it includes numbers, special character or uppercase letter.");
        Console.WriteLine("- The program also shows how safe your newly created your password is.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Enter the length of the password:");
        int length = InputValidator.GetValidIntegerInput();

        Console.WriteLine("Include numbers? (Y/N)");
        bool includeNumbers = InputValidator.GetValidYesNoInput();

        Console.WriteLine("Include special characters? (Y/N)");
        bool includeSpecialChars = InputValidator.GetValidYesNoInput();

        Console.WriteLine("Include uppercase letters? (Y/N)");
        bool includeUppercase = InputValidator.GetValidYesNoInput();

        // Generate the password
        string password = GeneratePassword(length, includeNumbers, includeSpecialChars, includeUppercase);

        // Save the password to a file
        string filePath = "password.txt";
        SavePasswordToFile(password, filePath);

        Console.WriteLine("Password generated successfully and saved to password.txt.");
        Console.WriteLine("Password: " + password);
        Console.WriteLine("Please copy the password from the file.");

        Console.ReadLine();
    }

    static string GeneratePassword(int length, bool includeNumbers, bool includeSpecialChars, bool includeUppercase)
    {
        StringBuilder password = new StringBuilder();

        string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";
        string specialChars = "!@#$%^&*()_-+=<>?";

        string chars = lowercaseChars;

        if (includeNumbers)
        {
            chars += numbers;
        }

        if (includeSpecialChars)
        {
            chars += specialChars;
        }

        if (includeUppercase)
        {
            chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            password.Append(chars[random.Next(chars.Length)]);
        }

        return password.ToString();
    }

    static void SavePasswordToFile(string password, string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Generated Password: " + password);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving the password: " + ex.Message);
        }
    }
}

class InputValidator
{
    public static int GetValidIntegerInput()
    {
        int result;
        bool isValidInput = false;

        do
        {
            string input = Console.ReadLine();
            isValidInput = int.TryParse(input, out result);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer:");
            }
        } while (!isValidInput);

        return result;
    }

    public static bool GetValidYesNoInput()
    {
        bool isValidInput = false;
        string input;

        do
        {
            input = Console.ReadLine();
            isValidInput = input.Equals("Y", StringComparison.OrdinalIgnoreCase) || input.Equals("N", StringComparison.OrdinalIgnoreCase);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter Y or N:");
            }
        } while (!isValidInput);

        return input.Equals("Y", StringComparison.OrdinalIgnoreCase);
    }
}
