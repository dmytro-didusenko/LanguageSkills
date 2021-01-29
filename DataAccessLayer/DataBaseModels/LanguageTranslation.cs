using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.DataBaseModels
{
    public partial class LanguageTranslation
    {
        public int Id { get; set; }
        public string LanguageTranslationName { get; set; }
        public int LanguageWordId { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual Language LanguageWord { get; set; }
    }
}
