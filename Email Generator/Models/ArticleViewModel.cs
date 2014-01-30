using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Email_Generator.DatabaseModels;

namespace Email_Generator.Models
{
    public class ArticleViewModel
    {
        #region variables
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
        public DateTime? date { get; set; }
        [Display(Name = "Location")]
        public string location { get; set; }
        #endregion

        public ArticleViewModel(int? id)
        {
            if ((id.HasValue) && (!string.IsNullOrWhiteSpace(this.id.ToString())))
            {
                using (var db = new devEntities())
                {
                    var article = db.Articles.SingleOrDefault(i => i.Id == id);

                    this.id = article.Id;
                    this.description = article.Description;
                    this.title = article.Title;
                    this.category = article.Category;
                    this.link = article.Link;
                    this.issue = article.Issue;
                    this.position = article.Position;
                    this.time = article.Time;
                    this.date = article.Date;
                    this.location = article.Location;
                }
            }
        }

        //public void incrementPosition()
        //{
        //    this.position++;
        //}
    }
}