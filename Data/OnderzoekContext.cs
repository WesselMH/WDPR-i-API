using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Onderzoeken;

public class OnderzoekContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:AZURE_SQL_CONNECTIONSTRING");
    }
    public OnderzoekContext(DbContextOptions<OnderzoekContext> options)
        : base(options)
    {
    }

    public DbSet<Onderzoeken.Onderzoek> Onderzoek { get; set; } = default!;
}
