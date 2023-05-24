using DemoApp.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var configuration = new LoggerConfiguration().MinimumLevel
    .Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override(
        "Microsoft.EntityFrameworkCore",
        Serilog.Events.LogEventLevel.Information
    )
    .MinimumLevel.Override(
        "Microsoft.EntityFrameworkCore.Database.Command",
        Serilog.Events.LogEventLevel.Information
    )
    .WriteTo.Console()
    .Enrich.FromLogContext();
var logger = configuration.CreateLogger();
Log.Logger = logger;
var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(logger));
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(logger);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ShortIdValidationAttribute>();
    // options.Filters.Add<ValidationFilter>();
    // options.Filters.Add<ExceptionFilter>();
    // options.Filters.Add<LoggingFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddMediator();
builder.Services.AddScoped<IShortIdFactory, ShortIdFactory>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=:memory:");
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
    options.UseLoggerFactory(loggerFactory);
});
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
IAppDbContext context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

await context.Database.EnsureCreatedAsync();

app.UseSwagger();
app.UseSwaggerUI();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();

// app.MapControllers();
app.MapGet(
    "/",
    () =>
    {
        return Results.Ok();
    }
);

app.MapGet(
    "/product/{id}",
    async ([FromRoute] ProductId id) =>
    {
        return Results.Ok(id.Value);
    }
);

app.MapGet(
    "/invoice/{id}",
    async ([FromRoute] InvoiceId id) =>
    {
        return Results.Ok(id.Value);
    }
);
app.Run();
