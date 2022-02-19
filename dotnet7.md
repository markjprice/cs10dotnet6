# Sixth Edition's support for .NET 7
Microsoft will release previews of .NET 7 regularly until the final version on November 7, 2022.

- [Download .NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- November 7, 2022: Announcing .NET 7.0 — The Bestest .NET Yet
- October 2022: Announcing .NET 7 Release Candidate 2
- September 2022: Announcing .NET 7 Release Candidate 1
- August 2022: Announcing .NET 7 Preview 7
- July 2022: Announcing .NET 7 Preview 6
- June 2022: Announcing .NET 7 Preview 5
- May 2022: Announcing .NET 7 Preview 4
- April 2022: Announcing .NET 7 Preview 3
- March 2022: Announcing .NET 7 Preview 2
- February 17, 2022: [Announcing .NET 7 Preview 1](https://devblogs.microsoft.com/dotnet/announcing-net-7-preview-1/)

## All Chapters
After [downloading](https://dotnet.microsoft.com/download/dotnet/7.0) and installing .NET 7.0 SDK, follow the step-by-step instructions in the book and they should work as expected since the project file will automatically reference .NET 7.0 as the target framework. 

To upgrade a project in the GitHub repository from .NET 6.0 to .NET 7.0 just requires a target framework change in your project file.

Change this:
```
<TargetFramework>net6.0</TargetFramework>
```
To this:
```
<TargetFramework>net7.0</TargetFramework>
```
For projects that reference additional NuGet packages, use the latest NuGet package version, as shown in the rest of this page, instead of the version given in the book. You can search for the correct NuGet package version numbers yourself at the following link: https://www.nuget.org

## Chapter 4 - Writing, Debugging, and Testing Functions
For the `Instrumenting` project, the additional referenced NuGet packages should use the .NET 7.0 versions, as shown in the following markup: 
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="7.0.0-*" />
  </ItemGroup>

</Project>
```
For the `CalculatorLibUnitTests` project, the additional referenced NuGet packages for unit testing can use the latest versions, as shown in the following markup:
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.0.0" />
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />
    <PackageReference Include="coverlet.collector" Version="3.1.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference 
      Include="..\CalculatorLib\CalculatorLib.csproj" />
  </ItemGroup>

</Project>
```
## Chapter 10 - Working with Databases Using Entity Framework Core
For the `WorkingWithEFCore` project, the additional referenced NuGet packages should use the .NET 7.0 versions, as shown in the following markup:
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Proxies" Version="7.0.0-*" />
  </ItemGroup>

</Project>
```
## Chapter 11 - Querying and Manipulating Data Using LINQ
For the `LinqWithEFCore` and `Exercise02` projects, the additional referenced NuGet package should use the .NET 7.0 version, as shown in the following markup:
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.0-*" />
  </ItemGroup>

</Project>
```
## Chapter 13 - Practical Applications of C# and .NET
For the `NorthwindContextLib` project, the referenced NuGet package for SQLite should use the .NET 7.0 version, as shown in the following markup:
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\NorthwindEntitiesLib\NorthwindEntitiesLib.csproj" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SQLite" Version="7.0.0-*" />
  </ItemGroup>

</Project>
```
## Chapter 15 - Building Websites Using the Model-View-Controller Pattern
For the `NorthwindMvc` project, the referenced NuGet packages should use the .NET 7.0 versions, as shown in the following markup:
```
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <UserSecretsId>aspnet-NorthwindMvc-72F8E5E5-AF15-4520-91A9-EF8090AF2961</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <None Update="app.db" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.UI" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.0-*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.0-*" />
    
    <!-- added in Chapter 17 to call a web service -->
    <PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\NorthwindContextLib\NorthwindContextLib.csproj" />
  </ItemGroup>

</Project>
```

## Chapter 16 - Building and Consuming Web Services
For the `NorthwindService` project, the referenced NuGet packages should use the latest versions, as shown in the following markup:
```
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\NorthwindContextLib\NorthwindContextLib.csproj" />

    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
    <PackageReference Include="Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore" 
                      Version="7.0.0-*" />
  </ItemGroup>

</Project>
```
