using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TestSystem
{
    public partial class TestForm : Form
    {
        private int _testId; // Добавляем поле для хранения ID теста
        private int _currentUserId;
        private int _currentQuestionIndex = 0; // Индекс текущего вопроса
        private int _currentTestId; // Добавляем объявление переменной _currentTestId
        private List<Question> _questions; // Список вопросов
        private TextBox _textBoxAnswer; // Поле для хранения ссылки на TextBox
        public TestForm(int testId, int userId)
        {
            InitializeComponent();

            _testId = testId;
            _currentUserId = userId;
            _currentTestId = testId;
            LoadQuestionsByTestId(_testId);
        }
        private void TestForm_Load(object sender, EventArgs e)
        {
            // Создаем кнопки навигации
            CreateNavigationButtons();
        }
        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            // 1. Получаем ответ пользователя
            GetUserAnswer(out int? optionId, out string answerText);

            // 2. Сохраняем ответ пользователя с помощью SaveUserAnswer
            int userId = _currentUserId;
            int testId = _currentTestId;
            int questionId = _questions[_currentQuestionIndex].Id; // Получаем ID текущего вопроса
            SaveUserAnswer(userId, testId, questionId, optionId, answerText);

            // 3. Переходим к следующему вопросу
            if (_currentQuestionIndex < _questions.Count - 1) // Проверяем, что не достигли конца списка
            {
                _currentQuestionIndex++;
                DisplayQuestion(_currentQuestionIndex); // Передаем индекс вопроса в DisplayQuestion
            }
            else
            {
                // Тест завершен
                Console.WriteLine($"Тест завершен: {_currentQuestionIndex}, Count: {_questions.Count}");
                MessageBox.Show("Тест завершен!");
            }
        }
        private void btnPreviousQuestion_Click(object sender, EventArgs e)
        {
            // Переход к предыдущему вопросу
            if (_currentQuestionIndex > 0)
            {
                _currentQuestionIndex--;
                DisplayQuestion(_currentQuestionIndex);
            }
            else
            {
                MessageBox.Show("Это первый вопрос.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSubmitTest_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer(); // Сохраняем последний ответ

            int correctAnswers = CalculateCorrectAnswers(); // Подсчитываем правильные ответы

            double percentage = (double)correctAnswers / _questions.Count * 100; // Вычисляем процент

            // Выводим результат
            MessageBox.Show($"Тест завершен!\nПравильных ответов: {correctAnswers} из {_questions.Count}\nПроцент: {percentage:F2}%",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //  Дополнительно:  Сохранить результат в базу данных
            SaveResultToDatabase(_currentUserId, _testId, correctAnswers, _questions.Count, percentage);

            //  Закрываем форму (или выполняем другие действия после завершения теста)
            this.Close();
        }

        private Button CreateButton(string text, string name, EventHandler clickHandler, Point location)
        {
            Button button = new Button();
            button.Text = text;
            button.Name = name;
            button.Click += clickHandler;
            button.Location = location;
            button.AutoSize = true;
            return button;
        }
        private void CreateNavigationButtons()
        {
            int buttonY = this.ClientSize.Height - 50; // Размещаем кнопки внизу формы

            // Кнопка "Предыдущий вопрос"
            Button btnPreviousQuestion = CreateButton(
                "Предыдущий вопрос",
                "btnPreviousQuestion",
                btnPreviousQuestion_Click,
                new Point(20, buttonY) // Примерное расположение внизу
            );
            this.Controls.Add(btnPreviousQuestion);

            // Кнопка "Следующий вопрос"
            Button btnNextQuestion = CreateButton(
                "Следующий вопрос",
                "btnNextQuestion",
                btnNextQuestion_Click,
                new Point(btnPreviousQuestion.Right + 10, buttonY) // Расположение справа от предыдущей кнопки
            );
            this.Controls.Add(btnNextQuestion);

            // Кнопка "Завершить тест"
            Button btnSubmitTest = CreateButton(
                "Завершить тест",
                "btnSubmitTest",
                btnSubmitTest_Click,
                new Point(btnNextQuestion.Right + 10, buttonY) // Расположение справа от следующей кнопки
            );
            this.Controls.Add(btnSubmitTest);

            this.Controls.Add(btnPreviousQuestion);
            this.Controls.Add(btnNextQuestion);
            this.Controls.Add(btnSubmitTest);

            this.Controls.Add(btnPreviousQuestion);
            btnPreviousQuestion.BringToFront(); // Выводим кнопку на передний план
            this.Controls.Add(btnNextQuestion);
            btnNextQuestion.BringToFront();   // Выводим кнопку на передний план
            this.Controls.Add(btnSubmitTest);
            btnSubmitTest.BringToFront();  // Выводим кнопку на передний план
        }
        private void ClearFormControls()
        {
            // Создаем список элементов управления для удаления
            List<Control> controlsToRemove = new List<Control>();

            // Перебираем все элементы управления на panel1
            foreach (Control control in panel1.Controls)
            {
                // Добавляем элемент управления в список для удаления
                controlsToRemove.Add(control);
            }

            // Перебираем список элементов управления для удаления и удаляем их
            foreach (Control control in controlsToRemove)
            {
                // Удаляем элемент управления из коллекции Controls
                panel1.Controls.Remove(control);

                // Освобождаем ресурсы
                control.Dispose();
            }
        }
        private void DisplayQuestion(int questionIndex)
        {
            // Проверяем, что индекс находится в допустимых пределах
            if (questionIndex < 0 || questionIndex >= _questions.Count)
            {
                MessageBox.Show("Ошибка: Недопустимый индекс вопроса.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Очищаем форму от старых элементов управления
            ClearFormControls();

            // Получаем вопрос из списка
            Question question = _questions[questionIndex];

            // Отображаем вопрос и варианты ответов на форме
            int y = 50; // Начальная позиция по вертикали
            Label questionLabel = new Label();
            questionLabel.Text = question.Text;
            questionLabel.Location = new Point(20, y);
            questionLabel.AutoSize = true;
            panel1.Controls.Add(questionLabel); // Добавляем Label на panel1
            y += 30; // Увеличиваем позицию для следующих элементов

            // Удаляем TextBox, если он существует
            if (_textBoxAnswer != null)
            {
                panel1.Controls.Remove(_textBoxAnswer);
                _textBoxAnswer.Dispose(); // Освобождаем ресурсы
                _textBoxAnswer = null;
            }

            switch (question.Type)
            {
                case "Выбор одного":
                    // Отображаем RadioButton для каждого варианта ответа
                    foreach (Answer answer in question.Answers)
                    {
                        RadioButton answerRadioButton = new RadioButton();
                        answerRadioButton.Text = answer.Text; // Устанавливаем текст RadioButton
                        answerRadioButton.Location = new Point(40, y); // Расположение для RadioButton
                        answerRadioButton.Tag = answer.Id; // Сохраняем ID варианта ответа в Tag
                        panel1.Controls.Add(answerRadioButton); // Добавляем RadioButton на panel1
                        y += 25;
                    }
                    break;

                case "Выбор нескольких":
                    // Отображаем CheckBox для каждого варианта ответа
                    foreach (Answer answer in question.Answers)
                    {
                        CheckBox answerCheckBox = new CheckBox();
                        answerCheckBox.Text = answer.Text; // Устанавливаем текст CheckBox
                        answerCheckBox.Location = new Point(40, y); // Расположение для CheckBox
                        answerCheckBox.Tag = answer.Id; // Сохраняем ID варианта ответа в Tag
                        panel1.Controls.Add(answerCheckBox); // Добавляем CheckBox на panel1
                        y += 25;
                    }
                    break;

                case "Текстовый":
                    // Отображаем TextBox для ввода текстового ответа
                    _textBoxAnswer = new TextBox();
                    _textBoxAnswer.Location = new Point(40, y);  // Расположение для TextBox
                    _textBoxAnswer.Width = 200;
                    panel1.Controls.Add(_textBoxAnswer);
                    y += 30; // Перемещаем y ниже TextBox
                    break;
            }
        }
        private bool OptionExists(int optionId)
        {
            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
            string query = "SELECT COUNT(*) FROM options WHERE id = @OptionId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OptionId", optionId);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Возвращает true, если вариант ответа существует
                }
            }
        }
        private bool UserExists(int userId)
        {
            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
            string query = "SELECT COUNT(*) FROM users WHERE id = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Возвращает true, если пользователь существует
                }
            }
        }
        private void GetUserAnswer(out int? optionId, out string answerText)
        {
            optionId = null;
            answerText = null;

            Question currentQuestion = _questions[_currentQuestionIndex];

            switch (currentQuestion.Type)
            {
                case "Выбор одного":
                    foreach (Control control in panel1.Controls)
                    {
                        if (control is RadioButton radioButton && radioButton.Checked)
                        {
                            optionId = (int)radioButton.Tag;
                            break;
                        }
                    }
                    break;

                case "Выбор нескольких":
                    answerText = "";
                    foreach (Control control in panel1.Controls)
                    {
                        if (control is CheckBox checkBox)
                        {
                            if (checkBox.Checked)
                            {
                                answerText += checkBox.Tag + ","; // Append selected option IDs
                            }
                        }
                    }
                    if (answerText.Length > 0)
                    {
                        answerText = answerText.TrimEnd(','); // Remove trailing comma
                    }
                    break;

                case "Текстовый":
                    if (_textBoxAnswer != null)
                    {
                        answerText = _textBoxAnswer.Text;
                    }
                    break;
            }
        }
        private void SaveResultToDatabase(int userId, int testId, int correctAnswers, int totalQuestions, double percentage)
        {
            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
            string query = "INSERT INTO test_results (user_id, test_id, score, date) " +
                           "VALUES (@UserId, @TestId, @Score, @Date) " +
                           "ON DUPLICATE KEY UPDATE score = @Score, date = @Date";

            // Вычисляем score (можно использовать разные формулы)
            int score = correctAnswers; // Простейший вариант: score = количество правильных ответов
                                        // int score = (int)(percentage * 100); // score = процент правильных ответов

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@TestId", testId);
                    command.Parameters.AddWithValue("@Score", score);
                    command.Parameters.AddWithValue("@Date", DateTime.Now); // Сохраняем текущую дату и время

                    command.ExecuteNonQuery();
                }
            }
        }
        private void LoadQuestionsByTestId(int testId)
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string query = @"
            SELECT
                q.id AS question_id,
                q.question_text,
                q.question_type,
                o.id AS answer_id,
                o.option_text AS answer_text,
                o.is_correct
            FROM questions q
            LEFT JOIN options o ON q.id = o.question_id
            WHERE q.test_id = " + testId;

                DataTable questionsData = dbHelper.ExecuteQuery(query);

                _questions = new List<Question>();
                Dictionary<int, Question> questionDictionary = new Dictionary<int, Question>();

                foreach (DataRow row in questionsData.Rows)
                {
                    int questionId = Convert.ToInt32(row["question_id"]);
                    string questionText = row["question_text"].ToString();
                    string questionType = row["question_type"].ToString();

                    // Проверяем на DBNull перед преобразованием
                    int? answerId = row.Table.Columns.Contains("answer_id") && row["answer_id"] != DBNull.Value ? Convert.ToInt32(row["answer_id"]) : (int?)null;
                    //string answerText = row.Table.Columns.Contains("option_text") && row["option_text"] != DBNull.Value ? row["option_text"].ToString() : "";
                    string answerText = answerId.HasValue ? GetOptionTextFromDatabase(answerId.Value) : "";
                    bool? isCorrect = row.Table.Columns.Contains("is_correct") && row["is_correct"] != DBNull.Value ? Convert.ToBoolean(row["is_correct"]) : (bool?)null;

                    if (!questionDictionary.ContainsKey(questionId))
                    {
                        Question question = new Question
                        {
                            Id = questionId,
                            Text = questionText,
                            Type = questionType,
                            Answers = new List<Answer>()
                        };
                        questionDictionary.Add(questionId, question);
                        _questions.Add(question);
                    }

                    Question currentQuestion = questionDictionary[questionId];

                    // Создаем новый объект Answer только если есть данные об ответе
                    if (answerId.HasValue)
                    {
                        Answer answer = new Answer
                        {
                            Id = answerId.Value,
                            Text = answerText, // Присваиваем текст варианта ответа свойству Text
                            IsCorrect = isCorrect.HasValue ? isCorrect.Value : false // Если isCorrect == null, считаем, что ответ неправильный
                        };
                        currentQuestion.Answers.Add(answer);
                    }
                }

                if (_questions.Count == 0)
                {
                    MessageBox.Show("В этом тесте нет вопросов.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DisplayQuestion(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке вопросов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private string GetOptionTextFromDatabase(int optionId)
        {
            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
            string query = "SELECT option_text FROM options WHERE id = @OptionId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OptionId", optionId);
                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
        }
        private void SaveUserAnswer(int userId, int testId, int questionId, int? optionId, string answerText)
        {
            DBHelper dbHelper = new DBHelper();
            try
            {
                dbHelper.OpenConnection();

                string query = @"
            INSERT INTO user_answers (user_id, test_id, question_id, option_id, answer_text)
            VALUES (@UserId, @TestId, @QuestionId, @OptionId, @AnswerText)
            ON DUPLICATE KEY UPDATE
            option_id = @OptionId, answer_text = @AnswerText";

                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@UserId", userId },
            { "@TestId", testId },
            { "@QuestionId", questionId },
            { "@OptionId", optionId.HasValue ? (object)optionId.Value : DBNull.Value }, // Обрабатываем Nullable<int>
            { "@AnswerText", string.IsNullOrEmpty(answerText) ? DBNull.Value : (object)answerText } // Обрабатываем NULL
        };

                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении ответа: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
        private void SaveCurrentAnswer()
        {
            if (_questions == null || _questions.Count == 0)
            {
                return;
            }

            // Проверяем существование пользователя
            if (!UserExists(_currentUserId))
            {
                MessageBox.Show($"Пользователь с ID {_currentUserId} не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int questionId = _questions[_currentQuestionIndex].Id;
            int? optionId = null;
            string answerText = null;
            GetUserAnswer(out optionId, out answerText);

            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO user_answers (user_id, test_id, question_id, option_id, answer_text) " +
                               "VALUES (@UserId, @TestId, @QuestionId, @OptionId, @AnswerText) " +
                               "ON DUPLICATE KEY UPDATE " +
                               "option_id = CASE WHEN @OptionId IS NULL OR EXISTS (SELECT 1 FROM options WHERE id = @OptionId) THEN @OptionId ELSE NULL END, " +  // Проверяем optionId перед обновлением
                               "answer_text = @AnswerText";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", _currentUserId);
                    command.Parameters.AddWithValue("@TestId", _testId);
                    command.Parameters.AddWithValue("@QuestionId", questionId);

                    // Обрабатываем NULL значения для optionId
                    if (optionId.HasValue)
                    {
                        command.Parameters.AddWithValue("@OptionId", optionId.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@OptionId", DBNull.Value);
                    }

                    // Обрабатываем NULL значения для answerText
                    command.Parameters.AddWithValue("@AnswerText", string.IsNullOrEmpty(answerText) ? (object)DBNull.Value : answerText);

                    command.ExecuteNonQuery();
                }
                // Проверяем существование option_id, если он есть
                if (optionId.HasValue && !OptionExists(optionId.Value))
                {
                    MessageBox.Show($"Вариант ответа с ID {optionId.Value} не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Прерываем сохранение, если вариант ответа не существует
                }
            }
        }
        private int CalculateCorrectAnswers()
        {
            int correctAnswers = 0;

            // 1. Получаем все ответы пользователя для данного теста
            string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
            string query = @"
        SELECT ua.question_id, ua.option_id, ua.answer_text, q.question_type
        FROM user_answers ua
        INNER JOIN questions q ON ua.question_id = q.id
        WHERE ua.user_id = @UserId AND ua.test_id = @TestId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", _currentUserId);
                    command.Parameters.AddWithValue("@TestId", _testId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int questionId = Convert.ToInt32(reader["question_id"]);
                            string questionType = reader["question_type"].ToString();
                            int? userAnswerOptionId = reader["option_id"] != DBNull.Value ? Convert.ToInt32(reader["option_id"]) : (int?)null;
                            string userAnswerText = reader["answer_text"] != DBNull.Value ? reader["answer_text"].ToString() : null;

                            // 2. Получаем правильные ответы для данного вопроса
                            string correctAnswerQuery = @"
                        SELECT id, is_correct, option_text
                        FROM options
                        WHERE question_id = @QuestionId";

                            // Создаем новое соединение для каждого запроса
                            using (MySqlConnection correctAnswerConnection = new MySqlConnection(connectionString))
                            {
                                correctAnswerConnection.Open();
                                using (MySqlCommand correctAnswerCommand = new MySqlCommand(correctAnswerQuery, correctAnswerConnection))
                                {
                                    correctAnswerCommand.Parameters.AddWithValue("@QuestionId", questionId);
                                    using (MySqlDataReader correctAnswerReader = correctAnswerCommand.ExecuteReader())
                                    {
                                        if (questionType == "Выбор одного")
                                        {
                                            // Для вопросов с выбором одного варианта
                                            while (correctAnswerReader.Read())
                                            {
                                                bool isCorrect = Convert.ToBoolean(correctAnswerReader["is_correct"]);
                                                int optionId = Convert.ToInt32(correctAnswerReader["id"]);

                                                if (isCorrect && userAnswerOptionId.HasValue && userAnswerOptionId.Value == optionId)
                                                {
                                                    correctAnswers++;
                                                    break; // Только один правильный ответ
                                                }
                                            }
                                        }
                                        else if (questionType == "Выбор нескольких")
                                        {
                                            // Для вопросов с выбором нескольких вариантов
                                            List<int> correctOptionIds = new List<int>();
                                            List<int> userOptionIds = new List<int>();

                                            // Получаем все правильные варианты ответа из базы данных
                                            while (correctAnswerReader.Read())
                                            {
                                                bool isCorrect = Convert.ToBoolean(correctAnswerReader["is_correct"]);
                                                int optionId = Convert.ToInt32(correctAnswerReader["id"]);

                                                if (isCorrect)
                                                {
                                                    correctOptionIds.Add(optionId);
                                                }
                                            }

                                            // Получаем выбранные пользователем варианты ответа
                                            if (!string.IsNullOrEmpty(userAnswerText))
                                            {
                                                string[] userOptionIdStrings = userAnswerText.Split(',');
                                                foreach (string userOptionIdString in userOptionIdStrings)
                                                {
                                                    if (int.TryParse(userOptionIdString, out int userOptionId))
                                                    {
                                                        userOptionIds.Add(userOptionId);
                                                    }
                                                }
                                            }

                                            // Сравниваем выбранные варианты ответа с правильными вариантами ответа
                                            if (correctOptionIds.Count == userOptionIds.Count && correctOptionIds.All(userOptionIds.Contains))
                                            {
                                                correctAnswers++;
                                            }
                                        }
                                        else if (questionType == "Текстовый")
                                        {
                                            // Для текстовых вопросов
                                            string correctOptionText = null;
                                            while (correctAnswerReader.Read())
                                            {
                                                bool isCorrect = Convert.ToBoolean(correctAnswerReader["is_correct"]);
                                                if (isCorrect)
                                                {
                                                    correctOptionText = correctAnswerReader["option_text"].ToString();
                                                    break; // Только один правильный ответ
                                                }
                                            }

                                            // Сравниваем текст, введенный пользователем, с правильным ответом
                                            if (!string.IsNullOrEmpty(userAnswerText) && !string.IsNullOrEmpty(correctOptionText) && userAnswerText.Trim().ToLower() == correctOptionText.Trim().ToLower())
                                            {
                                                correctAnswers++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return correctAnswers;
        }
        public enum QuestionType
        {
            Text,           // Текстовый вопрос
            SingleChoice,   // Один вариант ответа (радиокнопки)
            MultipleChoice  // Несколько вариантов ответа (чекбоксы)
        }
        public class Question
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public QuestionType QuestionType { get; set; } // Добавляем свойство QuestionType
            public string Type { get; set; } // Добавляем тип вопроса
            public List<Answer> Answers { get; set; }
            public string CorrectAnswerText { get; set; } // Для текстовых вопросов
        }
        public class Answer
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}
