using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.Interfaces;
using ExamBusinessLogic.ViewModels;
using ExamDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamDatabaseImplement.Implements
{
    public class MainClassStorage : IMainClassStorage
    {
        public List<MainClassViewModel> GetFullList()
        {
            using (var context = new ExamDatabase())
            {
                return context.MainClasses
                    .Include(rec => rec.ExtraClasses)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<MainClassViewModel> GetFilteredList(MainClassBindingModel model)
        {
            if (model == null) return null;
            using (var context = new ExamDatabase())
            {
                return context.MainClasses
                    .Include(rec=>rec.ExtraClasses)
                    .Where(rec => rec.Field1 == model.Field1)
                    .Select(CreateModel).ToList();
            }
        }

        public MainClassViewModel GetElement(MainClassBindingModel model)
        {
            if (model == null) return null;
            using (var context = new ExamDatabase())
            {
                var component = context.MainClasses
                    .Include(rec=>rec.ExtraClasses)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if(component==null) return null;
                return CreateModel(component);
            }
        }

        public void Insert(MainClassBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new MainClass(), context);
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

        public void Update(MainClassBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var mainClass = context.MainClasses
                            .FirstOrDefault(rec => rec.Id == model.Id);
                        if (mainClass == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, mainClass, context);
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

        public void Delete(MainClassBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                var mainClass = context.MainClasses
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (mainClass == null)
                {
                    throw new Exception("Элемент не найден");
                }
                context.MainClasses.Remove(mainClass);
                context.SaveChanges();
            }
        }

        private MainClass CreateModel(MainClassBindingModel model
            , MainClass MainClass, ExamDatabase context)
        {
            MainClass.DateCreate = model.DateCreate;
            MainClass.Field1 = model.Field1;
            MainClass.Field2 = model.Field2;
            if (MainClass.Id == 0) { context.Add(MainClass); }
            return MainClass;
        }

        private MainClassViewModel CreateModel(MainClass MainClass)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            MainClass.ExtraClasses
                .ForEach(rec => dict.Add((int)rec.Id, rec.Field1.ToString()));
            return new MainClassViewModel
            {
                Id = (int)MainClass.Id,
                Field1 = MainClass.Field1,
                Field2 = MainClass.Field2,
                Dictionary = dict,
                DateCreate=MainClass.DateCreate
            };
        }
    }
}
