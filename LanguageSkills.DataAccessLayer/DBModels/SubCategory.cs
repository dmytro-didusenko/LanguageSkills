using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            SubCategoryTranslations = new HashSet<SubCategoryTranslation>();
            Words = new HashSet<Word>();
        }

        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryImagePath { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<SubCategoryTranslation> SubCategoryTranslations { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}
