using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

// Defines an API controller with the route "api/auctions"
[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    // _context is an instance of AuctionDbContext. This provides the necessary methods and properties to interact with the database.
    private readonly AuctionDbContext _context;

    // _mapper is an instance of IMapper, a part of AutoMapper. It's used to map objects from one type to another, simplifying object transformations.
    private readonly IMapper _mapper;


    // Constructor injecting the database context and AutoMapper
    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Handles GET requests to retrieve all auction data
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        // Fetching auctions from the database including related items, ordered by item make
        var auctions = await _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();

        // Mapping the auctions to DTOs and returning them
        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    // Handles GET requests to retrieve a specific auction by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        // Fetching a single auction by ID including related items
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        // If auction is not found, return NotFound (404)
        if (auction == null) return NotFound();

        // Mapping the auction to a DTO and returning it
        return _mapper.Map<AuctionDto>(auction);
    }

    // Handles POST requests to create a new auction
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto);
        // TODO: Add current user as seller
        auction.Seller = "test";

        _context.Auctions.Add(auction);

        // Saving changes to the database and checking if successful
        var result = await _context.SaveChangesAsync() > 0;

        // If not successful, return BadRequest
        if (!result) return BadRequest("Could not save changes to the DB");

        // If successful, return the created auction data
        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));
    }

    // Handles PUT requests to update an existing auction
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        // If auction not found, return NotFound (404)
        if (auction == null) return NotFound();

        // TODO: check seller == username

        // Updating auction properties if they are provided
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

        // Saving changes and checking if successful
        var result = await _context.SaveChangesAsync() > 0;

        // If successful, return Ok
        if (result) return Ok();

        // If not successful, return BadRequest
        return BadRequest("Problem saving changes");
    }

    // Handles DELETE requests to remove an auction
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id);

        // If auction not found, return NotFound (404)
        if (auction == null) return NotFound();

        // TODO: check seller == username

        _context.Auctions.Remove(auction);

        // Saving changes and checking if successful
        var result = await _context.SaveChangesAsync() > 0;

        // If not successful, return BadRequest
        if (!result) return BadRequest("Could not update DB");

        // If successful, return Ok
        return Ok();
    }
}
