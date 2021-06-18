using ExamBusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ExamDatabaseImplement.Models
{
    public class ExtraClass
    {
        public int Id { set; get; }
        [Required]
        public int Field1 { set; get; }
        [Required]
        public string Field2 { set; get; }
        [Required]
        public DateTime DateCreate { set; get; }
        [Required]
        public MyEnum Type { set; get; }
        [Required]
        public int MainClassId { set; get; }
        [Required]
        public string MainClassName { set; get; }
        public MainClass MainClass { set; get; }
    }
}
