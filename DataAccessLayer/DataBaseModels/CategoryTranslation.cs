using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.DataBaseModels
{
    public partial class CategoryTranslation
    {
        public int Id { get; set; }
        public string CategoryTranslationName { get; set; }
        public int CategotyId { get; set; }
        public int LanguageId { get; set; }

        public virtual Category Categoty { get; set; }
        public virtual Language Language { get; set; }
    }
}
