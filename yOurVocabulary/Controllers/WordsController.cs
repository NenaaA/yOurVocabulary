using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using yOurVocabulary.Models;

namespace yOurVocabulary.Controllers
{
    [Authorize]
    public class WordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Words
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var model = new List<DisplayWordViewModel>();
            var profileWords = db.ProfileWords.
                Where(pw => pw.Profile.ProfileUser.Id == userId).
                Select(pw => new { pw.Word, pw.LastChecked, pw.CheckedCount }).
                ToList();
                
            foreach (var profileWord in profileWords)
            {
                var storyID = db.StoryWords.
                    Where(sw => sw.WordId == profileWord.Word.WordId).
                    First().StoryId;

                var lang = db.Stories.FirstOrDefault(s => s.StoryId == storyID).Language.Name;
                model.Add(new DisplayWordViewModel
                {
                    WordName = profileWord.Word.Name,
                    CheckedCount = profileWord.CheckedCount,
                    LastChecked = profileWord.LastChecked.ToString(),
                    Language = lang
                });
            }


            return View(model);
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WordName,CheckedCount")] Word word)
        {
            if (ModelState.IsValid)
            {
                db.Words.Add(word);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(word);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WordName,CheckedCount")] Word word)
        {
            if (ModelState.IsValid)
            {
                db.Entry(word).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(word);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Word word = db.Words.Find(id);
            db.Words.Remove(word);
            db.SaveChanges();
            return RedirectToAction("Index");
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
