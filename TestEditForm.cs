using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class TestEditForm : Form
    {
        private int? _testId; //идентификатор для редактирования теста
        private int _currentUserId;

        public TestEditForm(int? testId = null)
        {
            InitializeComponent();
            _testId = testId;
            LoadQuestions(); // Загружаем вопросы
            _testId = testId;
            LoadCategories();

            if (_testId.HasValue)
            {
                LoadTestData();
            }
        }
        private void LoadQuestions()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                // Проверяем, что _testId имеет значение
                if (_testId.HasValue)
                {
                    // Получаем вопросы для данного теста
                    string selectQuestionsQuery = $"SELECT id, question_text FROM questions WHERE test_id = {_testId.Value}";
                    DataTable questions = dbHelper.ExecuteQuery(selectQuestionsQuery);

                    // Очищаем DataGridView
                    dgvQuestions.Rows.Clear();
                    dgvQuestions.Columns.Clear();

                    // Создаем столбцы для DataGridView
                    dgvQuestions.Columns.Add("id", "ID");
                    dgvQuestions.Columns.Add("question_text", "Текст вопроса");

                    // Заполняем DataGridView данными из DataTable
                    foreach (DataRow row in questions.Rows)
                    {
                        dgvQuestions.Rows.Add(row["id"], row["question_text"]);
                    }
                }
                else
                {
                    // Если _testId не имеет значения, выводим сообщение
                    dgvQuestions.Rows.Clear();
                    dgvQuestions.Columns.Clear();
                    MessageBox.Show("Сохраните тест перед добавлением вопросов.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке вопросов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void LoadCategories()
        {
            DBHelper dbHelper = new DBHelper();
            DataTable categories = dbHelper.ExecuteQuery("SELECT name FROM categories");
            foreach (DataRow row in categories.Rows)
            {
                cmbTestCategory.Items.Add(row["name"].ToString());
            }
        }
        private void LoadTestData()
        {
            DBHelper dbHelper = new DBHelper();
            DataTable test = dbHelper.ExecuteQuery($"SELECT name, description, category_id FROM tests WHERE id = {_testId}");
            if (test.Rows.Count > 0)
            {
                txtTestName.Text = test.Rows[0]["name"].ToString();
                txtTestDescription.Text = test.Rows[0]["description"].ToString();
                // Поиск категории по ID
                int categoryId = Convert.ToInt32(test.Rows[0]["category_id"]);
                string categoryName = dbHelper.ExecuteScalar($"SELECT name FROM categories WHERE id = {categoryId}")?.ToString();
                cmbTestCategory.SelectedItem = categoryName;

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mainForm = new MainForm(_currentUserId);
            mainForm.ShowDialog();
        }
        private void btnSaveTest_Click(object sender, EventArgs e)
        {
            string testName = txtTestName.Text.Trim();
            string testDescription = txtTestDescription.Text.Trim();
            string testCategory = cmbTestCategory.SelectedItem?.ToString().Trim();

            if (string.IsNullOrEmpty(testName) || string.IsNullOrEmpty(testDescription) || string.IsNullOrEmpty(testCategory))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DBHelper dbHelper = new DBHelper();
            // Получение ID выбранной категории
            int categoryId = Convert.ToInt32(dbHelper.ExecuteScalar($"SELECT id FROM categories WHERE name = '{testCategory}'"));

            try
            {
                if (_testId.HasValue)
                {
                    // Обновление существующего теста
                    dbHelper.ExecuteNonQuery($"UPDATE tests SET name = '{testName}', description = '{testDescription}', category_id = {categoryId}  WHERE id = {_testId}");
                }
                else
                {
                    // Добавление нового теста
                    dbHelper.ExecuteNonQuery($"INSERT INTO tests (name, description, category_id) VALUES ('{testName}', '{testDescription}', {categoryId})");
                }

                MessageBox.Show("Тест успешно сохранен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                MainForm mainForm = new MainForm(_currentUserId);
                mainForm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении теста: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            // Проверяем, что название теста не пустое
            string testName = txtTestName.Text;
            if (string.IsNullOrEmpty(testName))
            {
                MessageBox.Show("Сохраните тест перед добавлением вопросов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем, что тест уже сохранен и имеет ID
            if (!_testId.HasValue)
            {
                MessageBox.Show("Сохраните тест перед добавлением вопросов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QuestionEditForm questionEditForm = new QuestionEditForm(_testId.Value);
            questionEditForm.ShowDialog();

            // Обновляем список вопросов после закрытия QuestionEditForm
            LoadQuestions();
        }
        private void btnAddQuestion_Click_1(object sender, EventArgs e)
        {
            // Получаем название теста из текстового поля
            string testName = txtTestName.Text;

            // Проверяем, что название теста не пустое
            if (string.IsNullOrEmpty(testName))
            {
                MessageBox.Show("Сохраните тест перед добавлением вопросов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DBHelper dbHelper = new DBHelper();

            // Если _testId не установлено, получаем его из базы данных по названию теста
            if (!_testId.HasValue)
            {
                object testId = dbHelper.ExecuteScalar($"SELECT id FROM tests WHERE name = '{testName}'");
                if (testId == null)
                {
                    MessageBox.Show("Сохраните тест перед добавлением вопросов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _testId = Convert.ToInt32(testId);
            }

            QuestionEditForm questionEditForm = new QuestionEditForm(_testId.Value);
            questionEditForm.ShowDialog();

            // Обновляем список вопросов после закрытия QuestionEditForm
            LoadQuestions();
        }
        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в DataGridView
            if (dgvQuestions.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного вопроса
                int questionId = Convert.ToInt32(dgvQuestions.SelectedRows[0].Cells["id"].Value);

                // Подтверждаем удаление вопроса
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить этот вопрос?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DBHelper dbHelper = new DBHelper();
                    MySqlConnection connection = dbHelper.Connection; // Получаем объект соединения через свойство

                    try
                    {
                        dbHelper.OpenConnection();
                        MySqlTransaction transaction = connection.BeginTransaction(); // Начинаем транзакцию

                        try
                        {
                            // Удаляем ответы пользователей, связанные с этим вопросом
                            string deleteUserAnswersQuery = "DELETE FROM user_answers WHERE question_id = @questionId";
                            MySqlCommand deleteUserAnswersCommand = new MySqlCommand(deleteUserAnswersQuery, connection, transaction); // Передаем транзакцию
                            deleteUserAnswersCommand.Parameters.AddWithValue("@questionId", questionId);
                            deleteUserAnswersCommand.ExecuteNonQuery();

                            // Удаляем варианты ответов, связанные с этим вопросом
                            string deleteAnswersQuery = "DELETE FROM answers WHERE question_id = @questionId";
                            MySqlCommand deleteAnswersCommand = new MySqlCommand(deleteAnswersQuery, connection, transaction); // Передаем транзакцию
                            deleteAnswersCommand.Parameters.AddWithValue("@questionId", questionId);
                            deleteAnswersCommand.ExecuteNonQuery();

                            // Удаляем вопрос из таблицы questions
                            string deleteQuestionQuery = "DELETE FROM questions WHERE id = @questionId";
                            MySqlCommand deleteQuestionCommand = new MySqlCommand(deleteQuestionQuery, connection, transaction); // Передаем транзакцию
                            deleteQuestionCommand.Parameters.AddWithValue("@questionId", questionId);
                            int rowsAffected = deleteQuestionCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Вопрос успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось удалить вопрос.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            transaction.Commit(); // Подтверждаем транзакцию
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Откатываем транзакцию в случае ошибки
                            throw; // Пробрасываем исключение выше
                        }

                        // Обновляем список вопросов в DataGridView
                        LoadQuestions();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении вопроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        dbHelper.CloseConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите вопрос для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
