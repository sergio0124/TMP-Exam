using ExamBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using LawFirmBusinessLogic.Attributes;

namespace ExamBusinessLogic.ViewModels
{
    public class MainClassViewModel
    {
        [Column(visible: false)]
        public int Id { set; get; }
        [Column(title: "Номер", width: 50)]
        public int Field1 { set; get; }
        [Column(title: "Номер", width: 50)]
        public string Field2 { set; get; }
        [Column(title: "Номер", gridViewAutoSize:GridViewAutoSize.AllCells, format:"d")]
        public DateTime DateCreate { set; get; }
        [Column(visible:false)]
        public Dictionary<int, (string,MyEnum,DateTime)> Dictionary { set; get; }
    }
}
