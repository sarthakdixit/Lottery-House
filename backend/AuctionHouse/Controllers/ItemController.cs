using AuctionHouse.Models;
using AuctionHouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ItemDetail>> GetItemDetail(int itemid)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            ItemDetail itemDetail = await _itemService.GetItemDetail(itemid, token);
            return Ok(itemDetail);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Item>>> GetActiveItems()
        {
            List<Item> items = await _itemService.GetActiveItems();
            return Ok(items);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Item>>> GetInactiveItems()
        {
            List<Item> items = await _itemService.GetInactiveItems();
            return Ok(items);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ItemDetail>>> GetItemsToChooseWinner()
        {
            List<ItemDetail> items = await _itemService.GetItemsToChooseWinner();
            return Ok(items);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertAnItem([FromBody] Item item)
        {
            await _itemService.InsertAnItem(item);
            return Ok();
        }
    }
}
