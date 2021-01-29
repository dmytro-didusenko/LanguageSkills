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

        public string CreatePath(string directoryName = "", string fileName = "Languages")
        {
            string rootPath = Directory.GetCurrentDirectory() + @"\wwwroot\Dictionary\" + directoryName + fileName + ".xlsx";
            Console.WriteLine(rootPath);
            return rootPath;
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
                        if (worksheet.Cells[i, j].Value == null) 
                            break;

                        var data = new ParsedData
                        {
                            Word = worksheet.Cells[i, 1].Value.ToString(),
                            Language = worksheet.Cells[1, j].Value.ToString(),
                            Translation = worksheet.Cells[i, j].Value.ToString()
                        };
                        parsedData.Add(data);
                    }
                }
            }

            return parsedData;
        }

        public void WriteLanguageDataToDataBase()
        {
            //Write data to languages table
            List<ParsedData> languageData = ParseData(GetDataFromFile(CreatePath()));
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
            foreach (var item in allLanguages)
            {
                _unitOfWork.Languages.Create(item);
                _unitOfWork.Save();
            }

            //Write data to languageTranlations table
            allLanguages = _unitOfWork.Languages.GetAll().ToList();
            foreach (var language in languageData)
            {
                try
                {
                    int idLanguageWord = allLanguages.First(l => l.FullName == language.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == language.Language).Id;
                    _unitOfWork.LanguageTranslations.Create(new LanguageTranslation()
                    {
                        LanguageTranslationName = language.Translation,
                        LanguageWordId = idLanguageWord,
                        LanguageId = idLanguage
                    });
                    _unitOfWork.Save();
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
        }

        public void WriteDataToDataBase()
        {
            

            throw new NotImplementedException();
        }
    }
}
