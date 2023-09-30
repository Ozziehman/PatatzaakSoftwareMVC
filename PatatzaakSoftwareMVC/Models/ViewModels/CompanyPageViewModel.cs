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
    }
}
