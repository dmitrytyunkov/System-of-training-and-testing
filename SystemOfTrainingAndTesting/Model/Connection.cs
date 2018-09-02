using System;
using System.Windows.Forms;
using System.Xml.Linq;
//using Npgsql;

namespace SystemOfTrainingAndTesting.Model
{
    /// <summary>
    /// Класс реализующий подключение и отключение от базы данных
    /// </summary>
    static class Connection
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private const string ConnectionString = "Server=192.168.1.3;Port=5432;User=postgres;Password=postgres;Database=data;";

        //static readonly NpgsqlConnection NpgsqlConnection = new NpgsqlConnection(ConnectionString);
        
        /// <summary>
        /// Метод для подключения к базе данных
        /// </summary>
        /// <returns></returns>
        internal static XDocument Connect(string key)
        {
            XDocument xDocument = null;
            switch (key)
            {
                case "users":
                    xDocument = XDocument.Load("res/users.xml");
                    break;
                case "tests":
                    xDocument = XDocument.Load("res/tests.xml");
                    break;
            }
            return xDocument;
        }
    }
}
