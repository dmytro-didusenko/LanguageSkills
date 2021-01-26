using System;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Implementation;

namespace BusinessLogicLayer
{
    public class UnitOfWork : IDisposable
    {
        private LanguageSkillsDBContext _db = new LanguageSkillsDBContext();
        private CategoryRepository _categoryRepository;
        private CategoryTranslationRepository _categoryTranslationRepository;

        public CategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_db);
                return _categoryRepository;
            }
        }

        public CategoryTranslationRepository CategoryTranslations
        {
            get
            {
                if (_categoryTranslationRepository == null)
                    _categoryTranslationRepository = new CategoryTranslationRepository(_db);
                return _categoryTranslationRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
