using Microsoft.AspNetCore.Mvc.RazorPages;
using PatatzaakSoftwareMVC.Data;

namespace PatatzaakSoftwareMVC.Models.PageModels
{
    public class ItemPageModel : PageModel
    {
        public List<Item> Items()
        {
            using (MainDb dbContext = new MainDb())
            {
                var items = dbContext.items.ToList();
                return items;
            }
        }
    }
}
