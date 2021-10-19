using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;

using static System.Convert;
using static System.Console;

namespace Packt.Shared
{
  public static class Protector
  {
    // salt size must be at least 8 bytes, we will use 16 bytes
    private static readonly byte[] salt =
      Encoding.Unicode.GetBytes("7BANANAS");

    // iterations should be high enough to take at least 100ms to 
    // generate a Key and IV on the target machine. 150,000 iterations
    // takes 131ms on my 11th Gen Intel Core i7-1165G7 @ 2.80GHz.
    private static readonly int iterations = 150_000;

    public static string Encrypt(
      string plainText, string password)
    {
      byte[] encryptedBytes;
      byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

      using (Aes aes = Aes.Create()) // abstract class factory method
      {
        // record how long it takes to generate the Key and IV
        Stopwatch timer = Stopwatch.StartNew();

        using (Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations))
        {
          aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key 
          aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV 
        }

        timer.Stop();

        WriteLine("{0:N0} milliseconds to generate Key and IV using {1:N0} iterations.",
          arg0: timer.ElapsedMilliseconds,
          arg1: iterations);

        using (MemoryStream ms = new())
        {
          using (ICryptoTransform transformer = aes.CreateEncryptor())
          {
            using (CryptoStream cs = new(
              ms, transformer, CryptoStreamMode.Write))
            {
              cs.Write(plainBytes, 0, plainBytes.Length);
            }
          }
          encryptedBytes = ms.ToArray();
        }
      }

      return ToBase64String(encryptedBytes);
    }

    public static string Decrypt(
      string cipherText, string password)
    {
      byte[] plainBytes;
      byte[] cryptoBytes = FromBase64String(cipherText);

      using (Aes aes = Aes.Create())
      {
        using (Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations))
        {
          aes.Key = pbkdf2.GetBytes(32);
          aes.IV = pbkdf2.GetBytes(16);
        }

        using (MemoryStream ms = new())
        {
          using (ICryptoTransform transformer = aes.CreateDecryptor())
          {
            using (CryptoStream cs = new(
              ms, transformer, CryptoStreamMode.Write))
            {
              cs.Write(cryptoBytes, 0, cryptoBytes.Length);
            }
          }
          plainBytes = ms.ToArray();
        }
      }

      return Encoding.Unicode.GetString(plainBytes);
    }

    private static Dictionary<string, User> Users = new();

    public static User Register(
      string username, string password, 
      string[]? roles = null)
    {
      // generate a random salt
      RandomNumberGenerator rng = RandomNumberGenerator.Create();
      byte[] saltBytes = new byte[16];
      rng.GetBytes(saltBytes);
      string saltText = ToBase64String(saltBytes);

      // generate the salted and hashed password 
      string saltedhashedPassword = SaltAndHashPassword(password, saltText);

      User user = new(username, saltText, 
        saltedhashedPassword, roles);

      Users.Add(user.Name, user);

      return user;
    }

    // check a user's password that is stored
    // in the private static dictionary Users
    public static bool CheckPassword(string username, string password)
    {
      if (!Users.ContainsKey(username))
      {
        return false;
      }

      User u = Users[username];

      return CheckPassword(password, 
        u.Salt, u.SaltedHashedPassword);
    }

    // check a user's password using salt and hashed password
    public static bool CheckPassword(string password, 
      string salt, string hashedPassword)
    {
      // re-generate the salted and hashed password 
      string saltedhashedPassword = SaltAndHashPassword(
        password, salt);

      return (saltedhashedPassword == hashedPassword);
    }

    private static string SaltAndHashPassword(string password, string salt)
    {
      using (SHA256 sha = SHA256.Create())
      {
        string saltedPassword = password + salt;
        return ToBase64String(sha.ComputeHash(
          Encoding.Unicode.GetBytes(saltedPassword)));
      }
    }

    public static string? PublicKey;

    public static string? ToXmlStringExt(
      this RSA rsa, bool includePrivateParameters)
    {
      RSAParameters rsap = rsa.ExportParameters(includePrivateParameters);

      XElement xml;

      if (includePrivateParameters)
      {
        xml = new XElement("RSAKeyValue",
          new XElement("Modulus", ToBase64String(rsap.Modulus)),
          new XElement("Exponent", ToBase64String(rsap.Exponent)),
          new XElement("P", ToBase64String(rsap.P)),
          new XElement("Q", ToBase64String(rsap.Q)),
          new XElement("DP", ToBase64String(rsap.DP)),
          new XElement("DQ", ToBase64String(rsap.DQ)),
          new XElement("InverseQ", ToBase64String(rsap.InverseQ))
        );
      }
      else
      {
        xml = new XElement("RSAKeyValue",
          new XElement("Modulus", ToBase64String(rsap.Modulus)),
          new XElement("Exponent", ToBase64String(rsap.Exponent)));
      }
      return xml?.ToString();
    }

    public static void FromXmlStringExt(
      this RSA rsa, string parametersAsXml)
    {
      XDocument xml = XDocument.Parse(parametersAsXml);
      XElement? root = xml.Element("RSAKeyValue");

      if (root is null) return;

      RSAParameters rsap = new()
      {
        Modulus = FromBase64String(root.Element("Modulus").Value),
        Exponent = FromBase64String(root.Element("Exponent").Value)
      };

      if (root.Element("P") != null)
      {
        rsap.P = FromBase64String(root.Element("P").Value);
        rsap.Q = FromBase64String(root.Element("Q").Value);
        rsap.DP = FromBase64String(root.Element("DP").Value);
        rsap.DQ = FromBase64String(root.Element("DQ").Value);
        rsap.InverseQ = FromBase64String(root.Element("InverseQ").Value);
      }
      rsa.ImportParameters(rsap);
    }

    public static string GenerateSignature(string data)
    {
      byte[] dataBytes = Encoding.Unicode.GetBytes(data);
      SHA256 sha = SHA256.Create();
      byte[] hashedData = sha.ComputeHash(dataBytes);
      RSA rsa = RSA.Create();

      PublicKey = rsa.ToXmlString(false); // exclude private key

      return ToBase64String(rsa.SignHash(hashedData,
        HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
    }

    public static bool ValidateSignature(
      string data, string signature)
    {
      if (PublicKey is null) return false;
      byte[] dataBytes = Encoding.Unicode.GetBytes(data);
      SHA256 sha = SHA256.Create();
      byte[] hashedData = sha.ComputeHash(dataBytes);
      byte[] signatureBytes = FromBase64String(signature);
      RSA rsa = RSA.Create();
      rsa.FromXmlString(PublicKey);
      return rsa.VerifyHash(hashedData, signatureBytes,
        HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    public static byte[] GetRandomKeyOrIV(int size)
    {
      RandomNumberGenerator r = RandomNumberGenerator.Create();
      byte[] data = new byte[size];
      r.GetBytes(data);

      // data is an array now filled with 
      // cryptographically strong random bytes
      return data;
    }

    public static void LogIn(string username, string password)
    {
      if (CheckPassword(username, password))
      {
        GenericIdentity gi = new(
          name: username, type: "PacktAuth");

        GenericPrincipal gp = new(
          identity: gi, roles: Users[username].Roles);

        // set the principal on the current thread so that
        // it will be used for authorization by default
        Thread.CurrentPrincipal = gp;
      }
    }
  }
}
