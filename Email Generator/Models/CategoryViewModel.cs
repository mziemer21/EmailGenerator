using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Email_Generator.DatabaseModels;

namespace Email_Generator.Models
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }

        public CategoryViewModel()
        {
            using (var db = new devEntities())
            {
                this.Categories = db.Categories.OrderByDescending(i => i.Id).Select(i => new Category
                {
                    id = i.Id,
                    name = i.Name,
                    position = i.Position ?? 0
                }).ToList();
            }
        }
    }
}