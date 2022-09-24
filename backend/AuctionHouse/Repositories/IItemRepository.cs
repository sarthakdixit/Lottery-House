using AuctionHouse.Models;

namespace AuctionHouse.Repositories
{
    public interface IItemRepository
    {
        public Task<Item> GetAnItem(int id);
        public Task InsertAnItem(Item item);
        public Task<List<Item>> GetActiveItems();
        public Task<List<Item>> GetInactiveItems();
        public Task<List<Item>> GetItemsToChooseWinner();
    }
}
