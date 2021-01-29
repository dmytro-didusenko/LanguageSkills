using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.DataBaseModels;
using OfficeOpenXml;

namespace BusinessLogicLayer.Initializer.Interfaces
{
    interface IIInitialize
    {
        string CreatePath(string directoryName, string fileName);

        ExcelWorksheet GetDataFromFile(string path);

        List<ParsedData> ParseData(ExcelWorksheet worksheet);

        void WriteLanguageDataToDataBase();

        void WriteTestDataToDataBase();
    }
}
