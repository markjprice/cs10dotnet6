using Packt.Shared; // Protector

using static System.Console;

WriteLine("Registering Alice with Pa$$w0rd.");
User alice = Protector.Register("Alice", "Pa$$w0rd");

WriteLine($"Name: {alice.Name}");
WriteLine($"Salt: {alice.Salt}");
WriteLine("Password (salted and hashed): {0}",
  arg0: alice.SaltedHashedPassword);
WriteLine();

Write("Enter a new user to register: ");
string? username = ReadLine();

Write($"Enter a password for {username}: ");
string? password = ReadLine();

if ((username is null) || (password is null))
{
  WriteLine("Username or password cannot be null.");
  return;
}

WriteLine("Registering a new user:");
User newUser = Protector.Register(username, password);
WriteLine($"Name: {newUser.Name}");
WriteLine($"Salt: {newUser.Salt}");
WriteLine("Password (salted and hashed): {0}",
  arg0: newUser.SaltedHashedPassword);
WriteLine();

bool correctPassword = false;

while (!correctPassword)
{
  Write("Enter a username to log in: ");
  string? loginUsername = ReadLine();

  Write("Enter a password to log in: ");
  string? loginPassword = ReadLine();

  if ((loginUsername is null) || (loginPassword is null))
  {
    WriteLine("Login username or password cannot be null.");
    return;
  }
  
  correctPassword = Protector.CheckPassword(
    loginUsername, loginPassword);

  if (correctPassword)
  {
    WriteLine($"Correct! {loginUsername} has been logged in.");
  }
  else
  {
    WriteLine("Invalid username or password. Try again.");
  }
}