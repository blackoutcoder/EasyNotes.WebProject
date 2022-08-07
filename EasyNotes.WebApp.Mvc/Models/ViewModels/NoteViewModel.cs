using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyNotes.WebApp.Mvc.Models.ViewModels
{
    public class NoteViewModel
    {
        public Note? VNote { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<SelectListItem>? CSelectListItem(IEnumerable<Category> Items)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item = new SelectListItem
            {
                Text = "----Select----",
                Value = "0"
            };
            list.Add(item);
            foreach (Category category in Items)
            {
                item = new SelectListItem
                {
                    Text = category.CategoryName,
                    Value = category.Id.ToString(),
                };
                list.Add(item);
            }
            return list;
        }
    }
}
