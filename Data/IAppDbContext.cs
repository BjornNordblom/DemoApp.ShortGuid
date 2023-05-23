using Microsoft.EntityFrameworkCore;

public interface IAppDbContext
{
    DbSet<Product> Products { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
