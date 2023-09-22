
using PatatzaakSoftwareMVC.Data;

namespace PatatzaakSoftwareMVC.Models.ViewModels
{
    public class CustomerPageViewModel
    {

        private readonly MainDb _context;

        public CustomerPageViewModel(MainDb context)
        {
            _context = context;
        }
        public Item CreateItemObject()
        {
            Item item = new Item();
            return item;
        }


        public Order CreateOrderObject()
        {
            Order order = new Order();
            _context.Add(order);
            _context.SaveChanges();

            return order;
        }

     
    }
}
