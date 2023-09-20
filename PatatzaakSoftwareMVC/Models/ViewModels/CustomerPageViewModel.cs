using Humanizer.Localisation.TimeToClockNotation;

namespace PatatzaakSoftwareMVC.Models.ViewModels
{
    public class CustomerPageViewModel
    {

        public Item CreateItemObject()
        {
            Item item = new Item();
            return item;
        }

        public OrderedItem CreateOrderedItemObject()
        {
            OrderedItem orderedItem = new OrderedItem();
            return orderedItem;
        }

        public Order CreateOrderObject()
        {
            Order order = new Order();
            return order;
        }

     
    }
}
