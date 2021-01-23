using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer
{
    public partial class TestTranslation
    {
        public int Id { get; set; }
        public string TestTranslationName { get; set; }
        public int TestId { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual Test Test { get; set; }
    }
}
