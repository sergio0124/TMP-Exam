using ExamBusinessLogic.BindingModels;
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
    public partial class FormMainClass : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly MainClassBusinessLogic logic;
        private readonly ExtraClassBusinessLogic extralogic;
        public FormMainClass(MainClassBusinessLogic logic, ExtraClassBusinessLogic extralogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.extralogic = extralogic;
        }

        private void FormMainClass_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new MainClassBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxField1.Text = view.Field1.ToString();
                        textBoxField2.Text = view.Field2;
                        foreach (var row in view.Dictionary)
                        {
                            dataGridView.Rows.Add(new object[] {row.Key, row.Value});
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxField1.Text))
            {
                MessageBox.Show("Заполните Field1", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxField2.Text))
            {
                MessageBox.Show("Заполните Field2", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new MainClassBindingModel
                {
                    Id = id,
                    Field1 = Convert.ToInt32(textBoxField1.Text),
                    Field2=textBoxField2.Text,
                    DateCreate=DateTime.Now
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
