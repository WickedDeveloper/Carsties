using Microsoft.EntityFrameworkCore;
using AuctionService.Entities;

namespace AuctionService.Data;

// AuctionDbContext is a subclass of DbContext provided by Entity Framework Core.
// It serves as the data access layer to the database, allowing interaction with Auction entities.
public class AuctionDbContext : DbContext
{
    // Constructor accepting DbContextOptions, which is typically configured in the application's startup class.
    // This constructor calls the base DbContext class's constructor with the provided options.
    public AuctionDbContext(DbContextOptions options) : base(options)
    {
    }

    // A DbSet property representing the Auctions table in the database.
    // This property is used to query and save instances of the Auction entity.
    public DbSet<Auction> Auctions { get; set; }
}
