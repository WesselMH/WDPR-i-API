using WDPR_i_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OnderzoekContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnderzoekContext") ?? throw new InvalidOperationException("Connection string 'OnderzoekContext' not found.")));
builder.Services.AddDbContext<ErvaringsDeskundigeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ErvaringsDeskundigeContext") ?? throw new InvalidOperationException("Connection string 'ErvaringsDeskundigeContext' not found.")));
builder.Services.AddDbContext<BeheerderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeheerderContext") ?? throw new InvalidOperationException("Connection string 'BeheerderContext' not found.")));
builder.Services.AddDbContext<BedrijfContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BedrijfContext") ?? throw new InvalidOperationException("Connection string 'BedrijfContext' not found.")));


// Add services to the container.

builder.Services.AddControllers();
// builder.Services.AddDbContext<WdprIDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
