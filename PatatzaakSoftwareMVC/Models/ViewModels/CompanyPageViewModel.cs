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
        public List<Order> GetPlacedOrders()
        {
            List<Order> orders = _context.orders.Where(o => o.Status == "Placed").ToList();
            return orders;
        }
        public List<Order> GetReadyOrders()
        {
            List<Order> orders = _context.orders.Where(o => o.Status == "Ready").ToList();
            return orders;
        }

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

        public string GetCustomerNameForOrder(int Id)
        {
            var order = _context.orders.Find(Id);
            var user = _context.users.Find(order.UserId);
            return user.Name;
        }
    }
}
