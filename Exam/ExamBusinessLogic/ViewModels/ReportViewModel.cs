using ExamBusinessLogic.Enums;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace ExamBusinessLogic.ViewModels
{
    [DataContract]
    public class ReportViewModel
    {
        [DataMember]
        public string MainClassField1 { get; set; }
        [DataMember]
        public DateTime MainClassDateCreate { get; set; }
        [DataMember]
        public string ExtraClassField1 { get; set; }
        [DataMember]
        public MyEnum Type { get; set; }
        [JsonIgnore]
        public DateTime ExtraClassDateCreate { get; set; }
    }
}
