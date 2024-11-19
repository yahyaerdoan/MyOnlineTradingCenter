using FluentValidation;
using FluentValidation.AspNetCore;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Contracts.Cofigurations;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Extenions;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Products;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Users;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Extensions;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Filters.Validations;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.LocalStorages;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Extensions;
using MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;
using MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Middlewares;
using MyOnlineTradingCenter.SignalRLayer.Concretions.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Add custom validation filter
    options.Filters.Add<ValidationFilter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Register FluentValidation services
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<CreateProductValidator>();

// Register Persistance Layer services
builder.Services.AddPersistanceServiceRegistrations();

builder.Services.AddInfrastructureServiceRegistrations();

builder.Services.AddApplicationServiceRegistrations();

builder.Services.AddSignalRServiceRegistrations();

builder.Services.AddStorageServices<LocalStorage>();

//builder.Services.AddStorageServices(StorageType.LocalStorage);
//builder.Services.AddStorageServices<AzureStorage>();
//builder.Services.AddStorageServices(StorageType.AzureStorage);

builder.Services.AddCorsServiceRegistrations();

builder.Services.AddAuthenticationServiceRegistrations(builder.Configuration);

builder.Services.AddLoggerConfigurationServiceRegistrations(builder.Configuration);

builder.Host.UseSerilog();

builder.Services.AddHttpLoggingServiceRegistrations();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddOptions<StorageSettings>().BindConfiguration("StorageSettings");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCongigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseLoggedUserRegistrations();

app.AddMapHubRegistrations();

app.MapControllers();

app.Run();
