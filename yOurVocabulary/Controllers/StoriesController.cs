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
    public class StoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Finish(int? id, int? rating)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var userStory = db.ProfileStories.Where(p => p.StoryId == id && p.Profile.ProfileUser.Id == userId).FirstOrDefault();

            if (!userStory.Read)
            {
                userStory.Read = true;
                userStory.Rating = rating;

                db.SaveChanges();
            }

            
            return RedirectToAction("Index");
        }

        // GET: Stories
        public ActionResult Index()
        {
            return View(db.Stories.ToList());
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var profileStory = db.ProfileStories.Where(p => p.Profile.ProfileUser.Id == userId && p.StoryId == id).FirstOrDefault();
            //then create a new currently reading relationship between the story
            //and the logged in user
            if (profileStory == null)
            {
                var profileStoryModel = new ProfileStory()
                {
                    Profile = db.Profiles.Where(p => p.ProfileUser.Id == userId).First(),
                    Story = db.Stories.Where(s => s.StoryId == id).First(),
                    Read = false
                };
                db.ProfileStories.Add(profileStoryModel);
                db.SaveChanges();
            }

            profileStory = db.ProfileStories.Where(p => p.Profile.ProfileUser.Id == userId && p.StoryId == id).FirstOrDefault();

            var model = new DisplayStoryModel() {
                StoryId = story.StoryId,
                Author = story.Author,
                Title = story.Title,
                Language = story.Language,
                Year = story.YearWritten,
                ImageURL = story.ImageURL,
                Words = story.TheStory.Split(' ').ToList(),
                Read=profileStory.Read
            }; 

            return View(model);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Author,Title,YearWritten,Language,Rating,ImageURL,TheStory")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(story);
                db.SaveChanges();

                var words = story.TheStory.Split(' ');
                foreach (var word in words)
                {
                    var wordModel = new Word()
                    {
                        Name = word
                    };
                    db.Words.Add(wordModel);
                    db.SaveChanges();
                    var storyWordsModel = new StoryWord()
                    {
                        Story = story,
                        Word = wordModel
                    };
                    db.StoryWords.Add(storyWordsModel);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(story);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Author,Title,YearWritten,Language,Rating,ImageURL,TheStory")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
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
