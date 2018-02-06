using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildFormation.Models;
using BuildFormation.ViewModels;

namespace BuildFormation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDal _dal;

        public HomeController() : this(new Dal())
        {
        }

        public HomeController(IDal dalIoc)
        {
            _dal = dalIoc;
        }

        public ActionResult Index()
        {

            var homeViewModel = new HomeViewModel
            {
               
                ListeDerniersTopics = _dal.ObtenirListeDerniersTopics(5),
                ListeDerniersDocuments = _dal.ObtenirListeDerniersDocuments(5),
                Limit = 5
            };


            return View(homeViewModel);
          
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}