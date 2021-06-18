using ExamBusinessLogic.BusinessLogics;
using ExamBusinessLogic.Interfaces;
using ExamListImplement.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

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
            return currentContainer;
        }

    }
}
