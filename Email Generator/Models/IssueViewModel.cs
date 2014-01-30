using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Email_Generator.DatabaseModels;

namespace Email_Generator.Models
{
    public class IssueViewModel
    {
        #region Variables
        [Display(Name = "ID"), Key]
        public int id { get; set; }
        [Display(Name = "Semester")]
        public string semester { get; set; }
        [Display(Name = "Year")]
        public int year { get; set; }
        [Display(Name = "volume")]
        public int volume { get; set; }
        public List<ArticleViewModel> articleList { get; set; }
        #endregion

        public IssueViewModel(int id)
        {
            using(var db = new devEntities())
            {
                var issue = db.Issues.SingleOrDefault(i => i.Id == id);
                if (issue != null)
                {
                    this.id = issue.Id;
                    this.semester = issue.Semester;
                    this.year = issue.Year;
                    this.volume = issue.Volume;
                    this.articleList = issue.Articles.Select(i => new ArticleViewModel(i.Id)
                    {
                        id = i.Id,
                        title = i.Title,
                        description = i.Description,
                        link = i.Link,
                        position = i.Position,
                        category = i.Category,
                        issue = i.Issue
                    }).ToList();
                }
            }
        }
    }
}