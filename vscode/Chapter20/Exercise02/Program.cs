using System.Xml; // XmlWriter
using Packt.Shared; // Protector

using static System.Console;
using static System.IO.Path;
using static System.Environment;

WriteLine("You must enter a password to encrypt the sensitive data in the document.");
WriteLine("You must enter the same passord to decrypt the document later.");
Write("Password: ");
string? password = ReadLine();

// define two example customers and
// note they have the same password
List<Customer> customers = new()
{
  new()
  {
    Name = "Bob Smith",
    CreditCard = "1234-5678-9012-3456",
    Password = "Pa$$w0rd",
  },
  new()
  {
    Name = "Leslie Knope",
    CreditCard = "8002-5265-3400-2511",
    Password = "Pa$$w0rd",
  }
};

// define an XML file to write to
string xmlFile = Combine(CurrentDirectory,
  "..", "protected-customers.xml");

XmlWriter xmlWriter = XmlWriter.Create(xmlFile,
  new XmlWriterSettings { Indent = true });

xmlWriter.WriteStartDocument();

xmlWriter.WriteStartElement("customers");

foreach (Customer c in customers)
{
  xmlWriter.WriteStartElement("customer");
  xmlWriter.WriteElementString("name", c.Name);

  // to protect the credit card number we must encrypt it
  xmlWriter.WriteElementString("creditcard",
    Protector.Encrypt(c.CreditCard, password));

  // to protect the password we must salt and hash it
  // and we must store the random salt used
  User u = Protector.Register(c.Name, c.Password);
  xmlWriter.WriteElementString("password", u.SaltedHashedPassword);
  xmlWriter.WriteElementString("salt", u.Salt);

  xmlWriter.WriteEndElement();
}
xmlWriter.WriteEndElement();
xmlWriter.WriteEndDocument();
xmlWriter.Close();

WriteLine();
WriteLine("Contents of the protected file:");
WriteLine();
WriteLine(File.ReadAllText(xmlFile));
