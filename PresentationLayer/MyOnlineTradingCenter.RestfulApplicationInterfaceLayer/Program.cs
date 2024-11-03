using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Contracts.Cofigurations;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Extenions;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Products;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Extensions;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.Filters.Validations;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.StorageServices.Storages.LocalStorages;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Extensions;
using MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;
using MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.SeriLogs.ColumnWriters;
using MyOnlineTradingCenter.SignalRLayer.Concretions.Extensions;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

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


builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name
        };
    });

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSql"), "Logs", needAutoCreateTable: true, columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message", new RenderedMessageColumnWriter() },
        {"message_template", new MessageTemplateColumnWriter() },
        {"level", new LevelColumnWriter() },
        {"time_stamp", new TimestampColumnWriter() },
        {"exception", new ExceptionColumnWriter() },
        {"log_event", new LogEventSerializedColumnWriter() },
        {"user_name", new UserNameColumnWriter() }
    })
    .WriteTo.Seq(builder.Configuration["SeqLoggerUserInterface:SeqServerUrl"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;    
});

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

app.Use(async (context, next) =>
{
    var userName = context.User?.Identity?.IsAuthenticated is not null || true ? context?.User?.Identity?.Name : string.Empty;
    LogContext.PushProperty("user_name", userName);
    await next();
});

app.AddMapHubRegistrations();

app.MapControllers();

app.Run();
