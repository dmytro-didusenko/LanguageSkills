using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class CategoryTranslationRepository : ICrudRepository<CategoryTranslation>
    {
        private LanguageSkillsDBContext _db;
        public CategoryTranslationRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<CategoryTranslation> GetAll()
        {
            return _db.CategoryTranslations;
        }

        public CategoryTranslation Get(int id)
        {
            return _db.CategoryTranslations.Find(id);
        }

        public void CreateRange(List<CategoryTranslation> categoryTranslations)
        {
            _db.CategoryTranslations.AddRange(categoryTranslations);
        }

        public void Update(CategoryTranslation categoryTranslation)
        {
            _db.Entry(categoryTranslation).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var categoryTranslation = _db.CategoryTranslations.Find(id);
            if (categoryTranslation != null)
                _db.CategoryTranslations.Remove(categoryTranslation);
        }
    }
}
