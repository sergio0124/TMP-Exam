using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ExamBusinessLogic.BusinessLogics
{
    public class ExtraClassBusinessLogic
    {
        private readonly IExtraClassStorage Storage;
        public ExtraClassBusinessLogic(IExtraClassStorage ExtraClassStorage)
        {
            Storage = ExtraClassStorage;
        }

        public List<ExtraClassViewModel> Read(ExtraClassBindingModel model)
        {
            if (model == null)
            {
                return Storage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ExtraClassViewModel> { Storage.GetElement(model) };
            }
            return Storage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ExtraClassBindingModel model)
        {
            var element = Storage.GetElement(new ExtraClassBindingModel
            {
                Field1 = model.Field1
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такой элемент");
            }
            if (model.Id.HasValue)
            {
                Storage.Update(model);
            }
            else
            {
                Storage.Insert(model);
            }
        }
        public void Delete(ExtraClassBindingModel model)
        {
            var element = Storage.GetElement(new ExtraClassBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            Storage.Delete(model);
        }
    }
}
