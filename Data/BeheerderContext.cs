using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounts;

public class BeheerderContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:AZURE_SQL_CONNECTIONSTRING");
    }
    public BeheerderContext(DbContextOptions<BeheerderContext> options)
        : base(options)
    {
    }

    public DbSet<Accounts.Beheerder> Beheerder { get; set; } = default!;
}
