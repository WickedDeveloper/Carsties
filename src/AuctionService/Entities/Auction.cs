namespace AuctionService.Entities;

// This class represents the Auction entity, defining the structure for auctions in the application.
public class Auction
{
    // Unique identifier for each auction.
    public Guid Id { get; set; }

    // The minimum reserve price set for the auction.
    public int ReservePrice { get; set; }

    // Username or identifier of the seller who created the auction.
    public string Seller { get; set; }

    // Username or identifier of the winner of the auction (if any).
    public string Winner { get; set; }

    // The final amount for which the auction was sold.
    public int? SoldAmount { get; set; }

    // The current highest bid amount in the auction.
    public int? CurrentHighBid { get; set; }

    // The date and time when the auction was created.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // The date and time when the auction was last updated.
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // The ending date and time for the auction.
    public DateTime AuctionEnd { get; set; }

    // The status of the auction, based on an enumeration (e.g., Live, Ended, Cancelled).
    public Status Status { get; set; }

    // The item being auctioned. This is a reference to an 'Item' entity.
    public Item Item { get; set; }
}
