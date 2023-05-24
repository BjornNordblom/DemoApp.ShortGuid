using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public interface IAppDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Claim> Claims { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
