using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.DataBaseModels
{
    public partial class WordTranslation
    {
        public int Id { get; set; }
        public string WordTranslationName { get; set; }
        public string PronunciationPath { get; set; }
        public int WordId { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual Word Word { get; set; }
    }
}
