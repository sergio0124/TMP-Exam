using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ExamBusinessLogic.Interfaces
{
    public interface IMainClassStorage
    {
        List<MainClassViewModel> GetFullList();
        List<MainClassViewModel> GetFilteredList(MainClassBindingModel model);
        MainClassViewModel GetElement(MainClassBindingModel model);
        void Insert(MainClassBindingModel model);
        void Update(MainClassBindingModel model);
        void Delete(MainClassBindingModel model);
    }
}
