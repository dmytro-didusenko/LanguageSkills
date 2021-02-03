using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;


namespace LanguageSkills.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void Index()
        {

        }
    }
}
