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
        public WesselWestSideContext (DbContextOptions<WesselWestSideContext> options)
            : base(options)
        {
        }

        public DbSet<Accounts.Benadering> Benadering { get; set; } = default!;

        public DbSet<Accounts.Beperking> Beperking { get; set; } = default!;

        public DbSet<Accounts.BenaderOptie> BenaderOptie { get; set; } = default!;

        public DbSet<Accounts.BeperkingOptie> BeperkingOptie { get; set; } = default!;

        public DbSet<Accounts.Beschikbaarheid> Beschikbaarheid { get; set; } = default!;

        public DbSet<Accounts.ErvaringsDeskundige> ErvaringsDeskundige { get; set; } = default!;

        public DbSet<Accounts.Hulpmiddel> Hulpmiddel { get; set; } = default!;

        public DbSet<Accounts.TypeCategorie> TypeCategorie { get; set; } = default!;

        public DbSet<Accounts.Voogd> Voogd { get; set; } = default!;

        public DbSet<Accounts.Account> Account { get; set; } = default!;

        public DbSet<Accounts.Bedrijf> Bedrijf { get; set; } = default!;

        public DbSet<Accounts.Beheerder> Beheerder { get; set; } = default!;

        public DbSet<BerichtenOpties.Chat> Chat { get; set; } = default!;

        public DbSet<BerichtenOpties.Email> Email { get; set; } = default!;

        public DbSet<Onderzoeken.Categorie> Categorie { get; set; } = default!;

        public DbSet<Onderzoeken.Onderzoek> Onderzoek { get; set; } = default!;

        public DbSet<Onderzoeken.SelectieCriterium> SelectieCriterium { get; set; } = default!;
    }
