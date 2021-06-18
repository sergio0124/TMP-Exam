using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBusinessLogic.ViewModels
{
    public class MainClassViewModel
    {
        public int Id { set; get; }
        public int Field1 { set; get; }
        public string Field2 { set; get; }
        public DateTime DateCreate { set; get; }
        public Dictionary<int, string> Dictionary { set; get; }
    }
}
