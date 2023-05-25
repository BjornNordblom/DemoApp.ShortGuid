using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Claim> Claims { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;

    public DbSet<ClaimHistory> ClaimHistories { get; set; } = null!;
    public DbSet<DebtorHistory> DebtorHistories { get; set; } = null!;

    // public DbSet<PaymentAllocation> PaymentAllocations { get; set; } = null!;
    // public DbSet<InvoicePaymentAllocation> InvoicePaymentAllocations { get; set; } = null!;

    // public DbSet<CostPayment> CostPayments { get; set; } = null!;
    // public DbSet<InvoicePayment> InvoicePayments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<InvoiceId>().HaveConversion<InvoiceIdConverter>();
        configurationBuilder.Properties<ClaimId>().HaveConversion<ClaimIdConverter>();
        configurationBuilder.Properties<DebtorId>().HaveConversion<DebtorIdConverter>();
        configurationBuilder.Properties<Currency>().HaveConversion<CurrencyConverter>();
        configurationBuilder.Properties<PaymentId>().HaveConversion<PaymentIdConverter>();
        configurationBuilder
            .Properties<PaymentAllocationId>()
            .HaveConversion<PaymentAllocationIdConverter>();
    }
}
