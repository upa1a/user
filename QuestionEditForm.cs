using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class QuestionEditForm : Form
    {
        private int _testId;
        private int? _questionId;
        private DataTable _answers = new DataTable();

        public QuestionEditForm(int testId, int? questionId = null)
        {
            InitializeComponent();
            _testId = testId;
            _questionId = questionId;

            // Инициализируем DataTable для хранения ответов
            _answers = new DataTable();
            _answers.Columns.Add("id", typeof(int));
            _answers.Columns.Add("text", typeof(string));
            _answers.Columns.Add("is_correct", typeof(bool));
            dgvAnswers.DataSource = _answers; // Привязываем DataTable к DataGridView

            LoadQuestionData(); 
        }
        private void LoadQuestionData()
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                // Объявляем переменные здесь, чтобы они были доступны во всем методе
                string questionType = "Текстовый"; // Значение по умолчанию
                string questionQuery;
                Dictionary<string, object> questionParameters = new Dictionary<string, object>();
                DataTable question = new DataTable(); // Инициализируем DataTable

                if (_questionId.HasValue)
                {
                    // Загружаем данные вопроса из базы данных
                    questionQuery = "SELECT question_text, question_type FROM questions WHERE id = @questionId";
                    questionParameters.Add("@questionId", _questionId);

                    question = dbHelper.ExecuteQuery(questionQuery, questionParameters);

                    if (question.Rows.Count > 0)
                    {
                        // Проверяем, что значение text не равно DBNull
                        txtQuestionText.Text = question.Rows[0]["question_text"] == DBNull.Value ? "" : question.Rows[0]["question_text"].ToString();

                        // Загружаем тип вопроса из базы данных и устанавливаем его в ComboBox
                        questionType = question.Rows[0]["question_type"] == DBNull.Value ? "Текстовый" : question.Rows[0]["question_type"].ToString();
                        cbQuestionType.SelectedItem = questionType;
                    }
                    else
                    {
                        // Если вопрос с таким ID не найден, выводим сообщение об ошибке
                        MessageBox.Show($"Вопрос с ID {_questionId} не найден в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Прекращаем выполнение метода
                    }

                    // Загружаем варианты ответов из базы данных
                    string answersQuery = "SELECT id, text, is_correct, correct_answer_text FROM answers WHERE question_id = @questionId"; // Изменено: добавили correct_answer_text
                    Dictionary<string, object> answersParameters = new Dictionary<string, object>();
                    answersParameters.Add("@questionId", _questionId);

                    DataTable answers = dbHelper.ExecuteQuery(answersQuery, answersParameters);

                    // Очищаем _answers перед загрузкой новых данных
                    _answers.Rows.Clear();

                    if (questionType == "Текстовый") // Если вопрос - текстовый
                    {
                        if (answers.Rows.Count > 0)
                        {
                            // Если есть сохраненный ответ, загружаем его в текстовое поле
                            txtCorrectAnswer.Text = answers.Rows[0]["correct_answer_text"] == DBNull.Value ? "" : answers.Rows[0]["correct_answer_text"].ToString();
                        }
                    }
                    else // Для типов вопросов "Выбор одного" и "Выбор нескольких"
                    {
                        dgvAnswers.Columns.Clear(); // Очищаем старые столбцы
                        dgvAnswers.AutoGenerateColumns = false; // Отключаем автоматическую генерацию столбцов

                        // Создаем столбцы вручную
                        DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
                        idColumn.DataPropertyName = "id"; // Связываем с именем столбца в DataTable
                        idColumn.HeaderText = "ID";
                        idColumn.Name = "id";
                        idColumn.Visible = false; // Скрываем столбец id

                        DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                        textColumn.DataPropertyName = "text";
                        textColumn.HeaderText = "Текст ответа";
                        textColumn.Name = "text";

                        DataGridViewCheckBoxColumn isCorrectColumn = new DataGridViewCheckBoxColumn();
                        isCorrectColumn.DataPropertyName = "is_correct";
                        isCorrectColumn.HeaderText = "Правильный?";
                        isCorrectColumn.Name = "is_correct";

                        // Добавляем столбцы в DataGridView
                        dgvAnswers.Columns.Add(idColumn);
                        dgvAnswers.Columns.Add(textColumn);
                        dgvAnswers.Columns.Add(isCorrectColumn);

                        foreach (DataRow row in answers.Rows)
                        {
                            _answers.Rows.Add(row["id"], row["text"], row["is_correct"]); // Добавили id
                        }
                    }
                }
                else
                {
                    // Если это новый вопрос, добавляем хотя бы один вариант ответа
                    _answers.Rows.Add(null, "", false); //null для id
                                                        // Устанавливаем значение по умолчанию для ComboBox
                    cbQuestionType.SelectedIndex = 0; // Выбираем первый элемент ("Текстовый")
                }

                cbQuestionType.SelectedItem = questionType;
                cbQuestionType_SelectedIndexChanged(null, EventArgs.Empty); // Вызываем обработчик события
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных вопроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private int? GetQuestionIdToDelete() // Изменили тип возвращаемого значения на int?
        {
            if (dgvAnswers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAnswers.SelectedRows[0];
                // Проверяем, что ячейка "id" существует и содержит данные
                if (selectedRow.Cells["id"].Value != null && selectedRow.Cells["id"].Value != DBNull.Value && !string.IsNullOrEmpty(selectedRow.Cells["id"].Value.ToString()))
                {
                    try
                    {
                        int answerId = Convert.ToInt32(selectedRow.Cells["id"].Value); // Получаем ID варианта ответа
                        Console.WriteLine($"Удаляем вариант ответа с ID: {answerId}");
                        return answerId; // Возвращаем int, если все хорошо
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Ошибка: Неверный формат ID варианта ответа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null; // Возвращаем null при ошибке
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите вариант ответа для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null; // Возвращаем null, если ячейка пуста
                }
            }
            else
            {
                MessageBox.Show("Выберите вариант ответа для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null; // Возвращаем null, если строка не выбрана
            }
        }
        private void DeleteQuestion(int questionId)
        {
            DBHelper dbHelper = new DBHelper();
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;

            try
            {
                dbHelper.OpenConnection();
                connection = dbHelper.Connection;
                transaction = connection.BeginTransaction();

                Console.WriteLine($"Удаляем вопрос с ID: {questionId}");

                // 2. Удаляем все варианты ответа, связанные с вопросом
                string deleteOptionsQuery = "DELETE FROM options WHERE question_id = @QuestionId";
                MySqlCommand deleteOptionsCommand = new MySqlCommand(deleteOptionsQuery, connection, transaction);
                deleteOptionsCommand.Parameters.AddWithValue("@QuestionId", questionId);
                int optionsDeleted = deleteOptionsCommand.ExecuteNonQuery();
                Console.WriteLine($"Удалено {optionsDeleted} записей из options");

                // 3. Удаляем вопрос
                string deleteQuestionQuery = "DELETE FROM questions WHERE id = @QuestionId";
                MySqlCommand deleteQuestionCommand = new MySqlCommand(deleteQuestionQuery, connection, transaction);
                deleteQuestionCommand.Parameters.AddWithValue("@QuestionId", questionId);
                int questionDeleted = deleteQuestionCommand.ExecuteNonQuery();
                Console.WriteLine($"Удалено {questionDeleted} записей из questions");

                // 1. Удаляем все ответы пользователей, связанные с вопросом
                string deleteUserAnswersQuery = "DELETE FROM user_answers WHERE question_id = @QuestionId";
                MySqlCommand deleteUserAnswersCommand = new MySqlCommand(deleteUserAnswersQuery, connection, transaction);
                deleteUserAnswersCommand.Parameters.AddWithValue("@QuestionId", questionId);
                int userAnswersDeleted = deleteUserAnswersCommand.ExecuteNonQuery();
                Console.WriteLine($"Удалено {userAnswersDeleted} записей из user_answers");

                transaction.Commit();

                MessageBox.Show("Вопрос успешно удален.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show("Ошибка при удалении вопроса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null)
                {
                    try
                    {
                        transaction?.Dispose();
                        connection?.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при закрытии соединения: {ex.Message}");
                    }
                }
                dbHelper.CloseConnection();
            }
        }

        private void btnAddAnswer_Click(object sender, EventArgs e)
        {
            // Добавляем новую строку в DataTable
            _answers.Rows.Add(null, "", false);
        }
        private void btnDeleteAnswer_Click(object sender, EventArgs e)
        {
            // Получаем ID варианта ответа
            int? answerId = GetQuestionIdToDelete();

            if (answerId.HasValue) // Проверяем, что был выбран вариант ответа
            {
                // Удаляем вариант ответа
                DBHelper dbHelper = new DBHelper();
                try
                {
                    dbHelper.OpenConnection();
                    //  Запрос на удаление варианта ответа
                    string deleteAnswerQuery = "DELETE FROM answers WHERE id = @answerId";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@answerId", answerId.Value }
            };

                    dbHelper.ExecuteNonQuery(deleteAnswerQuery, parameters);

                    // Обновляем DataTable _answers
                    DataRow[] rowsToRemove = _answers.Select($"id = {answerId.Value}");
                    foreach (DataRow row in rowsToRemove)
                    {
                        _answers.Rows.Remove(row);
                    }

                    MessageBox.Show("Вариант ответа успешно удален.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении варианта ответа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dbHelper.CloseConnection();
                }
            }
        }
        private void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            // Получаем текст вопроса из текстового поля
            string questionText = txtQuestionText.Text;

            // Проверяем, что текст вопроса не пустой
            if (string.IsNullOrEmpty(questionText))
            {
                MessageBox.Show("Пожалуйста, введите текст вопроса.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем выбранный тип вопроса из ComboBox
            string questionType = cbQuestionType.SelectedItem.ToString();

            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                // ***Сохраняем/обновляем вопрос (questions)***
                if (!_questionId.HasValue)
                {
                    // Добавляем новый вопрос
                    string insertQuestionQuery = "INSERT INTO questions (test_id, question_text, question_type) VALUES (@testId, @questionText, @questionType); SELECT LAST_INSERT_ID();";
                    Dictionary<string, object> insertParameters = new Dictionary<string, object>();
                    insertParameters.Add("@testId", _testId);
                    insertParameters.Add("@questionText", questionText);
                    insertParameters.Add("@questionType", questionType);
                    object result = dbHelper.ExecuteScalar(insertQuestionQuery, insertParameters);
                    if (result != null && result != DBNull.Value)
                    {
                        _questionId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить ID нового вопроса.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Обновляем существующий вопрос
                    string updateQuestionQuery = "UPDATE questions SET question_text = @questionText, question_type = @questionType WHERE id = @questionId";
                    Dictionary<string, object> updateParameters = new Dictionary<string, object>();
                    updateParameters.Add("@questionText", questionText);
                    updateParameters.Add("@questionType", questionType);
                    updateParameters.Add("@questionId", _questionId);
                    dbHelper.ExecuteNonQuery(updateQuestionQuery, updateParameters);
                }

                // ***Сохраняем варианты ответов / правильный ответ (answers)***
                if (questionType == "Текстовый")
                {
                    // Получаем текст правильного ответа
                    string correctAnswerText = txtCorrectAnswer.Text;
                    if (string.IsNullOrEmpty(correctAnswerText))
                    {
                        MessageBox.Show("Пожалуйста, введите текст правильного ответа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Сохраняем правильный ответ (INSERT или UPDATE)
                    string insertOrUpdateAnswerQuery = "INSERT INTO answers (question_id, correct_answer_text) VALUES (@questionId, @correctAnswerText) ON DUPLICATE KEY UPDATE correct_answer_text = @correctAnswerText";
                    Dictionary<string, object> insertOrUpdateAnswerParameters = new Dictionary<string, object>();
                    insertOrUpdateAnswerParameters.Add("@questionId", _questionId);
                    insertOrUpdateAnswerParameters.Add("@correctAnswerText", correctAnswerText);
                    dbHelper.ExecuteNonQuery(insertOrUpdateAnswerQuery, insertOrUpdateAnswerParameters);
                }
                else
                {
                    // Проверяем, что для каждого варианта ответа введен текст
                    foreach (DataRow row in _answers.Rows)
                    {
                        string answerText = row["text"] == DBNull.Value ? "" : row["text"].ToString();
                        if (string.IsNullOrEmpty(answerText))
                        {
                            MessageBox.Show("Пожалуйста, введите текст для всех вариантов ответов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Проверки на количество правильных ответов (Выбор одного/нескольких)
                    if (questionType == "Выбор одного")
                    {
                        int correctAnswersCount = 0;
                        foreach (DataRow row in _answers.Rows)
                        {
                            bool isCorrect = row["is_correct"] == DBNull.Value ? false : Convert.ToBoolean(row["is_correct"]);
                            if (isCorrect)
                            { 
                                correctAnswersCount++; 
                            }
                        }
                        if (correctAnswersCount != 1)
                        {
                            MessageBox.Show("Для типа вопроса 'Выбор одного' должен быть выбран только один правильный ответ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (questionType == "Выбор нескольких")
                    {
                        int correctAnswersCount = 0;
                        foreach (DataRow row in _answers.Rows)
                        {
                            bool isCorrect = row["is_correct"] == DBNull.Value ? false : Convert.ToBoolean(row["is_correct"]);
                            if (isCorrect)
                            { 
                                correctAnswersCount++; 
                            }
                        }
                        int minCorrectAnswers = (int)nudMinCorrectAnswers.Value;
                        int maxCorrectAnswers = (int)nudMaxCorrectAnswers.Value;
                        if (correctAnswersCount < minCorrectAnswers || correctAnswersCount > maxCorrectAnswers)
                        {
                            MessageBox.Show($"Для типа вопроса 'Выбор нескольких' должно быть выбрано от {minCorrectAnswers} до {maxCorrectAnswers} правильных ответов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Сохраняем варианты ответов (INSERT или UPDATE)
                    foreach (DataRow row in _answers.Rows)
                    {
                        string answerText = row["text"] == DBNull.Value ? "" : row["text"].ToString();
                        bool isCorrect = row["is_correct"] == DBNull.Value ? false : Convert.ToBoolean(row["is_correct"]);

                        //  Получаем ID варианта ответа (если он есть)
                        int? answerId = row["id"] != DBNull.Value ? (int?)row["id"] : null;

                        if (answerId.HasValue)
                        {
                            //  Обновляем существующий вариант ответа (UPDATE)
                            string updateAnswerQuery = "UPDATE answers SET text = @answerText, is_correct = @isCorrect WHERE id = @answerId";
                            Dictionary<string, object> updateAnswerParameters = new Dictionary<string, object>();
                            updateAnswerParameters.Add("@answerText", answerText);
                            updateAnswerParameters.Add("@isCorrect", isCorrect);
                            updateAnswerParameters.Add("@answerId", answerId.Value);
                            dbHelper.ExecuteNonQuery(updateAnswerQuery, updateAnswerParameters);
                        }
                        else
                        {
                            // Добавляем новый вариант ответа (INSERT)
                            string insertAnswerQuery = "INSERT INTO answers (question_id, text, is_correct) VALUES (@questionId, @answerText, @isCorrect)";
                            Dictionary<string, object> insertAnswerParameters = new Dictionary<string, object>();
                            insertAnswerParameters.Add("@questionId", _questionId);
                            insertAnswerParameters.Add("@answerText", answerText);
                            insertAnswerParameters.Add("@isCorrect", isCorrect);
                            dbHelper.ExecuteNonQuery(insertAnswerQuery, insertAnswerParameters);
                        }
                    }
                }

                MessageBox.Show("Вопрос и варианты ответов успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении вопроса и вариантов ответов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvAnswers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Получаем ID ответа из DataGridView
            int answerId = Convert.ToInt32(dgvAnswers.Rows[e.RowIndex].Cells["id"].Value);

            // Получаем текст ответа из DataGridView
            string answerText = dgvAnswers.Rows[e.RowIndex].Cells["text"].Value.ToString();

            // Получаем флаг "правильный ответ" из DataGridView
            bool isCorrect;
            if (dgvAnswers.Rows[e.RowIndex].Cells["is_correct"].Value == DBNull.Value)
            {
                // Если значение NULL, устанавливаем значение по умолчанию (например, false)
                isCorrect = false;
            }
            else
            {
                // Если значение не NULL, преобразуем его в bool
                isCorrect = Convert.ToBoolean(dgvAnswers.Rows[e.RowIndex].Cells["is_correct"].Value);
            }

            // Обновляем запись в таблице answers в базе данных
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string updateAnswerQuery = "UPDATE answers SET text = @answerText, is_correct = @isCorrect WHERE id = @answerId";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@answerText", answerText);
                parameters.Add("@isCorrect", isCorrect);
                parameters.Add("@answerId", answerId);

                dbHelper.ExecuteNonQuery(updateAnswerQuery, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении варианта ответа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void cbQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cbQuestionType.SelectedItem.ToString();

            if (selectedType == "Текстовый")
            {
                // Показываем txtCorrectAnswer и скрываем dgvAnswers
                txtCorrectAnswer.Visible = true;
                dgvAnswers.Visible = false;

                // Очищаем dgvAnswers
                _answers.Rows.Clear();

                // Скрываем элементы управления для "Выбор нескольких"
                nudMinCorrectAnswers.Visible = false;
                nudMaxCorrectAnswers.Visible = false;
            }
            else if (selectedType == "Выбор нескольких")
            {
                // Скрываем txtCorrectAnswer и показываем dgvAnswers
                txtCorrectAnswer.Visible = false;
                dgvAnswers.Visible = true;

                // Очищаем txtCorrectAnswer
                txtCorrectAnswer.Text = "";

                // Показываем элементы управления для "Выбор нескольких"
                nudMinCorrectAnswers.Visible = true;
                nudMaxCorrectAnswers.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
            }
            else
            {
                // Скрываем txtCorrectAnswer и показываем dgvAnswers
                txtCorrectAnswer.Visible = false;
                dgvAnswers.Visible = true;

                // Очищаем txtCorrectAnswer
                txtCorrectAnswer.Text = "";

                // Скрываем элементы управления для "Выбор нескольких"
                nudMinCorrectAnswers.Visible = false;
                nudMaxCorrectAnswers.Visible = false;
            }
        }
    }
}