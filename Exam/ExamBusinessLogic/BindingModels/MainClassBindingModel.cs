using System;
using System.Collections.Generic;

namespace ExamBusinessLogic.BindingModels
{
    public class MainClassBindingModel
    {
        public int? Id { set; get; }
        public int Field1 { set; get; }
        public string Field2 { set; get; }
        public DateTime DateCreate { set; get; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
