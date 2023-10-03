
using Humanizer.Localisation.TimeToClockNotation;
using PatatzaakSoftwareMVC.Data;
using System.ComponentModel.DataAnnotations;

namespace PatatzaakSoftwareMVC.Models.ViewModels
{
    public class CustomerPageViewModel
    {

        private readonly MainDb _context;

        [Display(Name = "Selected Voucher")]
        public int? SelectedVoucherId { get; set; }

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
