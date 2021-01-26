using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class SubCategoryTranslationRepository : ICrudRepository<SubCategoryTranslation>
    {
        private LanguageSkillsDBContext _db;
        public SubCategoryTranslationRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<SubCategoryTranslation> GetAll()
        {
            return _db.SubCategoryTranslations;
        }

        public SubCategoryTranslation Get(int id)
        {
            return _db.SubCategoryTranslations.Find(id);
        }

        public void Create(SubCategoryTranslation subCategoryTranslation)
        {
            _db.SubCategoryTranslations.Add(subCategoryTranslation);
        }

        public void Update(SubCategoryTranslation subCategoryTranslation)
        {
            _db.Entry(subCategoryTranslation).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var subCategoryTranslation = _db.SubCategoryTranslations.Find(id);
            if (subCategoryTranslation != null)
                _db.SubCategoryTranslations.Remove(subCategoryTranslation);
        }
    }
}
