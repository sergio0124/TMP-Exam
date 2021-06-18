using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using ExamDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamDatabaseImplement.Implements
{
    public class ExtraClassStorage : IExtraClassStorage
    {
        public List<ExtraClassViewModel> GetFullList()
        {
            using (var context = new ExamDatabase())
            {
                return context.ExtraClasses
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<ExtraClassViewModel> GetFilteredList(ExtraClassBindingModel model)
        {
            if (model == null) return null;
            using (var context = new ExamDatabase())
            {
                return context.ExtraClasses
                    .Where(rec => rec.Field1 == model.Field1)
                    .Select(CreateModel).ToList();
            }
        }

        public ExtraClassViewModel GetElement(ExtraClassBindingModel model)
        {
            if (model == null) return null;
            using (var context = new ExamDatabase())
            {
                var component = context.ExtraClasses
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (component == null) return null;
                return CreateModel(component);
            }
        }

        public void Insert(ExtraClassBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new ExtraClass(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(ExtraClassBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var extraClass = context.ExtraClasses
                            .FirstOrDefault(rec => rec.Id == model.Id);
                        if (extraClass == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, extraClass, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(ExtraClassBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                var extrsClass = context.ExtraClasses
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (extrsClass == null)
                {
                    throw new Exception("Элемент не найден");
                }
                context.ExtraClasses.Remove(extrsClass);
                context.SaveChanges();
            }
        }

        private ExtraClass CreateModel(ExtraClassBindingModel model
            , ExtraClass ExtraClass, ExamDatabase context)
        {
            ExtraClass.DateCreate = model.DateCreate;
            ExtraClass.Field1 = model.Field1;
            ExtraClass.Field2 = model.Field2;
            ExtraClass.Type = model.Type;
            MainClass mainClass = context.MainClasses.FirstOrDefault(rec => rec.Id == model.MainClassId);
            ExtraClass.MainClassId = mainClass.Id;
            ExtraClass.MainClassName = mainClass.Field1.ToString();
            ExtraClass.MainClass = mainClass;
            if (ExtraClass.Id==0) context.Add(ExtraClass);
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
