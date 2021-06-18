using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ExamBusinessLogic.Interfaces
{
    public interface IExtraClassStorage
    {
        List<ExtraClassViewModel> GetFullList();
        List<ExtraClassViewModel> GetFilteredList(ExtraClassBindingModel model);
        ExtraClassViewModel GetElement(ExtraClassBindingModel model);
        void Insert(ExtraClassBindingModel model);
        void Update(ExtraClassBindingModel model);
        void Delete(ExtraClassBindingModel model);
    }
}
