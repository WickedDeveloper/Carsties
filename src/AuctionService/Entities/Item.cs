using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

// The [Table("Items")] annotation specifies the database table name for this entity.
[Table("Items")]
public class Item
{
    // Unique identifier for each item.
    public Guid Id { get; set; }

    // Make of the item, for example, the manufacturer or brand.
    public string Make { get; set; }

    // Model of the item, further specifying the type or version.
    public string Model { get; set; }

    // Year of manufacture or release of the item.
    public int Year { get; set; }

    // Color of the item.
    public string Color { get; set; }

    // Mileage of the item, applicable for vehicles.
    public int Mileage { get; set; }

    // URL of the image representing the item.
    public string ImageUrl { get; set; }

    // Navigation property to the Auction entity.
    // This establishes a relationship between Item and Auction, indicating that an Item is part of an Auction.
    public Auction Auction { get; set; }

    // Foreign key linking to the Auction entity.
    // This is the identifier of the Auction to which this Item belongs.
    public Guid AuctionId { get; set; }
}
