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
builder.Services.AddHttpClient();
builder.Services.AddScoped<IShortIdFactory, ShortIdFactory>();

//builder.Services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=.app.sqlite");
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
    async (IHttpClientFactory clientFactory) =>
    {
        var httpClient = clientFactory.CreateClient();
        var response = await httpClient.PostAsJsonAsync(
            "http://localhost:5298/claim",
            new { ReferenceNumber = "1234567890", Currency = "USD" }
        );
        // Fetch the response
        var claim = await response.Content.ReadAsStringAsync();
        //var claim = await response.Content.ReadFromJsonAsync<ClaimGetResponse>();
        return Results.Ok(claim);
    }
);

app.MapPost(
    "/claim",
    async (
        [FromBody] ClaimCreateCommand request,
        IAppDbContext context,
        CancellationToken cancellationToken
    ) =>
    {
        var claim = new Claim
        {
            Id = new ClaimId(),
            ReferenceNumber = request.ReferenceNumber,
            Currency = new Currency(request.Currency)
        };
        context.Claims.Add(claim);
        await context.SaveChangesAsync(cancellationToken);

        return Results.Created(
            $"/claim/{claim.Id}",
            new ClaimGetResponse(claim.Id, claim.ReferenceNumber, claim.Currency)
        );
    }
);

app.MapGet(
    "/claim/{id}",
    async (IAppDbContext context, CancellationToken cancellationToken, [FromRoute] ClaimId id) =>
    {
        var result = await context.Claims.FindAsync(id, cancellationToken);
        if (result == null)
            return Results.NotFound();
        return Results.Ok(new ClaimGetResponse(result.Id, result.ReferenceNumber, result.Currency));
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
