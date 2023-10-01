
using Humanizer.Localisation.TimeToClockNotation;
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


        public Order CreateOrderObject(int currentUserId)
        {
            Order order = new Order();
            order.UserId = currentUserId;
            _context.Add(order);
            _context.SaveChanges();

            return order;
        }

     
    }
}
