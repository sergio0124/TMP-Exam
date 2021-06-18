using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using ExamListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamListImplement.Implements
{
    public class ExtraClassStorage : IExtraClassStorage
    {
        private readonly DataListSingleton source;
        public ExtraClassStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ExtraClassViewModel> GetFullList()
        {
            return source.ExtraClasses.Select(CreateModel).ToList();
        }
        public List<ExtraClassViewModel> GetFilteredList(ExtraClassBindingModel model)
        {
            return (model == null) ? null : source.ExtraClasses.Where(rec => rec.MainClassId == model.MainClassId)
                .Select(CreateModel).ToList();
        }

        public ExtraClassViewModel GetElement(ExtraClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var component = source.ExtraClasses
                .FirstOrDefault(rec => rec.Id == model.Id);
            return component != null ?
                CreateModel(component) : null;
        }

        public void Insert(ExtraClassBindingModel model)
        {
            ExtraClass tempExtraClass = new ExtraClass
            {
                Id = 1
            };
            source.ExtraClasses.ForEach(rec => rec.Id++);
            source.ExtraClasses.Add(CreateModel(model, tempExtraClass));
        }

        public void Update(ExtraClassBindingModel model)
        {
            ExtraClass tempExtraClass = source.ExtraClasses
                .FirstOrDefault(rec=>rec.Id==model.Id);
            if (tempExtraClass == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempExtraClass);
        }

        public void Delete(ExtraClassBindingModel model)
        {
            int index = source.ExtraClasses.FindIndex(rec => rec.Id == model.Id);
            if (index!=-1)
            {
                source.ExtraClasses.RemoveAt(index);
                return;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private ExtraClass CreateModel(ExtraClassBindingModel model, ExtraClass ExtraClass)
        {
            ExtraClass.DateCreate = model.DateCreate;
            ExtraClass.Field1 = model.Field1;
            ExtraClass.Field2 = model.Field2;
            ExtraClass.Type = model.Type;
            MainClass mainClass = source.MainClasses.FirstOrDefault(rec => rec.Id == model.MainClassId);
            ExtraClass.MainClassId = (int)mainClass.Id;
            ExtraClass.MainClassName = mainClass.Field1.ToString();
            ExtraClass.mainClass = mainClass;
            return ExtraClass;
        }

        private ExtraClassViewModel CreateModel(ExtraClass ExtraClass)
        {
            return new ExtraClassViewModel
            {
                Id = (int)ExtraClass.Id,
                Field1 = ExtraClass.Field1,
                Field2 = ExtraClass.Field2,
                MainClassId = ExtraClass.MainClassId,
                MainClassName = ExtraClass.MainClassName,
                Type = ExtraClass.Type,
                DateCreate = ExtraClass.DateCreate
            };
        }
    }
}
