using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer
{
    public partial class Language
    {
        public Language()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            SubCategoryTranslations = new HashSet<SubCategoryTranslation>();
            TestTranslations = new HashSet<TestTranslation>();
            WordTranslations = new HashSet<WordTranslation>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<SubCategoryTranslation> SubCategoryTranslations { get; set; }
        public virtual ICollection<TestTranslation> TestTranslations { get; set; }
        public virtual ICollection<WordTranslation> WordTranslations { get; set; }
    }
}
