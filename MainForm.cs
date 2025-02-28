using System;
using System.Data;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class MainForm : Form
    {
        private int _currentUserId;
        private string _currentUserRole;

        public MainForm(int userId)
        {
            InitializeComponent();
            LoadTestsToMenu(); 
            _currentUserId = userId; 
        }

        public MainForm(int userId, string userRole)
        {
            InitializeComponent();
            _currentUserId = userId;
            _currentUserRole = userRole;
            LoadUserInfo();
            LoadTestsToMenu();

        }
        private void LoadUserInfo()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                // Если пользователь является администратором, показываем кнопку AdminPanel
                if (_currentUserRole == "admin")
                {
                    // Ничего не делаем.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке информации о пользователе: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void LoadTestsToMenu()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();
                string selectTestsQuery = "SELECT id, name FROM tests";
                DataTable tests = dbHelper.ExecuteQuery(selectTestsQuery);

                testEditMenuItem.DropDownItems.Clear();
                testEditMenuItem.DropDownItems.Add(addTestMenuItem);

                foreach (DataRow row in tests.Rows)
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(row["name"].ToString());
                    menuItem.Tag = Convert.ToInt32(row["id"]);
                    menuItem.Click += TestEditMenuItem_Click;
                    testEditMenuItem.DropDownItems.Add(menuItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка тестов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void TestEditMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            int testId = (int)menuItem.Tag; // Получаем ID теста из Tag

            // Открываем форму TestEditForm, передавая ID теста в конструктор
            TestEditForm testEditForm = new TestEditForm(testId);
            testEditForm.ShowDialog();
            this.Close();
        }

        /*private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            // Переходим на форму AdminForm
            AdminForm adminForm = new AdminForm(_currentUserRole);
            adminForm.Show();
            this.Hide();
        }*/
        private void resultsMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы результатов теста
            TestResultsForm resultsForm = new TestResultsForm(_currentUserId);
            resultsForm.ShowDialog();
        }
        private void adminMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, является ли пользователь администратором
                if (_currentUserRole != "admin")
                {
                    throw new UnauthorizedAccessException("У вас нет прав для доступа к этой форме.");
                }

                // Переходим на форму AdminForm
                AdminForm adminForm = new AdminForm(_currentUserRole);
                adminForm.Show();
                this.Hide();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            // Выход из приложения
            Application.Exit();
        }
        private TestListForm _testListForm;
        private void addTestMenuItem_Click(object sender, EventArgs e)
        {
            // Открытие формы для редактирования теста
            TestEditForm testEditForm = new TestEditForm();
            testEditForm.ShowDialog();

            // Перезагрузка списка тестов, если форма не закрыта
            if (_testListForm != null && !_testListForm.IsDisposed)
            {
                _testListForm.LoadTests();
            }
        }
        private void testListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            TestListForm testListForm = new TestListForm(_currentUserId); // Передаем ID пользователя в TestListForm
            testListForm.ShowDialog();
        }
    }
}