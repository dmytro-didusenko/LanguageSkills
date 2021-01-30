using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class LanguageTranslationRepository : ICrudRepository<LanguageTranslation>
    {
        private LanguageSkillsDBContext _db;
        public LanguageTranslationRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<LanguageTranslation> GetAll()
        {
            return _db.LanguageTranslations;
        }

        public LanguageTranslation Get(int id)
        {
            return _db.LanguageTranslations.Find(id);
        }

        public void CreateRange(List<LanguageTranslation> languageTranslations)
        {
            _db.LanguageTranslations.AddRange(languageTranslations);
        }

        public void Update(LanguageTranslation languageTranslation)
        {
            _db.Entry(languageTranslation).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var languageTranslation = _db.LanguageTranslations.Find(id);
            if (languageTranslation != null)
                _db.LanguageTranslations.Remove(languageTranslation);
        }
    }
}
