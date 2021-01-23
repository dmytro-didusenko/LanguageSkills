using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.DataBaseModels
{
    public partial class SubCategoryTranslation
    {
        public int Id { get; set; }
        public string SubCategoryTranslationName { get; set; }
        public int SubCategoryId { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
