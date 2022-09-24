using AuctionHouse.Models;
using AuctionHouse.Repositories;
using System.Security.Claims;

namespace AuctionHouse.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ITokenService _tokenService;
        private readonly IInvestmentRespository _investmentRespository;

        public ItemService(IItemRepository itemRepository, ITokenService tokenService, IInvestmentRespository investmentRespository)
        {
            _itemRepository = itemRepository;
            _tokenService = tokenService;
            _investmentRespository = investmentRespository;
        }

        public async Task<List<Item>> GetActiveItems()
        {
            List<Item> items = await _itemRepository.GetActiveItems();
            if (items.Count == 0)
                throw new Exception("No active items");
            return items;
        }

        public async Task<Item> GetAnItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Item>> GetInactiveItems()
        {
            List<Item> items = await _itemRepository.GetInactiveItems();
            if (items.Count == 0)
                throw new Exception("No inactive items");
            return items;
        }

        public async Task<ItemDetail> GetItemDetail(int itemid, string token)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
            int userid = Convert.ToInt32(principal.FindFirstValue("Id"));

            Item item = await _itemRepository.GetAnItem(itemid);
            if (item == null)
                throw new Exception("Invalid item");

            List<Investment> investmentsList = await _investmentRespository.GetInvestmentsOfUniqueItemId(itemid);
            int count = investmentsList.Count;

            bool flag = false;
            Investment investment = await _investmentRespository.GetInvestmentUsingUserIdAndItemId(userid, itemid);
            if (investment != null)
                flag = true;

            ItemDetail itemDetail = new ItemDetail
            {
                Item = item,
                PaymentCount = count,
                UserPaid = flag
            };

            return itemDetail;
        }

        public async Task<List<ItemDetail>> GetItemsToChooseWinner()
        {
            List<Item> items = await _itemRepository.GetItemsToChooseWinner();
            if (items.Count == 0)
                throw new Exception("No items found");

            List<ItemDetail> list = new List<ItemDetail>();
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                List<Investment> investmentsList = await _investmentRespository.GetInvestmentsOfUniqueItemId(item.Id);
                int count = investmentsList.Count;
                ItemDetail itemDetail = new ItemDetail
                {
                    Item = item,
                    PaymentCount = count
                };
                list.Add(itemDetail);
            }
            return list;
        }

        public async Task InsertAnItem(Item item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;
            if (item.TimeFrame == "1D")
                item.ExpiredAt = DateTime.UtcNow.AddDays(1);
            if (item.TimeFrame == "1W")
                item.ExpiredAt = DateTime.UtcNow.AddDays(7);
            if (item.TimeFrame == "1M")
                item.ExpiredAt = DateTime.UtcNow.AddMonths(1);
            if (item.TimeFrame == "1Y")
                item.ExpiredAt = DateTime.UtcNow.AddYears(1);
            await _itemRepository.InsertAnItem(item);
        }
    }
}
