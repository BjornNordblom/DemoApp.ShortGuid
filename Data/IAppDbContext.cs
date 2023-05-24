using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public interface IAppDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Product> Products { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
