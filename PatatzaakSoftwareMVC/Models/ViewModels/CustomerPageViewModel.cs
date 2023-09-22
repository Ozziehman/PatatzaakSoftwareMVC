
namespace PatatzaakSoftwareMVC.Models.ViewModels
{
    public class CustomerPageViewModel
    {
    
        public Item CreateItemObject()
        {
            Item item = new Item();
            return item;
        }

        public Order CreateOrderObject()
        {
            Order order = new Order();
            return order;
        }

     
    }
}
