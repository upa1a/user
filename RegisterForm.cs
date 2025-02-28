using System;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private bool RegisterUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Хеширование пароля
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);


            DBHelper dbHelper = new DBHelper();
            string query = $"INSERT INTO users (username, password_hash, role) VALUES ('{username}', '{passwordHash}', 'user')";
            try
            {
                dbHelper.ExecuteNonQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Получение данных из текстовых полей
            string username = txtRegUsername.Text;
            string password = txtRegPassword.Text;

            // Вызов метода для регистрации пользователя (пока что "заглушка")
            if (RegisterUser(username, password))
            {
                // Если регистрация прошла успешно, выводим сообщение об этом и закрываем форму
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                // Если есть проблемы с регистрацией, выводим сообщение об ошибке
                MessageBox.Show("Произошла ошибка при регистрации. Пожалуйста, проверьте данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
