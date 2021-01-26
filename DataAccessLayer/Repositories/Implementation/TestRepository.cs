using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class TestRepository : ICrudRepository<Test>
    {
        private LanguageSkillsDBContext _db;
        public TestRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<Test> GetAll()
        {
            return _db.Tests;
        }

        public Test Get(int id)
        {
            return _db.Tests.Find(id);
        }

        public void Create(Test test)
        {
            _db.Tests.Add(test);
        }

        public void Update(Test test)
        {
            _db.Entry(test).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var test = _db.Tests.Find(id);
            if (test != null)
                _db.Tests.Remove(test);
        }
    }
}
