using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<InvoiceId>().HaveConversion<InvoiceIdConverter>();
        configurationBuilder.Properties<ProductId>().HaveConversion<ProductIdConverter>();
    }
}
