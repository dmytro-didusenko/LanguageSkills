using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer
{
    public partial class Test
    {
        public Test()
        {
            TestTranslations = new HashSet<TestTranslation>();
        }

        public int Id { get; set; }
        public string TestName { get; set; }

        public virtual ICollection<TestTranslation> TestTranslations { get; set; }
    }
}
