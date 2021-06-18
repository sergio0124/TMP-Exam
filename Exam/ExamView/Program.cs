using ExamBusinessLogic.BusinessLogics;
using ExamBusinessLogic.Interfaces;
using ExamDatabaseImplement.Implements;
using System;
using System.Collections.Generic;
using LawFirmBusinessLogic.Attributes;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
//https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/standard-date-and-time-format-strings
//Formats

namespace ExamView
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IMainClassStorage, MainClassStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainClassBusinessLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExtraClassStorage, ExtraClassStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ExtraClassBusinessLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportBusinessLogic>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }

        public static void ConfigGrid<T>(List<T> data, DataGridView grid)
        {
            var type = typeof(T);
            var config = new List<string>();
            grid.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                // получаем список атрибутов
                var attributes =
                prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    foreach (var attr in attributes)
                    {
                        // ищем нужный нам атрибут
                        if (attr is ColumnAttribute columnAttr)
                        {
                            config.Add(prop.Name);
                            var column = new DataGridViewTextBoxColumn
                            {
                                Name = prop.Name,
                                ReadOnly = true,
                                HeaderText = columnAttr.Title,
                                Visible = columnAttr.Visible,
                                Width = columnAttr.Width,
                                DefaultCellStyle = new DataGridViewCellStyle { Format = columnAttr.Format ?? "" }
                            };
                            if (columnAttr.GridViewAutoSize !=
                            GridViewAutoSize.None)
                            {
                                column.AutoSizeMode =
                                (DataGridViewAutoSizeColumnMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnMode),
                                columnAttr.GridViewAutoSize.ToString());
                            }
                            grid.Columns.Add(column);
                        }
                    }
                }
            }
            // добавляем строки
            foreach (var elem in data)
            {
                List<object> objs = new List<object>();
                foreach (var conf in config)
                {
                    var value =
                    elem.GetType().GetProperty(conf).GetValue(elem);
                    objs.Add(value);
                }
                grid.Rows.Add(objs.ToArray());
            }
        }
    }
}
