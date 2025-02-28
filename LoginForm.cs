using System;
using System.Data;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        DBHelper dbHelper = new DBHelper();

        private bool AuthenticateUser(string username, string password, out int userId, out string userRole)
        {
            userId = -1; // Инициализируем userId значением по умолчанию
            userRole = ""; // Инициализируем userRole значением по умолчанию

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string query = $"SELECT id, password_hash, role FROM users WHERE username = '{username}'"; // Получаем ID и роль пользователя
                DataTable result = dbHelper.ExecuteQuery(query);

                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string passwordHash = result.Rows[0]["password_hash"].ToString();
                userId = Convert.ToInt32(result.Rows[0]["id"]); // Получаем ID пользователя
                userRole = result.Rows[0]["role"].ToString(); // Получаем роль пользователя

                if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Получаем логин и пароль из текстовых полей
            string username = txtLoginUsername.Text;
            string password = txtLoginPassword.Text;
            int userId; // Объявляем переменную для хранения ID пользователя
            string userRole; // Объявляем переменную для хранения роли пользователя

            // Вызываем метод для аутентификации пользователя
            if (AuthenticateUser(username, password, out userId, out userRole))
            {
                //закрытие формы авторизации и показ главной формы.
                this.Hide();
                if (userRole == "admin")
                {
                    AdminForm adminForm = new AdminForm(userRole);
                    adminForm.Show();
                }
                else
                {
                    MainForm mainForm = new MainForm(userId, userRole); // Передаем ID пользователя и роль пользователя в MainForm
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.ShowDialog();

                }
            }
            else
            {
                // Вывод сообщения об ошибке аутентификации.
                MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
