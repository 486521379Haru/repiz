using System;
using System.Windows.Forms;

namespace AISTex
{
    public partial class enter : Form
    {
        private const string ValidUsername = "admin";
        private const string ValidPassword = "123";
        public enter()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string username = txtName.Text;
            string password = txtPassword.Text; 

            if (username == ValidUsername && password == ValidPassword)
            {
                MessageBox.Show("Вход выполнен успешно!");
                AddEmployeeForm AddEmployeeForm = new AddEmployeeForm();
                AddEmployeeForm.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.");
            }
        }
    }
}