using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yOurVocabulary.Models;

namespace yOurVocabulary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult DeletedProfile()
        {
            return View();
        }
        //public ActionResult Languages()
        //{
        //    return View(new Languages()
        //    {
        //        LanguageList = new List<Language>()
        //    });
        //}

        
        //public ActionResult SaveLanguages(string code, string name)
        //{

        //    ApplicationDbContext db = new ApplicationDbContext();
        //    db.Languages.Add(new Language()
        //    {
        //        Code = code,
        //        Name = name
        //    });
        //    db.SaveChanges();
            
        //    return RedirectToAction("Index");
        //}
        public ActionResult Index()
        {
            return View();
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