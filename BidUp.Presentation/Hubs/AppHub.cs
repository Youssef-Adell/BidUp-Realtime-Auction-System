using System.Security.Claims;
using BidUp.BusinessLogic.DTOs.BidDTOs;
using BidUp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace BidUp.Presentation.Hubs;

public class AppHub : Hub<IAppHubClient>
{
    private readonly IBiddingService biddingService;

    public AppHub(IBiddingService biddingService)
    {
        this.biddingService = biddingService;
    }


    [Authorize]
    public async Task BidUp(BidRequest bidRequest)
    {
        var userId = int.Parse(Context.UserIdentifier!);

        var result = await biddingService.BidUp(userId, bidRequest);

        if (!result.Succeeded)
        {
            await Clients.Caller.ErrorOccurred(result.Error!);
            return;
        }

        var createdBid = result.Response!;
        var auctionGroup = createdBid.AuctionId.ToString();

        await Clients.Group(auctionGroup).BidCreated(createdBid); // Notify clients who currently in the page of this auction
        await Clients.Group("AuctionsFeed").AuctionPriceUpdated(createdBid.AuctionId, createdBid.Amount);
    }

    [Authorize]
    public async Task AcceptBid(int bidId)
    {
        var userId = int.Parse(Context.UserIdentifier!);

        var result = await biddingService.AcceptBid(userId, bidId);

        if (!result.Succeeded)
        {
            await Clients.Caller.ErrorOccurred(result.Error!);
            return;
        }

        var acceptedBid = result.Response!;
        var auctionGroup = acceptedBid.AuctionId.ToString();

        await Clients.Group(auctionGroup).BidAccepted(acceptedBid); // Notify clients who currently in the page of this auction
        await Clients.Group("AuctionsFeed").AuctionDeletedOrEnded(acceptedBid.AuctionId);
    }


    // The client must call this method when the auction page loads to be able to receive bidding updates in realtime
    public async Task JoinAuctionRoom(int auctionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, auctionId.ToString());
    }

    // The client must call this method when the auction page is about to be closed to stop receiving unnecessary bidding updates
    public async Task LeaveAuctionRoom(int auctionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionId.ToString());
    }

    // The client must call this method when the feed page loads to be able to receive feed updates in realtime
    public async Task JoinAuctionsFeedRoom()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "AuctionsFeed");
    }

    // The client must call this method when the feed page is about to be closed to stop receiving unnecessary feed updates
    public async Task LeaveAuctionsFeedRoom()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AuctionsFeed");
    }
}
