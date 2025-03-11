Password Manager

A simple C# command-line Password Manager that securely generates, encrypts, stores, and retrieves passwords using AES-256 encryption.

Features

✅ Generate Secure Passwords – Uses cryptographic randomness for strong passwords.
✅ AES-256 Encryption – Ensures password safety using industry-standard encryption.
✅ Save Passwords Securely – Encrypts and stores passwords in a file.
✅ Retrieve & Decrypt Passwords – View stored passwords securely.
✅ Simple CLI Interface – Easy-to-use interactive command-line options.

Installation & Setup

1. Clone the repository:

git clone https://github.com/yourusername/password-manager.git
cd password-manager


2. Open the project in Visual Studio or any C# editor.


3. Build and run the project:

dotnet run



Usage

Run the program and choose an option:

1️⃣ Generate Password → Creates a strong password and displays it.
2️⃣ Save Password → Encrypts and stores a password for an account/service.
3️⃣ Retrieve Passwords → Decrypts and displays stored passwords.
4️⃣ Exit → Closes the application.

Security Considerations

🔒 Encryption Key: Change the default encryption key in the code to a more secure, randomly generated key.
🔒 IV (Initialization Vector): The IV is currently set to zero for simplicity; for better security, use a random IV.
🔒 File Storage: Ensure passwords.txt is stored securely and not accessible by unauthorized users.

Code Overview

GeneratePassword(int length) → Creates a random password using RNGCryptoServiceProvider.

Encrypt(string plainText) → Encrypts a password using AES-256 encryption.

Decrypt(string encryptedText) → Decrypts a stored password.

SavePassword() → Stores an encrypted password in a file.

RetrievePasswords() → Reads and decrypts saved passwords.


Contributing

💡 Found a bug? Want to add a new feature? Contributions are welcome! Fork the repository and submit a pull request.

License

📜 This project is licensed under the MIT License – see the LICENSE file for details.
