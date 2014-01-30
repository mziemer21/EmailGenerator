using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Email_Generator.DatabaseModels;

namespace Email_Generator.Models
{
    public class IndexViewModel
    {
        public List<IndexIssue> Issues { get; set; }

        public IndexViewModel()
        {
            using (var db = new devEntities())
            {
                this.Issues = db.Issues.OrderByDescending(i => i.Id).Select(i => new IndexIssue
                {
                    id = i.Id,
                    semester = i.Semester,
                    year = i.Year,
                    volume = i.Volume
                }).ToList();
            }
        }
    }

    public class IndexIssue
    {
        public int id { get; set; }
        public string semester { get; set; }
        public int year { get; set; }
        public int volume { get; set; }
    }
}