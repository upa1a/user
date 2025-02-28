using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class AdminForm : Form
    {
        private string _currentUserRole;

        public AdminForm(string userRole)
        {
            InitializeComponent();
            _currentUserRole = userRole;

            // Проверяем, является ли пользователь администратором
            if (_currentUserRole != "admin")
            {
                MessageBox.Show("У вас нет прав для доступа к этой форме.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            LoadUsers();
            LoadTests();
            LoadResults();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit(); // Закрываем приложение
            }
        }
        private void LoadUsers()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string selectUsersQuery = "SELECT id, username, role FROM users";
                DataTable users = dbHelper.ExecuteQuery(selectUsersQuery);

                dgvUsers.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void LoadTests()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string selectTestsQuery = "SELECT id, name, description, category_id FROM tests";
                DataTable tests = dbHelper.ExecuteQuery(selectTestsQuery);

                dgvAdminTests.DataSource = tests;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке тестов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void LoadResults()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string selectResultsQuery = "SELECT user_id, test_id, score FROM test_results";
                DataTable results = dbHelper.ExecuteQuery(selectResultsQuery);

                dgvAdminResults.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке результатов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления нового пользователя
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();

            // Обновляем список пользователей в DataGridView
            LoadUsers();
        }
        private void btnAddTest_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления нового теста
            TestEditForm testEditForm = new TestEditForm();
            testEditForm.ShowDialog();

            // Обновляем список тестов в DataGridView
            LoadTests();
        }
        private void btnEditTest_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в DataGridView
            if (dgvAdminTests.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного теста
                int testId = Convert.ToInt32(dgvAdminTests.SelectedRows[0].Cells["id"].Value);

                // Открываем форму для редактирования теста, передавая ID выбранного теста
                TestEditForm testEditForm = new TestEditForm(testId);
                testEditForm.ShowDialog();

                // Обновляем список тестов в DataGridView
                LoadTests();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тест для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в DataGridView
            if (dgvUsers.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного пользователя
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["id"].Value);

                // Подтверждаем удаление пользователя
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DBHelper dbHelper = new DBHelper();
                    try
                    {
                        dbHelper.OpenConnection();

                        // Удаляем пользователя из таблицы users
                        string deleteUserQuery = "DELETE FROM users WHERE id = @userId";
                        Dictionary<string, object> deleteParameters = new Dictionary<string, object>();
                        deleteParameters.Add("@userId", userId);

                        dbHelper.ExecuteNonQuery(deleteUserQuery, deleteParameters);

                        MessageBox.Show("Пользователь успешно удален.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем список пользователей в DataGridView
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        dbHelper.CloseConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnDeleteTest_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в DataGridView
            if (dgvAdminTests.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного теста
                int testId = Convert.ToInt32(dgvAdminTests.SelectedRows[0].Cells["id"].Value);

                // Подтверждаем удаление теста
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить этот тест?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DBHelper dbHelper = new DBHelper();
                    try
                    {
                        dbHelper.OpenConnection();

                        string deleteTestResultsQuery = "DELETE FROM test_results WHERE test_id = @testId";
                        Dictionary<string, object> deleteParameters = new Dictionary<string, object>();
                        deleteParameters.Add("@testId", testId);
                        dbHelper.ExecuteNonQuery(deleteTestResultsQuery, deleteParameters);

                        // Удаляем тест из таблицы tests
                        string deleteTestQuery = "DELETE FROM tests WHERE id = @testId";
                        //deleteParameters.Add("@testId", testId);

                        dbHelper.ExecuteNonQuery(deleteTestQuery, deleteParameters);

                        MessageBox.Show("Тест успешно удален.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем список тестов в DataGridView
                        LoadTests();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении теста: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        dbHelper.CloseConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тест для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}