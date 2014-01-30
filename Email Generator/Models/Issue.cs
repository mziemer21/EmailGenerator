using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Email_Generator.DatabaseModels;

namespace Email_Generator.Models
{
    public class Issue
    {
        #region Variables
        [Display(Name = "ID"), Key]
        public int id { get; set; }
        [Display(Name = "Semester:"), Required(ErrorMessage = "Please fill in the semester of the Issue"), StringLength(20)]
        public string semester { get; set; }
        [Display(Name = "Year:"), Required(ErrorMessage = "Please fill in the year of the Issue")]
        public int year { get; set; }
        [Display(Name = "Volume:"), Required(ErrorMessage = "Please fill in the volume of the Issue")]
        public int volume { get; set; }
        #endregion

        public Issue() { }

        public Issue(int id)
        {
            using (var db = new devEntities())
            {
                var issue = db.Issues.SingleOrDefault(i => i.Id == id);
                if (issue != null)
                {
                    this.id = issue.Id;
                    this.semester = issue.Semester;
                    this.year = issue.Year;
                    this.volume = issue.Volume;
                }
            }
        }

        public bool deleteIssue()
        {
            using (var db = new devEntities())
            {
                DatabaseModels.Issue issue;
                if (this.id > 0)
                {
                    issue = db.Issues.First(i => i.Id == this.id);
                    foreach (var article in issue.Articles)
                    {
                        Article.deleteArticle(article.Id);
                    }

                    try
                    {
                        db.Issues.Remove(issue);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    }
                }
                return false;
            }
        }

        public bool saveChanges()
        {
            using (var db = new devEntities())
            {
                DatabaseModels.Issue issue;
                if (this.id > 0)
                {
                    issue = db.Issues.SingleOrDefault(i => i.Id == this.id);
                    if (issue != null)
                    {
                        issue.Semester = this.semester;
                        issue.Year = this.year;
                        issue.Volume = this.volume;
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }
                        catch (Exception e)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                        }
                    }
                }
                else
                {
                    issue = new DatabaseModels.Issue();
                    issue.Semester = this.semester;
                    issue.Year = this.year;
                    issue.Volume = this.volume;
                    try
                    {
                        db.Issues.Add(issue);
                        db.SaveChanges();
                        this.id = issue.Id;
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