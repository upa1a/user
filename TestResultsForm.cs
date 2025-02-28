using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TestSystem
{
    public partial class TestResultsForm : Form
    {
        private int _userId;

        public TestResultsForm(int userId)
        {
            InitializeComponent();
            _userId = userId; // Сохраняем userId
        }
        private void TestResultsForm_Load(object sender, EventArgs e)
        {
            // Создание столбцов для DataGridView
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn();
            columnName.Name = "Name";
            columnName.HeaderText = "Название теста";
            dgvTestResults.Columns.Add(columnName);

            DataGridViewTextBoxColumn columnDate = new DataGridViewTextBoxColumn();
            columnDate.Name = "Date";
            columnDate.HeaderText = "Дата прохождения";
            dgvTestResults.Columns.Add(columnDate);

            DataGridViewTextBoxColumn columnGrade = new DataGridViewTextBoxColumn();
            columnGrade.Name = "Grade";
            columnGrade.HeaderText = "Оценка";
            dgvTestResults.Columns.Add(columnGrade);

            LoadTestResults(); // Загружаем результаты тестов
        }

        private void LoadTestResults()
        {
            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
            string query = @"
        SELECT t.name, tr.test_id, tr.score, tr.date
        FROM test_results tr
        INNER JOIN tests t ON tr.test_id = t.id
        WHERE tr.user_id = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", _userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string testName = reader["name"].ToString();
                            int testId = Convert.ToInt32(reader["test_id"]);
                            int score = Convert.ToInt32(reader["score"]);
                            DateTime date = Convert.ToDateTime(reader["date"]); // Получаем дату из базы данных

                            // Добавляем строку в DataGridView
                            dgvTestResults.Rows.Add(testName, date.ToString(), score); // Отображаем дату прохождения теста
                        }
                    }
                }
            }
        }
    }
}