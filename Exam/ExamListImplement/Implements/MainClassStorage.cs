using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Enums;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using ExamListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamListImplement.Implements
{
    public class MainClassStorage : IMainClassStorage
    {
        private readonly DataListSingleton source;
        public MainClassStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MainClassViewModel> GetFullList()
        {
            return source.MainClasses.Select(CreateModel).ToList();
        }
        public List<MainClassViewModel> GetFilteredList(MainClassBindingModel model)
        {
            return (model == null) ? null : source.MainClasses
                .Where(rec => (model.DateFrom.HasValue 
                && model.DateTo.HasValue 
                && rec.DateCreate >= model.DateFrom 
                && rec.DateCreate <= model.DateTo))
                .Select(CreateModel).ToList();
        }

        public MainClassViewModel GetElement(MainClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var component = source.MainClasses
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Field1==model.Field1);
            return component != null ?
                CreateModel(component) : null;
        }

        public void Insert(MainClassBindingModel model)
        {
            MainClass tempMainClass = new MainClass
            {
                Id = 1
            };           
            source.MainClasses.ForEach(rec => rec.Id++);
            source.MainClasses.Add(CreateModel(model, tempMainClass));
        }

        public void Update(MainClassBindingModel model)
        {
            MainClass tempMainClass = source.MainClasses
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (tempMainClass == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempMainClass);
        }

        public void Delete(MainClassBindingModel model)
        {
            int index = source.MainClasses.FindIndex(rec => rec.Id == model.Id);
            if (index != -1)
            {
                source.MainClasses.RemoveAt(index);
                return;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private MainClass CreateModel(MainClassBindingModel model, MainClass MainClass)
        {
            MainClass.DateCreate = model.DateCreate;
            MainClass.Field1 = model.Field1;
            MainClass.Field2 = model.Field2;
            return MainClass;
        }

        private MainClassViewModel CreateModel(MainClass MainClass)
        {
            Dictionary<int, (string, MyEnum, DateTime)> dict = 
                new Dictionary<int, (string, MyEnum, DateTime)>();
            source.ExtraClasses
                .ForEach(rec => dict.Add((int)rec.Id, (rec.Field1.ToString(), rec.Type, rec.DateCreate)));
            return new MainClassViewModel
            {
                Id = (int)MainClass.Id,
                Field1 = MainClass.Field1,
                Field2 = MainClass.Field2,
                Dictionary = dict,
                DateCreate = MainClass.DateCreate
            };
        }
    }
}
