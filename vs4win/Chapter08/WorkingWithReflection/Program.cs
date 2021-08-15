using System.Reflection; // Assembly
using System.Runtime.CompilerServices; // to use CompilerGeneratedAttribute
using Packt.Shared; // CoderAttribute

using static System.Console;

WriteLine("Assembly metadata:");
Assembly? assembly = Assembly.GetEntryAssembly();
if (assembly is null)
{
  WriteLine("Failed to get entry assembly.");
  return;
}

WriteLine($"  Full name: {assembly.FullName}");
WriteLine($"  Location: {assembly.Location}");

IEnumerable<Attribute> attributes = assembly.GetCustomAttributes();

WriteLine($"  Assembly-level attributes:");
foreach (Attribute a in attributes)
{
  WriteLine($"    {a.GetType()}");
}

AssemblyInformationalVersionAttribute? version = assembly
  .GetCustomAttribute<AssemblyInformationalVersionAttribute>();

WriteLine($"  Version: {version?.InformationalVersion}");

AssemblyCompanyAttribute? company = assembly
  .GetCustomAttribute<AssemblyCompanyAttribute>();

WriteLine($"  Company: {company?.Company}");

WriteLine();
WriteLine($"* Types:");
Type[] types = assembly.GetTypes();

foreach (Type type in types)
{
  // optional exercise for compiler-generated code
  CompilerGeneratedAttribute? compilerGenerated = 
    type.GetCustomAttribute<CompilerGeneratedAttribute>();
  if (compilerGenerated != null) continue; // skip to next item

  WriteLine();
  WriteLine($"Type: {type.FullName}");
  MemberInfo[] members = type.GetMembers();

  foreach (MemberInfo member in members)
  {
    WriteLine("{0}: {1} ({2})",
      arg0: member.MemberType,
      arg1: member.Name,
      arg2: member.DeclaringType?.Name);

    IOrderedEnumerable<CoderAttribute> coders =
      member.GetCustomAttributes<CoderAttribute>()
      .OrderByDescending(c => c.LastModified);

    foreach (CoderAttribute coder in coders)
    {
      WriteLine("-> Modified by {0} on {1}",
        coder.Coder, coder.LastModified.ToShortDateString());
    }
  }
}

class Animal
{
  [Coder("Mark Price", "22 August 2021")]
  [Coder("Johnni Rasmussen", "13 September 2021")]
  public void Speak()
  {
    WriteLine("Woof...");
  }
}
