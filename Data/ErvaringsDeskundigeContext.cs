using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounts;

public class ErvaringsDeskundigeContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:AZURE_SQL_CONNECTIONSTRING");
    }
    public ErvaringsDeskundigeContext(DbContextOptions<ErvaringsDeskundigeContext> options)
        : base(options)
    {
    }

    public DbSet<Accounts.ErvaringsDeskundige> ErvaringsDeskundige { get; set; } = default!;
}
