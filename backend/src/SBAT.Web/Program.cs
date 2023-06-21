using SBAT.Infrastructure.ServiceCollection;
using SBAT.Web.Helpers;
using SBAT.Web.ServiceCollection;
using SBAT.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string _allowAll = "freeForAll";

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
        builder.WithOrigins("https://localhost:5003", "http://localhost:5003");
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(_allowAll);
app.MapControllers();

app.Run();
