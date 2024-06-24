using BidUp.BusinessLogic.DTOs.AuctionDTOs;
using BidUp.BusinessLogic.DTOs.BidDTOs;
using BidUp.BusinessLogic.DTOs.CommonDTOs;

namespace BidUp.Presentation.Hubs;

public interface IAppHubClient
{
    Task AuctionCreated(AuctionResponse createdAuction);
    Task AuctionDeletedOrEnded(int auctionId);
    Task BidCreated(BidResponse createdBid);
    Task ErrorOccurred(ErrorResponse error);
}
