using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class WordRepository : ICrudRepository<Word>
    {
        private LanguageSkillsDBContext _db;
        public WordRepository(LanguageSkillsDBContext context)
        {
            this._db = context;
        }

        public IEnumerable<Word> GetAll()
        {
            return _db.Words;
        }

        public Word Get(int id)
        {
            return _db.Words.Find(id);
        }

        public void CreateRange(List<Word> words)
        {
            _db.Words.AddRange(words);
        }

        public void Update(Word word)
        {
            _db.Entry(word).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var word = _db.Words.Find(id);
            if (word != null)
                _db.Words.Remove(word);
        }
    }
}
