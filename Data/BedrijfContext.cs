using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounts;

public class BedrijfContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:AZURE_SQL_CONNECTIONSTRING");
    }

    public BedrijfContext(DbContextOptions<BedrijfContext> options)
        : base(options)
    {
    }

    public DbSet<Accounts.Bedrijf> Bedrijf { get; set; } = default!;
}
