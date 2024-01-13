using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounts;
using BerichtenOpties;
using Onderzoeken;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class WesselWestSideContext : IdentityDbContext
{
    public WesselWestSideContext(DbContextOptions<WesselWestSideContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BenaderOptie>().HasData(new BenaderOptie { Type = "Website" }, new BenaderOptie { Type = "Bellen" }, new BenaderOptie { Type = "Email" });
        modelBuilder.Entity<BeperkingOptie>().HasData(new BeperkingOptie { Beperking = "Slechtzien" }, new BeperkingOptie { Beperking = "Doof" }, new BeperkingOptie { Beperking = "Verlamt" });
        modelBuilder.Entity<Hulpmiddelen>().HasData(new Hulpmiddelen { Middel = "Screen reader" }, new Hulpmiddelen { Middel = "Blinde geleide hond" }, new Hulpmiddelen { Middel = "Tolk" });

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
