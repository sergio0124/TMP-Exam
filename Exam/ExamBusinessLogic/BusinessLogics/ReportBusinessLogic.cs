using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ExamBusinessLogic.BusinessLogics
{
    public class ReportBusinessLogic
    {
        private readonly IMainClassStorage MainClassStorage;

        public ReportBusinessLogic(IMainClassStorage MainClassStorage)
        {
            this.MainClassStorage = MainClassStorage;
        }

        public List<ReportViewModel> GetOrders(MainClassBindingModel model)
        {
            var MainClasss = MainClassStorage.GetFilteredList(model);
            var list = new List<ReportViewModel>();
            foreach (var MainClass in MainClasss)
            {
                var record = new ReportViewModel
                {
                    MainClassField1 = MainClass.Field1.ToString(),
                    MainClassDateCreate = MainClass.DateCreate
                };
                foreach (var component in MainClass.Dictionary)
                {
                    record.Type = component.Value.Item2;
                    record.ExtraClassField1 = component.Value.Item1;
                    record.ExtraClassDateCreate = component.Value.Item3;
                    list.Add(record);
                }
            }
            return list;
        }
        public async void SaveJSONDataContract(MainClassBindingModel model)
        {
            await Task.Run(()=> {
                DataContractJsonSerializer formatter =
                    new DataContractJsonSerializer(typeof(List<ReportViewModel>));
                using (FileStream fs = new FileStream(@"C:\Users\Mvideo\Desktop\4 семестр\Технологии программирования\otchet.json", FileMode.OpenOrCreate))
                {
                    formatter.WriteObject(fs, GetOrders(model));
                }
            });            
        }
    }
}
