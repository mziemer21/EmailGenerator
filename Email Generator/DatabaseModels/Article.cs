//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Email_Generator.DatabaseModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Position { get; set; }
        public int Category { get; set; }
        public int Issue { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
    
        public virtual Category Category1 { get; set; }
        public virtual Issue Issue1 { get; set; }
    }
}
