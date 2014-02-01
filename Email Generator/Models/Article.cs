using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Email_Generator.DatabaseModels;

namespace Email_Generator.Models
{
    public class Article
    {
        #region Variables
        [Display(Name = "Title:"), Required(ErrorMessage = "Please fill in the title of the article."), MaxLength(250)]
        public string title { get; set; }
        [Display(Name = "Description:"), Required(ErrorMessage = "Please fill in the description of the article"), DataType(DataType.MultilineText), MaxLength(8000), UIHint("tinymce_jquery_custom"), AllowHtml, MinLength(1)]
        public string description { get; set; }
        [Display(Name = "Read more link:"), StringLength(255)]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "URL format is wrong Ex. http://bus.wisc.edu")]
        public string link { get; set; }
        [Display(Name = "Category:"), Required(ErrorMessage = "Please choose a category for the article")]
        public int category { get; set; }
        [Display(Name = "Position:")]
        public int position { get; set; }
        [Display(Name = "Issue:")]
        public int issue { get; set; }
        [Display(Name = "ID"), Key]
        public int id { get; set; }
        [Display(Name = "Time")]
        public string time { get; set; }
        [Display(Name = "Date"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "[0:MM/dd/yyyy]", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        [Display(Name = "Location")]
        public string location { get; set; }
        #endregion

        public Article() {}

        public Article(int id)
        {
            DatabaseModels.Article article = new DatabaseModels.Article();
            article.Issue = id;
            article.Description = "";
        }

        //public void incrementPosition()
        //{
        //    this.position++;
        //}

        /*
        *NAME: deleteArticle
        *Description: delets and article 
        * RETURNS: true id successful, false if fails
        * PARAMETERS: int id -- The ID of the article to be deleted.
        */
        public static bool deleteArticle(int id)
        {
            using (var db = new devEntities())
            {
                var article = db.Articles.SingleOrDefault(i => i.Id == id);
                var issue = db.Issues.SingleOrDefault(i => i.Id == article.Issue);
                issue.Articles.Remove(article);
                db.Articles.Remove(article);
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
                return false;
            }
        }

        public bool saveChanges()
        {
            using (var db = new devEntities())
            {
                var issue = db.Issues.SingleOrDefault(i => i.Id == this.issue);
                if (issue.Id == 0)
                {
                    return false;
                }

                DatabaseModels.Article article;
                if (this.id > 0)
                {
                    article = db.Articles.SingleOrDefault(a => a.Id == this.id);
                    if ((article.Title != null) && (article.Description != null))
                    {
                        article.Title = this.title;
                        article.Category = this.category;
                        article.Description = this.description;
                        article.Position = this.position;
                        article.Time = this.time;
                        article.Date = this.date;
                        article.Location = this.location;
                        if (string.IsNullOrWhiteSpace(this.link))
                        {
                            article.Link = "";
                        }
                        else
                        {
                            article.Link = this.link;
                        }
                    }
                    try
                    {
                        db.SaveChanges();
                        this.id = article.Id;
                        return true;
                    }
                    catch (Exception e)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    }
                }
                else // Create a new article
                {
                    article = new DatabaseModels.Article();
                    article.Title = this.title;
                    article.Category = this.category;
                    article.Description = this.description;
                    article.Time = this.time;
                    article.Location = this.location;
                    article.Position = this.position;
                    if (string.IsNullOrWhiteSpace(this.link))
                    {
                        article.Link = "";
                    }
                    else
                    {
                        article.Link = this.link;
                    }
                    try
                    {
                        db.Articles.Add(article);
                        db.SaveChanges();
                        this.id = article.Id;
                        return true;
                    }
                    catch (Exception e)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    }
                }
            }
            return false;
        }

    }
}