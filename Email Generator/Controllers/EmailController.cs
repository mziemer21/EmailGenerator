using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Email_Generator.Models;

namespace Email_Generator.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        public ActionResult Issue(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToActionPermanent("CreateIssue");
            }
            var model = new IssueViewModel(id.Value);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult CreateIssue()
        {
            var model = new Issue();
            model.year = DateTime.Now.Year;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateIssue(Issue model)
        {
            if ((ModelState.IsValid) && (model.saveChanges()))
            {
                return RedirectToAction("Issue", new { id = model.id });
            }
            return View(model);
        }

        public ActionResult EditIssue(int? id)
        {
            var model = new IssueViewModel(id.Value);
            if (model.id != 0)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditIssue(Issue model)
        {
            if ((ModelState.IsValid) && (model.saveChanges()))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DeleteIssue(int? id)
        {
            var model = new Issue(id.Value);
            if (model.id != 0)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteIssue(Issue model)
        {
            if (model.deleteIssue())
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Article(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToActionPermanent("CreateArticle");
            }
            var model = new ArticleViewModel(id.Value);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult CreateArticle(int issueId)
        {
            using (var db = new Email_Generator.DatabaseModels.devEntities())
            {
                ViewBag.categories = new SelectList(db.Categories.OrderBy(i => i.Position).ToList(), "Id", "Name", null);
                var issue = db.Issues.SingleOrDefault(i => i.Id == issueId);

                if (issueId != 0)
                {
                    var model = new Article(issueId);
                    model.issue = issueId;
                    model.position = issue.Articles.Count;
                    model.date = DateTime.Now.Date;
                    return View(model);
                }
                return RedirectToAction("Issue");
            }
        }

        [HttpPost]
        public ActionResult CreateArticle(Article model)
        {
            if ((ModelState.IsValid) && (model.saveChanges()))
            {
                return RedirectToAction("Issue", new { id = model.issue });
            }
            using (var db = new Email_Generator.DatabaseModels.devEntities())
            {
                ViewBag.categories = new SelectList(db.Categories.OrderBy(i => i.Position).ToList(), "Id", "Name", model.category);
            }

            return View(model);
        }

        public ActionResult EditArticle(int? id)
        {
            var model = new ArticleViewModel(id.Value);
            using (var db = new Email_Generator.DatabaseModels.devEntities())
            {
                ViewBag.categories = new SelectList(db.Categories.OrderBy(i => i.Id).ToList(), "Id", "Name", model.category);
            }
            if (id != 0)
            {
                return View(model);
            }
            return RedirectToAction("Issue");
        }

        [HttpPost]
        public ActionResult EditArticle(Article model)
        {
            if ((ModelState.IsValid) && (model.saveChanges()))
            {
                return RedirectToAction("Issue", new { id = model.issue });
            }
            using (var db = new Email_Generator.DatabaseModels.devEntities())
            {
                ViewBag.categories = new SelectList(db.Categories.OrderBy(i => i.Position).ToList(), "Id", "Name", model.category);
            }
            var modelNew = new ArticleViewModel(model.id);
            return View(modelNew);
        }

        public ActionResult DeleteArticle(int? id)
        {
            var model = new ArticleViewModel(id.Value);
            if (id != 0)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteArticle(Article model)
        {
            var issueID = model.issue;
            if (Email_Generator.Models.Article.deleteArticle(model.id))
            {
                return RedirectToAction("Issue", new { id = issueID });
            }
            return View(model);
        }

        public ActionResult GenerateIssueCode(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToActionPermanent("Index");
            }
            var model = new IssueViewModel(id.Value);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult ArticlePosition(int? id)
        {
            return View();
        }

        [HttpPost]
        public string ArticlePosition(string articleID, string articleID2)
        {
            int aID1, aID2;
            articleID = articleID.Remove(0, 5);
            articleID2 = articleID2.Remove(0, 5);
            Int32.TryParse(articleID, out aID1);
            Int32.TryParse(articleID2, out aID2);

            var db = new Email_Generator.DatabaseModels.devEntities();
            var article = db.Articles.SingleOrDefault(i => i.Id == aID1);
            var article2 = db.Articles.SingleOrDefault(i => i.Id == aID2);

            var temp = article2.Position;
            article2.Position = article.Position;
            article.Position = temp;

            try
            {
                db.SaveChanges();
                return "Success!";
            }
            catch (Exception e)
            {
                return "Failed to swap articles.";
            }
        }
    }
}
