using AuctionHouse.Models;
using AuctionHouse.Repositories;
using System;
using System.Security.Claims;

namespace AuctionHouse.Services
{
    public class WinnerService : IWinnerService
    {
        public readonly IWinnerRepository _winnerRepository;
        public readonly IInvestmentRespository _investmentRespository;
        private readonly ITokenService _tokenService;
        private readonly IItemRepository _itemRepository;

        public WinnerService(IWinnerRepository winnerRepository, IInvestmentRespository investmentRespository, ITokenService tokenService, IItemRepository itemRepository)
        {
            _winnerRepository = winnerRepository;
            _investmentRespository = investmentRespository;
            _tokenService = tokenService;
            _itemRepository = itemRepository;
        }

        public async Task<Investment> ChooseWinner(int itemid)
        {
            List<Investment> list = await _investmentRespository.GetInvestmentsOfUniqueItemId(itemid);
            if (list.Count == 0)
                throw new Exception("No one participated in this lottery");

            var random = new Random();
            int index = random.Next(list.Count);

            Investment investment = list[index];

            return investment;
        }

        public async Task<List<ItemDetail>> GetWinsUsingUserId(string token)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
            int userid = Convert.ToInt32(principal.FindFirstValue("Id"));

            List<Investment> list = await _winnerRepository.GetWinsUsingUserId(userid);

            List<ItemDetail> itemDetailsList = new List<ItemDetail>();

            for (int i = 0; i < list.Count; i++)
            {
                int itemid = list[i].ItemId;
                Item item = await _itemRepository.GetAnItem(itemid);
                List<Investment> investmentsList = await _investmentRespository.GetInvestmentsOfUniqueItemId(itemid);
                int count = investmentsList.Count;
                ItemDetail itemDetail = new ItemDetail
                {
                    Item = item,
                    PaymentCount = count
                };
                itemDetailsList.Add(itemDetail);
            }
            return itemDetailsList;
        }

        public async Task InsertAWinner(Investment investment)
        {
            investment.CreatedAt = DateTime.UtcNow;
            await _winnerRepository.InsertAWinner(investment);
        }
    }
}
