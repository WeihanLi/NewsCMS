using System.Collections.Generic;

namespace Models.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public int ParentId { get; set; }

        public string CategoryName { get; set; }

        public List<CategoryViewModel> Children { get; set; }
    }
}
