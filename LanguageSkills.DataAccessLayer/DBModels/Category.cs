using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer
{
    public partial class Category
    {
        public Category()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            SubCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImagePath { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
