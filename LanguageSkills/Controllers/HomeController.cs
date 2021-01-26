using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.DataBaseModels;

namespace LanguageSkills.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void Index()
        {

            //Category category = new Category();
            //category.CategoryName = "New";
            //category.CategoryImagePath = "dfa";
            //_unitOfWork.Categories.Create(category);
            //_unitOfWork.Save();
            //Console.WriteLine(category.Id);
        }
    }
}
