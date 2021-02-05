using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using yOurVocabulary.Migrations;
using yOurVocabulary.Models;

namespace yOurVocabulary.Controllers
{
    [Authorize(Roles ="Admin, Creator, User")]
    public class StoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public ActionResult CheckWord(string wordName)
        //{
        //    var userId = User.Identity.GetUserId();
        //    wordName = wordName.ToLower();
        //    string tempWord = wordName.ToLower();

        //    char firstLetter = tempWord[0];
        //    char lastLetter = tempWord[tempWord.Length - 1];
        //    while (!Char.IsLetter(firstLetter))
        //    {
        //        tempWord = tempWord.Remove(0, 1);
        //        firstLetter = tempWord[0];
        //    }
        //    while (!Char.IsLetter(lastLetter))
        //    {
        //        tempWord = tempWord.Remove(tempWord.Length - 1);
        //        lastLetter = tempWord[tempWord.Length - 1];
        //    }


        //    var word = db.Words.FirstOrDefault(w=>w.Name== tempWord);
        //    var profile = db.Profiles.FirstOrDefault(p => p.ProfileUser.Id == userId);
        //    var profileWord = db.ProfileWords.
        //        FirstOrDefault(pw => pw.ProfileId == profile.ProfileId && pw.WordId == word.WordId);

        //    if (profileWord == null)
        //    {
        //        profileWord = new ProfileWord()
        //        {
        //            Profile = profile,
        //            Word = word,
        //            CheckedCount = 0,
        //            LastChecked = DateTime.Now
        //        };
        //        db.ProfileWords.Add(profileWord);
        //    }
        //    else
        //    {
        //        profileWord.LastChecked = DateTime.Now;
        //        profileWord.CheckedCount += 1;
        //    }

        //    db.SaveChanges();



        //    return RedirectToAction("Index", "Stories");
        //}

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

            //userStory relation
            if (!userStory.Read)
            {
                userStory.Read = true;
                userStory.Rating = rating;

                db.SaveChanges();
            }

            var profile = db.Profiles.FirstOrDefault(p => p.ProfileUser.Id == userId);

            //userWord relations
            var storyWords = db.StoryWords.Where(sw => sw.StoryId == story.StoryId).Select(sw=>sw.Word).ToList();
            foreach (var storyWord in storyWords)
            {
                var profileWord = db.ProfileWords.FirstOrDefault(pw => pw.Profile.ProfileUser.Id == userId && pw.WordId == storyWord.WordId);
                
                if (profileWord == null)
                {
                    var word = db.Words.FirstOrDefault(w => w.WordId == storyWord.WordId);
                    profileWord = new ProfileWord()
                    {
                        Profile = profile,
                        Word = word,
                        CheckedCount = 0,
                        LastChecked=null
                    };
                    db.ProfileWords.Add(profileWord);
                    db.SaveChanges();
                }
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

            var model = new DisplayStoryViewModel() {
                StoryId = story.StoryId,
                Author = story.Author,
                Title = story.Title,
                Language = db.Languages.FirstOrDefault(l=>l.Code==story.Language.Code).Name,
                Year = story.YearWritten,
                ImageURL = story.ImageURL,
                Words = story.TheStory.Split(' ').ToList(),
                Read=profileStory.Read
            }; 

            return View(model);
        }
        [Authorize(Roles ="Admin, Creator")]
        // GET: Stories/Create
        public ActionResult Create()
        {
            List<Language> Languages = db.Languages.ToList();
            ViewBag.Languages = Languages;
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoryId,Author,Title,YearWritten,Language,Rating,ImageURL,TheStory")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.Language = db.Languages.FirstOrDefault(l => l.Code==story.Language.Code);
                db.Stories.Add(story);
                db.SaveChanges();

                var words = story.TheStory.Split(' ');
                foreach (var word in words)
                {

                    //avoid distinct words and punctuations

                    string tempWord = word.ToLower();

                    char firstLetter = tempWord[0];
                    char lastLetter = tempWord[tempWord.Length - 1];
                    while (!Char.IsLetter(firstLetter))
                    {
                        tempWord = tempWord.Remove(0, 1); 
                        firstLetter = tempWord[0];
                    }
                    while (!Char.IsLetter(lastLetter))
                    {
                        tempWord = tempWord.Remove(tempWord.Length - 1);
                        lastLetter = tempWord[tempWord.Length - 1];
                    }


                    var wordModel = db.Words.FirstOrDefault(w => w.Name == tempWord);

                    if (wordModel == null)
                    {
                        wordModel = new Word()
                        {
                            Name = tempWord
                        }; 
                        db.Words.Add(wordModel);
                        db.SaveChanges();
                    }

                    var storyWordsModel = db.StoryWords.FirstOrDefault(sw => sw.StoryId == story.StoryId && sw.WordId == wordModel.WordId);

                    if (storyWordsModel == null)
                    {
                        storyWordsModel = new StoryWord()
                        {
                            Story = story,
                            Word = wordModel
                        };
                        db.StoryWords.Add(storyWordsModel);
                        db.SaveChanges();
                    }

                }

                return RedirectToAction("Index");
            }

            return View(story);
        }
        [Authorize(Roles ="Admin, Creator")]
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
            List<Language> Languages = db.Languages.ToList();
            ViewBag.Languages = Languages;
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoryId,Author,Title,YearWritten,Language,Rating,ImageURL,TheStory")] Story story)
        {
            if (ModelState.IsValid)
            {
                var prevStory = db.Stories.FirstOrDefault(s => s.StoryId == story.StoryId);
                prevStory.Language = db.Languages.FirstOrDefault(l => l.Code == story.Language.Code);
                prevStory.Author = story.Author;
                prevStory.Title = story.Title;
                prevStory.YearWritten = story.YearWritten;
                prevStory.ImageURL = story.ImageURL;
                prevStory.TheStory = story.TheStory;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(story);
        }
        [Authorize(Roles ="Admin, Creator")]
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
