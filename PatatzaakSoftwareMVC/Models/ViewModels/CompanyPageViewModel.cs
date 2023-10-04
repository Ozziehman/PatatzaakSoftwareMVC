using PatatzaakSoftwareMVC.Data;

namespace PatatzaakSoftwareMVC.Models.ViewModels
{
    public class CompanyPageViewModel
    {
        private readonly MainDb _context;

        public CompanyPageViewModel(MainDb context)
        {
            _context = context;
        }


        /// <summary>
        /// Collects all placed orders to display on the company page where orders can be processed
        /// </summary>
        /// <returns></returns>
        public List<Order> GetPlacedOrders()
        {
            List<Order> orders = _context.orders.Where(o => o.Status == "Placed").ToList();
            return orders;
        }

        /// <summary>
        /// Collects all the ready orders to display on the company page where it shows that an order is ready for pickup
        /// </summary>
        /// <returns></returns>
        public List<Order> GetReadyOrders()
        {
            List<Order> orders = _context.orders.Where(o => o.Status == "Ready").ToList();
            return orders;
        }

        /// <summary>
        /// Gets items for each order to display in dropdown for an order
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Item> GetItemsForOrder(int Id)
        {
            List<Item> itemListForOrder = new List<Item>();
            foreach(var orderedItem in _context.orderedItems.Where(o => o.OrderId == Id).ToList())
            {
                var item = _context.items.Find(orderedItem.ItemId);
                itemListForOrder.Add(item);
            }
            return itemListForOrder;
        }

        /// <summary>
        /// Gets customer name that belongs to the order to display in the dropdown for an order
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetCustomerNameForOrder(int Id)
        {
            var order = _context.orders.Find(Id);
            var user = _context.users.Find(order.UserId);
            return user.Name;
        }
    }
}
