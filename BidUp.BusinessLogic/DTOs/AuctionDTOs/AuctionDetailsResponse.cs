using BidUp.DataAccess.Entites;

namespace BidUp.BusinessLogic.DTOs.AuctionDTOs;

public class AuctionDetailsResponse
{
    public int Id { get; init; }
    public required string ProductName { get; init; }
    public required string ProductDescription { get; init; }
    public ProductCondition ProductCondition { get; init; }
    public decimal CurrentPrice { get; init; }
    public decimal MinBidIncrement { get; set; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public required string Category { get; init; }
    public required string City { get; init; }
    public required Auctioneer Auctioneer { get; init; }
    public required IEnumerable<string> Images { get; init; }
    public int? WinnerId { get; init; } // To allow the client to know if the auction has ended with a winner or not before sending a request to api/auctions/{auctionId}/bids/accepted-bid endpoint
}


public class Auctioneer
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? ProfilePictureUrl { get; init; }
}