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
        /// <summary>
        /// Get data from Exel file
        /// </summary>
        /// <returns>List with word, language and translation</returns>
        ExcelWorksheet GetDataFromFile(string path);

        List<ParsedData> ParseData(ExcelWorksheet worksheet);

        void WriteDataToDataBase(List<ParsedData> data);
    }
}
