# Development Notes

 I am creating this file because I want to keep track of any notes that I find out about during development.

 I have had a lot of information become available such as how to create a secret storage on local, how to migrate entities into the DB using ef, etc.

 These such commands I do not want to forget incase I want to make a new migration, remove a migration, add or change a secret on the local secret storage, and many others that I am sure will come up


 ## Adding secrets to the secret manager on local

 https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0&tabs=windows%2Cpowershell
 https://www.youtube.com/watch?v=Vp44MP1SPy8

 This is not a very important things to hide because it's just the local db so I will leave it here just like this. All appsettings.json files can be turned into this format.
 Once they are set like this you can remove them from the appsettings.json file and they will be hidden from now on.
 ```powershell
 dotnet user-secrets set "ConnectionStrings:TwistedDB" "Server=localhost;Database=Twisted_Storage;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
 ```

 you can follow this convention for adding any secret you want to the secret manager.

 To check what secrets are currently in the user secrets you can run this command:
 ```powershell
 dotnet user-secrets list
 ```

 ## Adding and Removing Migrations

 If you want to add a migration you can run this command:
 ```powershell
 Add-Migration ExampleEntity - Package Manager Console
 dotnet ef migrations add ExampleEntity --project <ProjectNameFilePath> (Infrastructure Project) --startup-project <StartupProjectNameFilePath> (API Project) - Command Line
 ```

 Using Package manager console you can do this method:

 Update-Database - Package Manager Console
 Remove-Migration - Package Manager Console
 Add-Migration <Name of Migration> - Package Manager Console

 ## Documentation for adding migration, binding to context, and using ef core with code first implementation

 https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-10.0 - Getting started
 https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations?view=aspnetcore-10.0 - Code First Migrations
 https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/data-annotations - Data Annotaions for Entities

 ## Using static classes

 I decided to use a static class instead of the DB for storing the OAuth2 token from Bnet. I will probably do that for all token since storing them in kind of unsecure and it doesn't make a lot of sense to do that.

 ## Notes about how to use AES and RSA encryption

 If you ever forget just F1 on the aes and rsa object and it takes you to the documentation to view how it works with some snippets

 https://learn.microsoft.com/en-us/dotnet/standard/security/encrypting-data - encrypting
 https://learn.microsoft.com/en-us/dotnet/standard/security/decrypting-data - decrypting
 https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-10.0&devlangs=csharp&f1url=%3FappId%3DDev18IDEF1%26l%3DEN-US%26k%3Dk%28System.Security.Cryptography.Aes%29%3Bk%28DevLang-csharp%29%26rd%3Dtrue - aes calss
 insert rsa class when I get there

 ## Information and documents about hashing

 https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.ipasswordhasher-1?view=aspnetcore-10.0&devlangs=csharp&f1url=%3FappId%3DDev18IDEF1%26l%3DEN-US%26k%3Dk%28Microsoft.AspNetCore.Identity.IPasswordHasher%601%29%3Bk%28DevLang-csharp%29%26rd%3Dtrue
 https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-10.0

 string key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
 $key = [Convert]::ToBase64String([Security.Cryptography.RandomNumberGenerator]::GetBytes(32)); dotnet user-secrets set "ApiKey" $key --project .NET_10/Twisted_Relay/API; $key

 simple code above that generates a random 32 byte array then converts to base64. Also the method I used above to generate that base64 and insert into the user secrets in one command. I really like that one!