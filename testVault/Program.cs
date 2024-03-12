

using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.Commons;
using VaultSharp;
using testVault.interfaces;
using testVault.Services;

namespace testVault
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            if (builder.Configuration.GetSection("Vault")["Role"] != null)
            {
                builder.Configuration.AddVault(options =>
                {
                    var vaultOptions = builder.Configuration.GetSection("Vault");
                    options.Address = vaultOptions["Address"];
                    options.Role = vaultOptions["Role"];
                    options.MountPath = vaultOptions["MountPath"];
                    options.SecretType = vaultOptions["SecretType"];
                    options.roleID = vaultOptions["roleID"];
                    options.secretID = vaultOptions["secretID"];
                    options.Secret = builder.Configuration.GetSection("VAULT_SECRET_ID").Value;
                });
            }

            builder.Services.AddTransient<IexampleService, exampleService>();

            var pass = builder.Configuration["database:Username"] + "\n" + builder.Configuration["database:Password"];

            if (pass != null) 
            {
                var app = builder.Build();

                app.MapGet("/", () => pass);

                app.Run();
            }
        }

    }
}



