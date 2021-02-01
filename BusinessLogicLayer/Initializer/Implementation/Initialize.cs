using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLogicLayer.Initializer.Interfaces;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.DataBaseModels;
using Microsoft.EntityFrameworkCore.Metadata;
using OfficeOpenXml;

namespace BusinessLogicLayer.Initializer.Implementation
{
    public class Initialize : IIInitialize
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

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

            return parsedData;
        }

        public void WriteLanguageDataToDataBase()
        {
            //Get data from file
            List<ParsedData> languageData = ParseData(GetDataFromFile(
                Directory.GetCurrentDirectory() + _createPath("", "Languages", ".xlsx")));
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

            //Save data to dataBase
            _unitOfWork.Languages.CreateRange(allLanguages);
            _unitOfWork.Save();
            

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
        }

        public void WriteTestDataToDataBase()
        {
            //Get data from file
            List<ParsedData> testData = ParseData(GetDataFromFile(
                Directory.GetCurrentDirectory() + _createPath("", "TestsNames", ".xlsx")));

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            //Write data to tests
            Test test = new Test();
            List<Test> allTests = new List<Test>();
            foreach (var testItem in testData.Where(testItem => test.TestName != testItem.Word))
            {
                test.Id = 0;
                test.TestName = testItem.Word;
                allTests.Add(test);
            }

            //Save data to dataBase
            _unitOfWork.Tests.CreateRange(allTests);
            _unitOfWork.Save();


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
        }

        public void WriteCategoryDataToDataBase()
        {
            //Get data from file
            List<ParsedData> categoryData = ParseData(GetDataFromFile(
                Directory.GetCurrentDirectory() + _createPath("", "CategoriesRoot", ".xlsx")));

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            //Write data to categories
            Category category = new Category();
            List<Category> allCategories = new List<Category>();
            foreach (var categoryItem in categoryData.Where(categoryItem => category.CategoryName != categoryItem.Word))
            {
                category.Id = 0;
                category.CategoryName = categoryItem.Word;
                category.CategoryImagePath = _createPath(@"pictures\", category.CategoryName, ".jpg");
                allCategories.Add(category);
            }

            //Save data to dataBase
            _unitOfWork.Categories.CreateRange(allCategories);
            _unitOfWork.Save();


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

        }

        public void WriteSubCategoryDataToDataBase()
        {
            //Get all categories
            List<Category> allCategories = _unitOfWork.Categories.GetAll().ToList();

            //Get all list of language
            List<Language> allLanguages = _unitOfWork.Languages.GetAll().ToList();

            foreach (var category in allCategories)
            {
                //Get data from file
                List<ParsedData> subCategoryData = ParseData(GetDataFromFile(
                    Directory.GetCurrentDirectory() + _createPath(category.CategoryName + @"\", 
                        category.CategoryName, ".xlsx")));

                //Write data to subCategories
                List<SubCategory> allSubCategories = new List<SubCategory>();
                string tempSubCategory = "";
                foreach (var subCategoryItem in subCategoryData.Where(subCategoryItem => 
                    tempSubCategory != subCategoryItem.Word))
                {
                    tempSubCategory = subCategoryItem.Word;

                    allSubCategories.Add(new SubCategory()
                    {
                        SubCategoryName = subCategoryItem.Word,
                        SubCategoryImagePath = _createPath(category.CategoryName + @"\pictures\",
                            subCategoryItem.Word, ".jpg"),
                        CategoryId = category.Id
                });
                }

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
        }

        public void WriteWordDataToDataBase()
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
                    List<ParsedData> wordData = ParseData(GetDataFromFile(
                        Directory.GetCurrentDirectory() + _createPath(
                            category.CategoryName + @"\" + subCategory.SubCategoryName + @"\",
                            subCategory.SubCategoryName, ".xlsx")));

                    //Write data to word table in dataBase
                    List<Word> allWord = new List<Word>();
                    string tempWord = "";
                    foreach (var word in wordData.Where(word =>
                        tempWord != word.Word))
                    {
                        tempWord = word.Word;

                        allWord.Add(new Word()
                        {
                            WordName = word.Word,
                            WordImagePath = _createPath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                        + @"\pictures\", tempWord, ".jpg"),
                            SubCategoryId = subCategory.Id
                        });
                    }

                    ////Save data to dataBase
                    //_unitOfWork.Words.CreateRange(allWord);
                    //_unitOfWork.Save();
                }


                Console.WriteLine(count++);
                ////Get all subCategories



                //allSubCategories = _unitOfWork.SubCategories.GetAll().ToList();
                //List<SubCategoryTranslation> allSubCategoryTranslations = new List<SubCategoryTranslation>();
                ////Write subCategory translation
                //foreach (var subCategoryItem in subCategoryData)
                //{
                //    try
                //    {
                //        int idSubCategory = allSubCategories.First(l => l.SubCategoryName == subCategoryItem.Word).Id;
                //        int idLanguage = allLanguages.First(l => l.ShortName == subCategoryItem.Language).Id;
                //        allSubCategoryTranslations.Add(new SubCategoryTranslation()
                //        {
                //            SubCategoryTranslationName = subCategoryItem.Translation,
                //            SubCategoryId = idSubCategory,
                //            LanguageId = idLanguage
                //        });
                //    }
                //    catch (InvalidOperationException)
                //    {
                //        Console.WriteLine("Data isn't exist");
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }
                //}

                ////Save data to dataBase
                //_unitOfWork.SubCategoryTranslations.CreateRange(allSubCategoryTranslations);
                //_unitOfWork.Save();
            }
        }





    }
}
