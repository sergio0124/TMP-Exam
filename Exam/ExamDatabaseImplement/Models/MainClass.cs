using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamDatabaseImplement.Models
{
    public class MainClass
    {
        public int Id { set; get; }
        [Required]
        public int Field1 { set; get; }
        [Required]
        public string Field2 { set; get; }
        public DateTime DateCreate { set; get; }
        [ForeignKey("MainClassId")]
        public virtual List<ExtraClass> ExtraClasses { set; get; }
    }
}
