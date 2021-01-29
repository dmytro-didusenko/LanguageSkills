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
        private SubCategoryRepository _subCategoryRepository;
        private SubCategoryTranslationRepository _subCategoryTranslationRepository;
        private WordRepository _wordRepository;
        private WordTranslationRepository _wordTranslationRepository;
        private TestRepository _testRepository;
        private TestTranslationRepository _testTranslationRepository;
        private LanguageRepository _languageRepository;
        private LanguageTranslationRepository _languageTranslationRepository;

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

        public SubCategoryRepository SubCategories
        {
            get
            {
                if (_subCategoryRepository == null)
                    _subCategoryRepository = new SubCategoryRepository(_db);
                return _subCategoryRepository;
            }
        }

        public SubCategoryTranslationRepository SubCategoryTranslations
        {
            get
            {
                if (_subCategoryTranslationRepository == null)
                    _subCategoryTranslationRepository = new SubCategoryTranslationRepository(_db);
                return _subCategoryTranslationRepository;
            }
        }

        public WordRepository Words
        {
            get
            {
                if (_wordRepository == null)
                    _wordRepository = new WordRepository(_db);
                return _wordRepository;
            }
        }

        public WordTranslationRepository WordTranslations
        {
            get
            {
                if (_wordTranslationRepository == null)
                    _wordTranslationRepository = new WordTranslationRepository(_db);
                return _wordTranslationRepository;
            }
        }
        public TestRepository Tests
        {
            get
            {
                if (_testRepository == null)
                    _testRepository = new TestRepository(_db);
                return _testRepository;
            }
        }

        public TestTranslationRepository TestTranslations
        {
            get
            {
                if (_testTranslationRepository == null)
                    _testTranslationRepository = new TestTranslationRepository(_db);
                return _testTranslationRepository;
            }
        }

        public LanguageRepository Languages
        {
            get
            {
                if (_languageRepository == null)
                    _languageRepository = new LanguageRepository(_db);
                return _languageRepository;
            }
        }

        public LanguageTranslationRepository LanguageTranslations
        {
            get
            {
                if (_languageTranslationRepository == null)
                    _languageTranslationRepository = new LanguageTranslationRepository(_db);
                return _languageTranslationRepository;
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
