using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BusinessLogicLayer.Initializer.Interfaces;
using DataAccessLayer.DataBaseModels;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLogicLayer.Initializer.Implementation
{
    public class InitializeLanguages : IIInitialize<Language>
    {
        public List<Language> GetDataFromFile()
        {
            string rootPath = Directory.GetCurrentDirectory() + @"\wwwroot\";
            Console.WriteLine(rootPath);
            

            throw new NotImplementedException();
        }
    }
}
