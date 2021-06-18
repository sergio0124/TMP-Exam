using ExamBusinessLogic.Enums;
using System;
using LawFirmBusinessLogic.Attributes;

namespace ExamBusinessLogic.ViewModels
{
    public class ExtraClassViewModel
    {
        [Column(visible: false)]
        public int Id { set; get; }
        [Column(title: "Номер", width: 50)]
        public int Field1 { set; get; }
        [Column(title: "Номер", width: 50)]
        public string Field2 { set; get; }
        [Column(title: "Номер", gridViewAutoSize:GridViewAutoSize.AllCells, format:"d")]
        public DateTime DateCreate { set; get; }
        [Column(title: "Номер", width: 50)]
        public MyEnum Type { set; get; }
        [Column(title: "Номер", width: 50)]
        public string MainClassName { set; get; }
        [Column(visible:false)]
        public int MainClassId { set; get; }
    }
}
