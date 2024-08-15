using MyOnlineTradingCenter.PersistenceLayer.Concretions.Extensions;
using FluentValidation.AspNetCore;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Products;
using FluentValidation;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Extensions;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Enums.StorageTypes;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.AzureStorages;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.LocalStorages;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Filters.Validations;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Extenions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

builder.Services.AddStorageServices<LocalStorage>();
//builder.Services.AddStorageServices(StorageType.LocalStorage);

//builder.Services.AddStorageServices<AzureStorage>();
//builder.Services.AddStorageServices(StorageType.AzureStorage);


builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
