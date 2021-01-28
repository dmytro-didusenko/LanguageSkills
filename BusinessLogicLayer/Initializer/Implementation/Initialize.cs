using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BusinessLogicLayer.Initializer.Interfaces;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.DataBaseModels;
using Microsoft.AspNetCore.Hosting;
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

                        var data = new ParsedData();
                        data.Word = worksheet.Cells[i, 1].Value.ToString();
                        data.Language = worksheet.Cells[1, j].Value.ToString();
                        data.Translation = worksheet.Cells[i, j].Value.ToString();
                        parsedData.Add(data);
                    }
                }
            }

            return parsedData;
        }

        public void WriteDataToDataBase(List<ParsedData> data)
        {
            
        }
    }
}
