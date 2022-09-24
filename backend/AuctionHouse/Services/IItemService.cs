using AuctionHouse.Models;

namespace AuctionHouse.Services
{
    public interface IItemService
    {
        public Task<Item> GetAnItem(int id);
        public Task InsertAnItem(Item item);
        public Task<List<Item>> GetActiveItems();
        public Task<List<Item>> GetInactiveItems();
        public Task<List<ItemDetail>> GetItemsToChooseWinner();
        public Task<ItemDetail> GetItemDetail(int itemid, string token);
    }
}
