using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLogicLayer;
using InitializeDataBase.ViewModels;
using DataAccessLayer.DataBaseModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OfficeOpenXml;

namespace InitializeDataBase
{
    public class Initialize
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        private readonly string _pathRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\LanguageSkills"));
        private bool _isData = true;
        private string _tempItem = "";
        private int _countStage = 0;

        private string _createPath(string directoryName, string fileName, string fileExtension)
        {
            return @"\wwwroot\Dictionary\" + directoryName + fileName + fileExtension;
        }

        public ExcelWorksheet GetDataFromFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            return worksheet;
        }

        public List<ParsedData> ParseData(ExcelWorksheet worksheet)
        {
            var parsedData = new List<ParsedData>();

            if (worksheet != null)
            {
                // get number of rows and columns in the sheet
                int rows = worksheet.Dimension.Rows;
                int columns = worksheet.Dimension.Columns;

                // loop through the worksheet rows and columns
                for (int i = 2; i <= rows; i++)
                {
                    for (int j = 2; j <= columns; j++)
                    {
                        if (worksheet.Cells[i, j].Value != null && worksheet.Cells[i, 1].Value != null && 
                            worksheet.Cells[1, j].Value != null)
                        {
                            var data = new ParsedData
                            {
                                Word = worksheet.Cells[i, 1].Value.ToString(),
                                Language = worksheet.Cells[1, j].Value.ToString(),
                                Translation = worksheet.Cells[i, j].Value.ToString()
                            };
                            parsedData.Add(data);
                        }
                        else
                            break;
                    }
                }
            }

            if (parsedData.Count == 0)
            {
                _isData = false;
                Console.WriteLine("No data available");
            }
            return parsedData;
        }

        private void _writeLanguagesToDataBase()
        {
            //Get data from file
            List<ParsedData> languageData = ParseData(GetDataFromFile(
                _pathRoot + _createPath("", "Languages", ".xlsx")));
            //Write data to languages
            List<Language> allLanguages = new List<Language>();
            for (int i = 0, j = 0; i < languageData.Count; i++)
            {
                if (allLanguages.FirstOrDefault(l => l.FullName == languageData[i].Word) == null)
                {
                    allLanguages.Add(new Language()
                    {
                        ShortName = languageData[i + j].Language,
                        FullName = languageData[i + j++].Word
                    });
                }
            }

            if (!_isData)
                return;
            //Save data to dataBase
            _unitOfWork.Languages.CreateRange(allLanguages);
            _unitOfWork.Save();
            Console.WriteLine("{0}/8. Languages have added", ++_countStage);


            //Get all languages
            allLanguages = _unitOfWork.Languages.GetAll().ToList();
            //Write data to language translations
            var allLanguageTranslations = new List<LanguageTranslation>();
            foreach (var language in languageData)
            {
                try
                {
                    int idLanguageWord = allLanguages.First(l => l.FullName == language.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == language.Language).Id;


                    allLanguageTranslations.Add(new LanguageTranslation()
                    {
                        LanguageTranslationName = language.Translation,
                        LanguageWordId = idLanguageWord,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data isn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Save data to dataBase
            _unitOfWork.LanguageTranslations.CreateRange(allLanguageTranslations);
            _unitOfWork.Save();
            Console.WriteLine("{0}/8. Language translations have added", ++_countStage);
        }

        private void _writeTestsToDataBase()
        {
            //Get data from file
            List<ParsedData> testData = ParseData(GetDataFromFile(
                _pathRoot + _createPath("", "TestsNames", ".xlsx")));

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            //Write data to tests
            List<Test> allTests = new List<Test>();
            foreach (var testItem in testData.Where(testItem => _tempItem != testItem.Word))
            {
                _tempItem = testItem.Word;

                allTests.Add(new Test()
                {
                    TestName = testItem.Word
                });
            }

            if (!_isData)
                return;
            //Save data to dataBase
            _unitOfWork.Tests.CreateRange(allTests);
            _unitOfWork.Save();
            Console.WriteLine("{0}/8. Tests have added", ++_countStage);


            //Get all tests
            allTests = _unitOfWork.Tests.GetAll().ToList();
            List<TestTranslation> allTestTranslations = new List<TestTranslation>();
            //Write test translation
            foreach (var testItem in testData)
            {
                try
                {
                    int idTest = allTests.First(l => l.TestName == testItem.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == testItem.Language).Id;
                    allTestTranslations.Add(new TestTranslation()
                    {
                        TestTranslationName = testItem.Translation,
                        TestId = idTest,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data isn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Save data to dataBase
            _unitOfWork.TestTranslations.CreateRange(allTestTranslations);
            _unitOfWork.Save();
            Console.WriteLine("{0}/8. Test translations have added", ++_countStage);
        }

        private void _writeCategoriesToDataBase()
        {
            //Get data from file
            List<ParsedData> categoryData = ParseData(GetDataFromFile(
                _pathRoot + _createPath("", "CategoriesRoot", ".xlsx")));

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            //Write data to categories
            List<Category> allCategories = new List<Category>();
            foreach (var categoryItem in categoryData.Where(categoryItem => _tempItem != categoryItem.Word))
            {
                _tempItem = categoryItem.Word;
                allCategories.Add(new Category()
                {
                    CategoryName = categoryItem.Word,
                    CategoryImagePath = _createPath(@"pictures\", categoryItem.Word, ".jpg")
            });
            }

            if (!_isData)
                return;
            //Save data to dataBase
            _unitOfWork.Categories.CreateRange(allCategories);
            _unitOfWork.Save();
            Console.WriteLine("{0}/8. Categories have added", ++_countStage);


            //Get all categories
            allCategories = _unitOfWork.Categories.GetAll().ToList();
            List<CategoryTranslation> allCategoryTranslations = new List<CategoryTranslation>();
            //Write category translation
            foreach (var categoryItem in categoryData)
            {
                try
                {
                    int idCategory = allCategories.First(l => l.CategoryName == categoryItem.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == categoryItem.Language).Id;
                    allCategoryTranslations.Add(new CategoryTranslation()
                    {
                        CategoryTranslationName = categoryItem.Translation,
                        CategotyId = idCategory,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data isn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Save data to dataBase
            _unitOfWork.CategoryTranslations.CreateRange(allCategoryTranslations);
            _unitOfWork.Save();
            Console.WriteLine("{0}/8. Category translations have added", ++_countStage);
        }

        private void _writeSubCategoriesToDataBase()
        {
            //Get all categories
            List<Category> allCategories = _unitOfWork.Categories.GetAll().ToList();

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            foreach (var category in allCategories)
            {
                //Get data from file
                List<ParsedData> subCategoryData = ParseData(GetDataFromFile(
                    _pathRoot + _createPath(category.CategoryName + @"\", 
                        category.CategoryName, ".xlsx")));

                //Write data to subCategories
                List<SubCategory> allSubCategories = new List<SubCategory>();
                foreach (var subCategoryItem in subCategoryData.Where(subCategoryItem => _tempItem != subCategoryItem.Word))
                {
                    _tempItem = subCategoryItem.Word;

                    allSubCategories.Add(new SubCategory()
                    {
                        SubCategoryName = subCategoryItem.Word,
                        SubCategoryImagePath = _createPath(category.CategoryName + @"\pictures\",
                            subCategoryItem.Word, ".jpg"),
                        CategoryId = category.Id
                });
                }

                if (!_isData)
                    return;
                //Save data to dataBase
                _unitOfWork.SubCategories.CreateRange(allSubCategories);
                _unitOfWork.Save();

                //Get all subCategories
                allSubCategories = _unitOfWork.SubCategories.GetAll().ToList();
                List<SubCategoryTranslation> allSubCategoryTranslations = new List<SubCategoryTranslation>();
                //Write subCategory translation
                foreach (var subCategoryItem in subCategoryData)
                {
                    try
                    {
                        int idSubCategory = allSubCategories.First(l => l.SubCategoryName == subCategoryItem.Word).Id;
                        int idLanguage = allLanguages.First(l => l.ShortName == subCategoryItem.Language).Id;
                        allSubCategoryTranslations.Add(new SubCategoryTranslation()
                        {
                            SubCategoryTranslationName = subCategoryItem.Translation,
                            SubCategoryId = idSubCategory,
                            LanguageId = idLanguage
                        });
                    }
                    catch (InvalidOperationException)
                    {
                        _isData = false;
                        Console.WriteLine("Data isn't exist");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                //Save data to dataBase
                _unitOfWork.SubCategoryTranslations.CreateRange(allSubCategoryTranslations);
                _unitOfWork.Save();
            }
            Console.WriteLine("{0}/8. SubCategories and subCategory translation have added", ++_countStage);
        }

        private void _writeWordsToDataBase()
        {
            //Get all categories
            List<Category> allCategories = _unitOfWork.Categories.GetAll().ToList();

            //Get all subCategories
            List<SubCategory> allSubCategories = _unitOfWork.SubCategories.GetAll().ToList();

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            foreach (var category in allCategories)
            {
                foreach (var subCategory in allSubCategories.Where(s => s.CategoryId == category.Id))
                {
                    //Get data from file
                    List<ParsedData> wordData = ParseData(GetDataFromFile(_pathRoot + _createPath(
                            category.CategoryName + @"\" + subCategory.SubCategoryName + @"\",
                            subCategory.SubCategoryName, ".xlsx")));

                    //Write data to word table in dataBase
                    List<Word> allWords = new List<Word>();
                    foreach (var word in wordData.Where(word => _tempItem != word.Word))
                    {
                        _tempItem = word.Word;

                        allWords.Add(new Word()
                        {
                            WordName = word.Word,
                            WordImagePath = _createPath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                        + @"\pictures\", word.Word, ".jpg"),
                            SubCategoryId = subCategory.Id
                        });
                    }

                    if (!_isData)
                        return;
                    //Save data to dataBase
                    _unitOfWork.Words.CreateRange(allWords);
                    _unitOfWork.Save();

                    //Get all words
                    allWords = _unitOfWork.Words.GetAll().ToList();
                    List<WordTranslation> allWordTranslations = new List<WordTranslation>();

                    //Write subCategory translation
                    foreach (var word in wordData)
                    {
                        try
                        {
                            int idWord = allWords.First(w => w.WordName == word.Word).Id;
                            int idLanguage = allLanguages.First(l => l.ShortName == word.Language).Id;
                            allWordTranslations.Add(new WordTranslation()
                            {
                                WordTranslationName = word.Translation,
                                WordId = idWord,
                                LanguageId = idLanguage,
                                PronunciationPath = _createPath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                                + @"\pronounce\" + word.Language + @"\", 
                                                            word.Word, ".wav"),
                            });
                        }
                        catch (InvalidOperationException)
                        {
                            _isData = false;
                            Console.WriteLine("Data isn't exist");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    //Save data to dataBase
                    _unitOfWork.WordTranslations.CreateRange(allWordTranslations);
                    _unitOfWork.Save();
                }
            }
            Console.WriteLine("{0}/8. Words and word translations have added", ++_countStage);
        }

        public void Initializer()
        {
            Action initializeDataToDataBaseAction = _writeLanguagesToDataBase;
            initializeDataToDataBaseAction += _writeTestsToDataBase;
            initializeDataToDataBaseAction += _writeCategoriesToDataBase;
            initializeDataToDataBaseAction += _writeSubCategoriesToDataBase;
            initializeDataToDataBaseAction += _writeWordsToDataBase;
            initializeDataToDataBaseAction += () =>
            {
                if (_isData && _countStage == 8)
                {
                    Console.WriteLine("Data successfully added to DataBase");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }
            };

            initializeDataToDataBaseAction();
        }
    }
}
