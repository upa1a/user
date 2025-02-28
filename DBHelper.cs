using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TestSystem
{
    public class DBHelper
    {
        private string connectionString = "server=localhost;port=3306;database=test_system;user=root;password=0000;";
        public MySqlConnection Connection { get; private set; } // Используем свойство для доступа к соединению

        public DBHelper()
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                try
                {
                    Connection.Open();
                }
                catch (MySqlException ex)
                {
                    // Обработка ошибок соединения
                    Console.WriteLine("Ошибка при подключении к базе данных: " + ex.Message);
                    throw; // Важно перебросить исключение, чтобы остановить выполнение программы
                }
            }
        }

        public void CloseConnection()
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                try
                {
                    Connection.Close();
                }
                catch (MySqlException ex)
                {
                    // Обработка ошибок закрытия соединения
                    Console.WriteLine("Ошибка при закрытии соединения: " + ex.Message);
                    // Можно решить не перебрасывать исключение, чтобы не сломать программу
                }
            }
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand command = new MySqlCommand(query, Connection); // Используем свойство Connection
            AddParameters(command, parameters); // Добавляем параметры в команду
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            try
            {
                OpenConnection();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine(ex.Message); // Логируем ошибку для отладки
                throw; // Важно перебросить исключение, чтобы остановить выполнение программы
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        public object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            object result = null;
            MySqlCommand command = new MySqlCommand(query, Connection); // Используем свойство Connection
            AddParameters(command, parameters); // Добавляем параметры в команду

            try
            {
                OpenConnection();
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine(ex.Message); // Логируем ошибку для отладки
                throw; // Важно перебросить исключение, чтобы остановить выполнение программы
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            int rowsAffected = 0;
            MySqlCommand command = new MySqlCommand(query, Connection); // Используем свойство Connection
            AddParameters(command, parameters); // Добавляем параметры в команду

            try
            {
                OpenConnection();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine(ex.Message); // Логируем ошибку для отладки
                throw; // Важно перебросить исключение, чтобы остановить выполнение программы
            }
            finally
            {
                CloseConnection();
            }
            return rowsAffected;
        }

        // Вспомогательный метод для добавления параметров в команду
        public void AddParameters(MySqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value); // Обрабатываем значения NULL
                }
            }
        }
    }
}