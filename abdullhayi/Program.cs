using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class PasswordManager
{
    private const string FilePath = "passwords.txt";
    private const string EncryptionKey = "mysecurekey12345mysecurekey12345"; // Must be 32 bytes for AES-256

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nPassword Manager");
            Console.WriteLine("1. Generate Password");
            Console.WriteLine("2. Save Password");
            Console.WriteLine("3. Retrieve Passwords");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    string password = GeneratePassword(12);
                    Console.WriteLine($"Generated Password: {password}");
                    break;
                case "2":
                    SavePassword();
                    break;
                case "3":
                    RetrievePasswords();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static string GeneratePassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
        var password = new StringBuilder();
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] data = new byte[length];
            rng.GetBytes(data);
            for (int i = 0; i < length; i++)
            {
                password.Append(chars[data[i] % chars.Length]);
            }
        }
        return password.ToString();
    }

    static void SavePassword()
    {
        Console.Write("Enter account/service name: ");
        string account = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        string encryptedData = Encrypt($"{account}: {password}");
        File.AppendAllText(FilePath, encryptedData + Environment.NewLine);

        Console.WriteLine("Password saved securely!");
    }

    static void RetrievePasswords()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("No saved passwords found.");
            return;
        }

        string[] encryptedLines = File.ReadAllLines(FilePath);
        Console.WriteLine("\nStored Passwords:");
        foreach (string line in encryptedLines)
        {
            Console.WriteLine(Decrypt(line));
        }
    }

    static byte[] GetEncryptionKey()
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(EncryptionKey);
        Array.Resize(ref keyBytes, 32); // Ensure it's 32 bytes for AES-256
        return keyBytes;
    }

    static string Encrypt(string plainText)
    {
        byte[] keyBytes = GetEncryptionKey();
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = new byte[16]; // Zero IV for simplicity
            using (var encryptor = aes.CreateEncryptor())
            {
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    static string Decrypt(string encryptedText)
    {
        byte[] keyBytes = GetEncryptionKey();
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = new byte[16]; // Zero IV for simplicity
            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
