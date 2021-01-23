using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer
{
    public partial class Word
    {
        public Word()
        {
            WordTranslations = new HashSet<WordTranslation>();
        }

        public int Id { get; set; }
        public string WordName { get; set; }
        public string WordImagePath { get; set; }
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<WordTranslation> WordTranslations { get; set; }
    }
}
