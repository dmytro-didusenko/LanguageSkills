using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class TestTranslationRepository : ICrudRepository<TestTranslation>
    {
        private LanguageSkillsDBContext _db;
        public TestTranslationRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<TestTranslation> GetAll()
        {
            return _db.TestTranslations;
        }

        public TestTranslation Get(int id)
        {
            return _db.TestTranslations.Find(id);
        }

        public void CreateRange(List<TestTranslation> testTranslations)
        {
            _db.TestTranslations.AddRange(testTranslations);
        }

        public void Update(TestTranslation testTranslation)
        {
            _db.Entry(testTranslation).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var testTranslation = _db.TestTranslations.Find(id);
            if (testTranslation != null)
                _db.TestTranslations.Remove(testTranslation);
        }
    }
}
