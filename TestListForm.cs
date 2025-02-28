using System;
using System.Data;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class TestListForm : Form
    {
        private int _currentUserId;

        public TestListForm(int userId)
        {
            InitializeComponent();
            _currentUserId = userId; // Сохраняем ID пользователя
        }
        private void TestListForm_Load(object sender, EventArgs e)
        {
            // Загрузка данных о тестах из БД
            LoadTests();
        }

        public void LoadTests()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string query = @"
                    SELECT
                        id,
                        name,
                        description,
                        (SELECT name FROM categories WHERE id = category_id) AS category_name,
                        (SELECT COUNT(*) FROM questions WHERE test_id = t.id) AS question_count,
                        CASE
                            WHEN EXISTS (SELECT 1 FROM test_results WHERE test_id = t.id AND user_id = @userId) THEN 1
                            ELSE 0
                        END AS is_passed
                    FROM tests t";

                // Заменяем @userId на ID текущего пользователя
                query = query.Replace("@userId", _currentUserId.ToString());
                DataTable testsData = dbHelper.ExecuteQuery(query);

                // Отображение данных в DataGridView
                dgvTests.DataSource = testsData;

                // Настраиваем отображение столбцов
                if (dgvTests.Columns.Contains("name"))
                {
                    dgvTests.Columns["name"].DataPropertyName = "name";
                }
                if (dgvTests.Columns.Contains("description"))
                {
                    dgvTests.Columns["description"].DataPropertyName = "description";
                }
                if (dgvTests.Columns.Contains("category_name")) 
                {
                    dgvTests.Columns["category_name"].DataPropertyName = "category_name";
                }

                // Скрываем столбец id
                if (dgvTests.Columns.Contains("id"))
                {
                    dgvTests.Columns["id"].Visible = false;
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

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (dgvTests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите тест.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Получение выбранного теста
            DataGridViewRow selectedRow = dgvTests.SelectedRows[0];
            int testId = Convert.ToInt32(selectedRow.Cells["id"].Value); // Получаем ID теста

            // Открытие формы для прохождения теста
            TestForm testForm = new TestForm(testId, _currentUserId); // Передаем ID теста и ID пользователя
            testForm.ShowDialog();
        }
    }
}