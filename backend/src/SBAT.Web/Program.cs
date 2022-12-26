using SBAT.Infrastructure.ServiceCollection;
using SBAT.Web.Helpers;
using SBAT.Web.ServiceCollection;
using SBAT.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database services
var connectionStrings = builder.Configuration.
    GetSection(ConnectionStringsOptions.ConnectionStrings)
    .Get<ConnectionStringsOptions>();
if(connectionStrings is not null)
{
    builder.Services.AddDatabaseContext(connectionStrings!.SbatDatabase);
}

builder.Services.AddRepositories();

builder.Services.AddServices();
builder.Services.AddMappings();
builder.Services.AddModelValidations();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(err => err.UseCustomException(app.Environment));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
