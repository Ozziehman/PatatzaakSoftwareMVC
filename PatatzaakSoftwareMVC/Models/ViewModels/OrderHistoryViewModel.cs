using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;

namespace PatatzaakSoftwareMVC.Models.ViewModels
{
    public class OrderHistoryViewModel
    {
        private readonly MainDb _context;

        public OrderHistoryViewModel(MainDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets items for each order to display in dropdown for an order
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Item> GetItemsForOrder(int Id)
        {
            List<Item> itemListForOrder = new List<Item>();
            foreach (var orderedItem in _context.orderedItems.Where(o => o.OrderId == Id).ToList())
            {
                var item = _context.items.Find(orderedItem.ItemId);
                itemListForOrder.Add(item);
            }
            return itemListForOrder;
        }
    }
}
