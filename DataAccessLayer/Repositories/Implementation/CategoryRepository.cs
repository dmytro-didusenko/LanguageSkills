using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class CategoryRepository : ICrudRepository<Category>
    {
        private LanguageSkillsDBContext _db;
        public CategoryRepository(LanguageSkillsDBContext context)
        { 
            this._db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories;
        }

        public Category Get(int id)
        {
            return _db.Categories.Find(id);
        }

        public void Create(Category category)
        {
            _db.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var category = _db.Categories.Find(id);
            if (category != null)
                _db.Categories.Remove(category);
        }
    }
}
