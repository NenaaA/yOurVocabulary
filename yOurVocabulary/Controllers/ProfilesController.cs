using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using yOurVocabulary.Models;

namespace yOurVocabulary.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Profiles
        //public ActionResult Index()
        //{
        //    var iden = User.Identity.GetUserId();
        //    return View(db.Profiles.FirstOrDefault(m => m.ProfileUser.Id == iden));
        //}

        public ActionResult BecomeCreator()
        {
            //var userId = db.Profiles.FirstOrDefault(p => p.ProfileId == id).ProfileUser.Id;
            var userId = User.Identity.GetUserId();
            return View(new CreatorApplication()
            {
                UserId = userId
            });
        }
        [HttpPost]
        public ActionResult BecomeCreator(CreatorApplication model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            db.CreatorApplications.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        //// GET: Profiles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Profile profile = db.Profiles.Find(id);
        //    if (profile == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(profile);
        //}

        //// GET: Profiles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Profiles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,ProfileName")] Profile profile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Profiles.Add(profile);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(profile);
        //}

        // GET: Profiles/Edit/5
        public ActionResult Edit()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Profile profile = db.Profiles.Find(id);
            var userId = User.Identity.GetUserId();
            var profile = db.Profiles.FirstOrDefault(p => p.ProfileUser.Id == userId);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfileId,ProfileName")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                var prevProfile = db.Profiles.FirstOrDefault(p => p.ProfileId == profile.ProfileId);
                if (!prevProfile.ProfileName.Equals(profile.ProfileName))
                {
                    prevProfile.ProfileName = profile.ProfileName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Manage");
            }
            return View(profile);
        }

        public ActionResult DeleteAccount()
        {

            var userId = User.Identity.GetUserId();

            Profile profile = db.Profiles.FirstOrDefault(p=>p.ProfileUser.Id==userId);
            if (profile == null)
            {
                return HttpNotFound();
            }


            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            var userId = User.Identity.GetUserId();

            Profile profile = db.Profiles.FirstOrDefault(p => p.ProfileUser.Id == userId);

            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            db.Profiles.Remove(profile);
            db.SaveChanges();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("DeletedAccount", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
