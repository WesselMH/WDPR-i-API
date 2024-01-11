using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Accounts;
using Microsoft.AspNetCore.Identity;

namespace WDPR_i_API;

public static class DataSeeder
{
    public static async Task<WebApplication> SeedAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<WesselWestSideContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                context.Database.EnsureCreated();

                var users = context.Account.FirstOrDefault();
                if (users == null)
                {
                    var admin = new Beheerder { Id = 0, UserName = "Admin", GebruikersNaam = "Admin", Wachtwoord = "Admin1/", EmailAccount = "Admin@email.com" };

                    var beheerder = new Beheerder { Id = 0, UserName = "Beheerder", GebruikersNaam = "Beheerder", Wachtwoord = "Beheerder1/", EmailAccount = "Beheerder@email.com" };

                    var bedrijf = new Bedrijf { Id = 0, UserName = "Bedrijf", GebruikersNaam = "Bedrijf", Wachtwoord = "Bedrijf1/", EmailAccount = "Bedrijf@email.com", Informatie = "Dit is een bedrijf", Locatie = "BedrijfStraat 100", URL = "Youtube.com" };

                    var gebruiker = new ErvaringsDeskundige { Id = 0, UserName = "Gebruiker", GebruikersNaam = "Gebruiker", Wachtwoord = "Gebruiker1/", EmailAccount = "Gebruiker@email.com", Voornaam = "Ge", Achternaam = "bruiker", GeboorteDatum = new DateTime(2003, 12, 30), PostCode = "1234 AB", TelefoonNummer = "0612345678" };

                    if (!await roleManager.RoleExistsAsync("beheerder"))
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = "beheerder" });
                    }
                    if (!await roleManager.RoleExistsAsync("bedrijf"))
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = "bedrijf" });
                    }
                    if (!await roleManager.RoleExistsAsync("ervaringsDeskundige"))
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = "ervaringsDeskundige" });
                    }

                    await userManager.AddToRoleAsync(admin, "beheerder");
                    await userManager.AddToRoleAsync(admin, "bedrijf");
                    await userManager.AddToRoleAsync(admin, "ervaringsDeskundige");

                    await userManager.AddToRoleAsync(beheerder, "beheerder");
                    await userManager.AddToRoleAsync(bedrijf, "bedrijf");
                    await userManager.AddToRoleAsync(gebruiker, "ervaringsDeskundige");

                    context.Account.AddRange(
                        admin,
                        beheerder,
                        bedrijf,
                        gebruiker
                    );

                    context.SaveChanges();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return app;
        }
    }
}