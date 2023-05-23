using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.MapControllers();
app.MapGet(
    "/",
    () =>
    {
        var guids = new List<ShortGuid>();
        for (int i = 0; i < 10; i++)
        {
            var g = Guid.NewGuid();
            var sg = new ShortGuid(g);
            guids.Add(sg);
        }
        return Results.Ok(guids);
    }
);

app.MapGet(
    "/{id}",
    ([FromRoute] ShortGuid id) =>
    {
        return Results.Ok((Guid)id);
    }
);

app.MapGet(
    "/product/{id}",
    async ([FromRoute] ProductId id) =>
    {
        return Results.Ok(id.Guid);
    }
);
app.Run();
