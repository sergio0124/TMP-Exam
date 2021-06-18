using ExamBusinessLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace ExamView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public ReportBusinessLogic logic;
        public FormMain(ReportBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void extraClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormExtraClasses>();
            form.ShowDialog();
        }

        private void mainClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMainClasses>();
            form.ShowDialog();
        }

        private void отчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReport>();
            form.ShowDialog();
        }
    }
}
