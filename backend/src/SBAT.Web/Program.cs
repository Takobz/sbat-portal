using SBAT.Infrastructure.ServiceCollection;
using SBAT.Web.Helpers;
using SBAT.Web.ServiceCollection;
using SBAT.Web.Settings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string _allowAll = "freeForAll";

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
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

//Add Infra dependencies
builder.Services.AddIdentityManager();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddRepositories();
InfraServiceCollectionExtension.AddInfraServices(builder.Services);

//Add Web.API services
ServiceCollectionExtensions.AddServices(builder.Services);
builder.Services.AddMappings();
builder.Services.AddModelValidations();

//Cors
//Add more specific cors policies.
builder.Services.AddCors(options =>  {
    options.AddPolicy(_allowAll, builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(err => err.UseCustomException(app.Environment));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(_allowAll);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
