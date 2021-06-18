using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ExamBusinessLogic.BusinessLogics
{
    public class MainClassBusinessLogic
    {
        private readonly IMainClassStorage Storage;
        public MainClassBusinessLogic(IMainClassStorage MainClassStorage)
        {
            Storage = MainClassStorage;
        }

        public List<MainClassViewModel> Read(MainClassBindingModel model)
        {
            if (model == null)
            {
                return Storage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MainClassViewModel> { Storage.GetElement(model) };
            }
            return Storage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MainClassBindingModel model)
        {
            var element = Storage.GetElement(new MainClassBindingModel
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
        public void Delete(MainClassBindingModel model)
        {
            var element = Storage.GetElement(new MainClassBindingModel
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
