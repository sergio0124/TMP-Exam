using ExamBusinessLogic.Enums;
using System;

namespace ExamBusinessLogic.BindingModels
{
    public class ExtraClassBindingModel
    {
        public int? Id { set; get; }
        public int Field1 { set; get; }
        public string Field2 { set; get; }
        public DateTime DateCreate { set; get; }
        public MyEnum Type { set; get; }
        public int MainClassId { set; get; }
    }
}
