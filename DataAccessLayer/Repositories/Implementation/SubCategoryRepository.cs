using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class SubCategoryRepository : ICrudRepository<SubCategory>
    {
        private LanguageSkillsDBContext _db;
        public SubCategoryRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<SubCategory> GetAll()
        {
            return _db.SubCategories;
        }

        public SubCategory Get(int id)
        {
            return _db.SubCategories.Find(id);
        }

        public void Create(SubCategory subCategory)
        {
            _db.SubCategories.Add(subCategory);
        }

        public void Update(SubCategory subCategory)
        {
            _db.Entry(subCategory).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var subCategory = _db.SubCategories.Find(id);
            if (subCategory != null)
                _db.SubCategories.Remove(subCategory);
        }
    }
}
