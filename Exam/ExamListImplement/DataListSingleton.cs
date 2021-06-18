using ExamListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamListImplement
{
    public class DataListSingleton
    {
        public static DataListSingleton instance;
        public List<MainClass> MainClasses { set; get; }
        public List<ExtraClass> ExtraClasses { set; get; }
        private DataListSingleton()
        {
            MainClasses = new List<MainClass>();
            ExtraClasses = new List<ExtraClass>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
