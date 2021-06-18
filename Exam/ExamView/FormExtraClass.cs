using ExamBusinessLogic.BindingModels;
using ExamBusinessLogic.BusinessLogics;
using ExamBusinessLogic.Enums;
using ExamBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace ExamView
{
    public partial class FormExtraClass : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly MainClassBusinessLogic logic;
        private readonly ExtraClassBusinessLogic extralogic;
        public FormExtraClass(MainClassBusinessLogic logic, ExtraClassBusinessLogic extralogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.extralogic = extralogic;
        }

        private void FormExtraClass_Load(object sender, EventArgs e)
        {
            try
            {
                List<MainClassViewModel> list = logic.Read(null);
                if (list != null)
                {
                    comboBoxMainClass.DisplayMember = "Field1";
                    comboBoxMainClass.ValueMember = "Id";
                    comboBoxMainClass.DataSource = list;
                    comboBoxMainClass.SelectedItem = null;
                }
                var types = Enum.GetValues(typeof(MyEnum));
                comboBoxType.DataSource = types;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (id.HasValue) {
                ExtraClassViewModel model = extralogic.Read(new ExtraClassBindingModel { Id=id})[0];
                if (model != null) {
                    textBoxField1.Text = model.Field1.ToString();
                    textBoxField2.Text = model.Field2;
                    comboBoxMainClass.SelectedValue = model.MainClassId;
                    comboBoxType.SelectedItem = model.Type;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxField1.Text))
            {
                MessageBox.Show("Заполните поле Field1", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxField2.Text))
            {
                MessageBox.Show("Заполните поле Field1", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMainClass.SelectedValue == null)
            {
                MessageBox.Show("Выберите MainClass", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxType.SelectedValue == null)
            {
                MessageBox.Show("Выберите Type", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                MyEnum status;
                Enum.TryParse<MyEnum>(comboBoxType.SelectedValue.ToString(), out status);
                extralogic.CreateOrUpdate(new ExtraClassBindingModel
                {
                    Field1 = Convert.ToInt32(textBoxField1.Text),
                    Field2 = textBoxField2.Text,
                    MainClassId = Convert.ToInt32(comboBoxMainClass.SelectedValue),
                    Type = status,
                    DateCreate = DateTime.Now,
                    Id=id
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
