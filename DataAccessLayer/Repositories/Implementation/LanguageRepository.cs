using System.Collections.Generic;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class LanguageRepository : ICrudRepository<Language>
    {
        private LanguageSkillsDBContext _db;
        public LanguageRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<Language> GetAll()
        {
            return _db.Languages;
        }

        public Language Get(int id)
        {
            return _db.Languages.Find(id);
        }

        public void Create(Language language)
        {
            _db.Languages.Add(language);
        }

        public void Update(Language language)
        {
            _db.Entry(language).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var language = _db.Languages.Find(id);
            if (language != null)
                _db.Languages.Remove(language);
        }
    }
}
