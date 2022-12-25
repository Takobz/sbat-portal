using SBAT.Infrastructure.ServiceCollection;
using SBAT.Web.ServiceCollection;
using SBAT.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database services
//TODO: Fix warning for get Section: CS8602
var connectionStrings = builder.Configuration.
    GetSection(ConnectionStringsOptions.ConnectionStrings)
    .Get<ConnectionStringsOptions>();
builder.Services.AddDatabaseContext(connectionStrings.SbatDatabase);
builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddMappings();

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
