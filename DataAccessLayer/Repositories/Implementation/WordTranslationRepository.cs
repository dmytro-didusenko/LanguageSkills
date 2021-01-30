using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class WordTranslationRepository : ICrudRepository<WordTranslation>
    {
        private LanguageSkillsDBContext _db;
        public WordTranslationRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<WordTranslation> GetAll()
        {
            return _db.WordTranslations;
        }

        public WordTranslation Get(int id)
        {
            return _db.WordTranslations.Find(id);
        }

        public void CreateRange(List<WordTranslation> wordTranslations)
        {
            _db.WordTranslations.AddRange(wordTranslations);
        }

        public void Update(WordTranslation wordTranslation)
        {
            _db.Entry(wordTranslation).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var wordTranslation = _db.WordTranslations.Find(id);
            if (wordTranslation != null)
                _db.WordTranslations.Remove(wordTranslation);
        }
    }
}
