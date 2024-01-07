namespace AuctionService.Entities;

// The 'Status' enum defines the possible states of an auction.
public enum Status
{
    // The auction is currently active and open for bids.
    Live,

    // The auction has ended. This status can be set after the auction end time has passed or if the auction has been manually closed.
    Finished,

    // The auction has ended, but the reserve price set by the seller was not met.
    // This status is used to indicate that the highest bid did not reach the minimum amount the seller was willing to accept.
    ReserveNotMet
}
