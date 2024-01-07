using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

// MappingProfiles class inherits from AutoMapper's Profile class.
// This class is used to define object-to-object mappings.
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Mapping from Auction entity to AuctionDto.
        // It includes members from the associated Item entity.
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);

        // Mapping from Item entity to AuctionDto.
        // This mapping is used when converting an Item to AuctionDto, typically when Item is a member of Auction.
        CreateMap<Item, AuctionDto>();

        // Mapping from CreateAuctionDto to Auction entity.
        // This mapping also includes a specific rule: map the CreateAuctionDto to the Item property of the Auction.
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));

        // Mapping from CreateAuctionDto to Item entity.
        // This is useful when a new auction is being created, and the DTO contains item details.
        CreateMap<CreateAuctionDto, Item>();
    }
}
