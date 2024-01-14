using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounts;
using BerichtenOpties;
using Onderzoeken;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class WesselWestSideContext : IdentityDbContext
{
    public WesselWestSideContext(DbContextOptions<WesselWestSideContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BenaderOptie>().HasData(new BenaderOptie { Id = "1", Type = "Website" }, new BenaderOptie { Id = "2", Type = "Bellen" }, new BenaderOptie { Id = "3", Type = "Email" });
        modelBuilder.Entity<BeperkingOptie>().HasData(new BeperkingOptie { Id = "1", Beperking = "Slechtzien" }, new BeperkingOptie { Id = "2", Beperking = "Doof" }, new BeperkingOptie { Id = "3", Beperking = "Verlamt" });
        modelBuilder.Entity<Hulpmiddelen>().HasData(new Hulpmiddelen { Id = "1", Middel = "Screen reader" }, new Hulpmiddelen { Id = "2", Middel = "Blinde geleide hond" }, new Hulpmiddelen { Id = "3", Middel = "Tolk" });

        modelBuilder.Entity<Beheerder>().HasData(
            new Beheerder { Id = "", GebruikersNaam = "Admin", UserName = "Admin", Email = "Admin@example.com", Wachtwoord = "Admin1/" },
            new Beheerder { Id = "", GebruikersNaam = "Beheerder", UserName = "Beheerder", Email = "Beheerder@example.com", Wachtwoord = "Beheerder1/" }
        // Add more accounts as needed
        );

        modelBuilder.Entity<Bedrijf>().HasData(
            new Bedrijf { Id = "", GebruikersNaam = "Bedrijf", UserName = "Bedrijf", Email = "Bedrijf@example.com", Informatie = "Dit is een bedrijf", Locatie = "Bedrijdstraat 1", URL = "google.com", Wachtwoord = "Bedrijf1/" }
        );

        modelBuilder.Entity<ErvaringsDeskundige>().HasData(
            new ErvaringsDeskundige { Id = "", UserName = "Gebruiker", GebruikersNaam = "test gebruiker", Wachtwoord = "Gebruiker1/", EmailAccount = "Test@email.com", Voornaam = "Test", Achternaam = "Gebruiker", GeboorteDatum = new DateTime(2000, 6, 10), PostCode = "1234 AB", TelefoonNummer = "0612345678" }
            // Add more accounts as needed
        );

        // var roleManager = new RoleManager<IdentityRole>(
        //     new RoleStore<IdentityRole>(new WesselWestSideContext(new DbContextOptions<WesselWestSideContext>())),
        //     null, null, null, null
        // );

        // string[] roleNames = { "beheerder", "bedrijf", "ervaringDeskundige" };
        // foreach (var roleName in roleNames)
        // {
        //     var roleExist = roleManager.RoleExistsAsync(roleName).Result;
        //     if (!roleExist)
        //     {
        //         // Create the roles and seed them to the database
        //         roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        //     }
        // }
        // var userManager = new UserManager<IdentityUser>(
        //     new UserStore<IdentityUser>(new WesselWestSideContext(new DbContextOptions<WesselWestSideContext>())),
        //     null, null, null, null, null, null, null, null
        // );

        // var gebruiker = userManager.FindByNameAsync("Gebruiker").Result;
        // userManager.AddToRoleAsync(gebruiker, "ervaringDeskundige").Wait();

        // var beheerder = userManager.FindByNameAsync("Beheerder").Result;
        // userManager.AddToRoleAsync(beheerder, "beheerder").Wait();

        // var bedrijf = userManager.FindByNameAsync("Bedrijf").Result;
        // userManager.AddToRoleAsync(bedrijf, "bedrijf").Wait();

        // var admin = userManager.FindByNameAsync("Admin").Result;
        // userManager.AddToRoleAsync(admin, "beheerder").Wait();
        // userManager.AddToRoleAsync(admin, "bedrijf").Wait();
        // userManager.AddToRoleAsync(admin, "ervaringDeskundige").Wait();
    }

    public DbSet<BenaderOptie> BenaderOptie { get; set; } = default!;

    public DbSet<BeperkingOptie> BeperkingOptie { get; set; } = default!;

    public DbSet<Beschikbaarheid> Beschikbaarheid { get; set; } = default!;

    public DbSet<ErvaringsDeskundige> ErvaringsDeskundige { get; set; } = default!;

    public DbSet<Google> Google { get; set; } = default!;

    public DbSet<Hulpmiddel> Hulpmiddel { get; set; } = default!;

    public DbSet<Hulpmiddelen> Hulpmiddelen { get; set; } = default!;

    public DbSet<Voogd> Voogd { get; set; } = default!;

    public DbSet<Account> Account { get; set; } = default!;

    public DbSet<Bedrijf> Bedrijf { get; set; } = default!;

    public DbSet<Beheerder> Beheerder { get; set; } = default!;

    public DbSet<Chat> Chat { get; set; } = default!;

    public DbSet<Email> Email { get; set; } = default!;

    public DbSet<Categorie> Categorie { get; set; } = default!;

    public DbSet<Onderzoek> Onderzoek { get; set; } = default!;

    public DbSet<SelectieCriterium> SelectieCriterium { get; set; } = default!;
}
