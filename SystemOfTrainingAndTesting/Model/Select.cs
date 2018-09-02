using System;
using System.Linq;
using System.Data.Common;
using System.Xml.Linq;
using System.Collections.Generic;
//using Npgsql;

namespace SystemOfTrainingAndTesting.Model
{
    /// <summary>
    /// Класс реализующий поиск объектов в базе данных
    /// </summary>
    static class Select
    {
        /// <summary>
        /// Метод для выбора пользователя в xml-файле
        /// </summary>
        /// <param name="loginString">Имя пользователя</param>
        /// <param name="passwordString">Пароль</param>
        /// <returns></returns>
        internal static Dictionary<string, string> SelectUser(string loginString, string passwordString)
        {
            XDocument xDocument = Connection.Connect("users");
            XElement xElementRoot = xDocument.Element("users");

            Dictionary<string, string> userDictionary = new Dictionary<string, string>();

            foreach (XElement userElement in xElementRoot.Elements("user").ToList())
            {
                if (userElement.Element("login").Value.ToLower().Equals(loginString.ToLower()) && 
                        userElement.Element("password").Value.Equals(passwordString))
                {
                    userDictionary.Add("id", userElement.Attribute("name").Value);

                    foreach (var temp in userElement.Elements().ToList())
                    {
                        userDictionary.Add(temp.Name.LocalName, temp.Value);
                    }
                }
            }
            return userDictionary;
        }


        /// <summary>
        /// Метод для выбора пользователей из xml-файла
        /// </summary>
        /// <returns></returns>
        internal static List<Dictionary<string, string>> SelectUsers()
        {
            XDocument xDocument = Connection.Connect("users");
            XElement xElementRoot = xDocument.Element("users");

            Dictionary<string, string> userDictionary = new Dictionary<string, string>();
            List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();

            foreach (XElement userElement in xElementRoot.Elements("user").ToList())
            {
                userDictionary = new Dictionary<string, string>();

                userDictionary.Add("id", userElement.Attribute("name").Value);

                foreach (var temp in userElement.Elements().ToList())
                {
                    userDictionary.Add(temp.Name.LocalName, temp.Value);
                }
                users.Add(userDictionary);
            }
            return users;
        }

        /// <summary>
        /// Метод для выбора тем из xml-файла
        /// </summary>
        /// <returns></returns>
        internal static List<Dictionary<string, Object>> SelectThemes()
        {
            XDocument xDocument = Connection.Connect("tests");

            XElement xElementRoot = xDocument.Element("themes");

            Dictionary<string, Object> themeDictionary = new Dictionary<string, Object>();
            List<Dictionary<string, Object>> themes = new List<Dictionary<string, Object>>();

            foreach (XElement userElement in xElementRoot.Elements("theme").ToList())
            {
                themeDictionary = new Dictionary<string, Object>();

                themeDictionary.Add("id", userElement.Attribute("name").Value);

                foreach (var temp in userElement.Elements().ToList())
                {
                    if (temp.Name.LocalName != "test")
                        themeDictionary.Add(temp.Name.LocalName, temp.Value);
                    else
                        Select.SelectTests(temp.ToString(), Convert.ToInt32(themeDictionary["id"]));
                }
                themes.Add(themeDictionary);
            }
            return themes;
        }

        /// <summary>
        /// Метод для выбора тестов в xml-файле
        /// </summary>
        /// <param name="xmlStr">xml-строка</param>
        /// <param name="themeId">Идентификатор темы</param>
        /// <returns></returns>
        internal static Dictionary<string, Object> SelectTests(string xmlStr, int themeId)
        {
            XDocument xDocument = XDocument.Parse(xmlStr);

            Dictionary<string, Object> testDictionary = new Dictionary<string, Object>();
            XElement xElementRoot = xDocument.Element("test");

            testDictionary.Add("id", xElementRoot.Attribute("name").Value);
            testDictionary.Add("themeId", themeId);

            foreach (var xElement in xElementRoot.Elements().ToList())
            {
                if (!xElement.Name.LocalName.Equals("question"))
                    testDictionary.Add(xElement.Name.LocalName, xElement.Value);
                else
                    Select.SelectQuestions(xElement.ToString(), Convert.ToInt32(testDictionary["id"]));
            }
            return testDictionary;
        }

        /// <summary>
        /// Метод для выбора вопросов в xml-файле
        /// </summary>
        /// <param name="xmlStr">xml-строка</param>
        /// <param name="testId">Идентификатор пользователя</param>
        /// <returns></returns>
        internal static Dictionary<string, Object> SelectQuestions(string xmlStr, int testId)
        {
            XDocument xDocument = XDocument.Parse(xmlStr);

            Dictionary<string, Object> dictionaryQuestion = new Dictionary<string, Object>();
            XElement xElementRoot = xDocument.Element("question");

            dictionaryQuestion.Add("id", xElementRoot.Attribute("name").Value);
            dictionaryQuestion.Add("testId", testId);

            foreach (var xElement in xElementRoot.Elements().ToList())
            {
                if (!(xElement.Name.LocalName.Equals("answer") || xElement.Name.LocalName.Equals("corrAnswer")))
                    dictionaryQuestion.Add(xElement.Name.LocalName, xElement.Value);
                else
                    Select.SelectAnswers(xElement.ToString(), Convert.ToInt32(dictionaryQuestion["id"]));
            }
            return dictionaryQuestion;
        }

        /// <summary>
        /// Метод для выбора ответов в xml-файле
        /// </summary>
        /// <param name="xmlStr">xml-строка</param>
        /// <param name="idQuestion">Идентификатор ответа</param>
        /// <returns></returns>
        internal static Dictionary<string, Object> SelectAnswers(string xmlStr, int idQuestion)
        {
            XElement xElement = XElement.Parse(xmlStr);

            Dictionary<string, Object> dictionaryAnswer = new Dictionary<string, Object>();

            dictionaryAnswer.Add("id", xElement.Attribute("name").Value);
            dictionaryAnswer.Add("questionId", idQuestion);

            if (xElement.Name.LocalName.Equals("answer"))
                dictionaryAnswer.Add("correction", false);
            else
                dictionaryAnswer.Add("correction", true);
            dictionaryAnswer.Add("text", xElement.Value);
            return dictionaryAnswer;
        }

        /// <summary>
        /// Метод для выбора статистики пользователя из базы данных
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <returns></returns>
        internal static DbDataReader SelectStatistics(int idUser)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id_test, number_of_correct_answers FROM statistics WHERE id_user = {0}", idUser),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;

        }

        /// <summary>
        /// Метод для выбора обучающих тестов из базы данных
        /// </summary>
        /// <returns></returns>
        internal static DbDataReader SelectEducationTests()
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id, id_theme, title, description, concat(title,'. ',description), number_of_questions FROM education_tests"),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;
        }

        /// <summary>
        /// Метод для выбора инструкций из базы данных
        /// </summary>
        /// <returns></returns>
        internal static DbDataReader SelectInstruction()
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id, title, description, id_theme, link FROM instructions"),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;

        }

        /// <summary>
        /// Метод для выбора обучающих тестов из базы данных
        /// </summary>
        /// <param name="idTheme">Идентификатор темы</param>
        /// <returns></returns>
        internal static DbDataReader SelectEducationTests(int idTheme)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id, title, description, concat(title,'. ',description), number_of_questions FROM education_tests WHERE id_theme={0}",idTheme),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;
        }

        /// <summary>
        /// Метод для выбора инструкций из базы данных
        /// </summary>
        /// <returns></returns>
        internal static DbDataReader SelectInstruction(int idTheme)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id, title, description, link, concat(title,'. ',description) FROM instructions WHERE id_theme={0}", idTheme),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;

        }

        /// <summary>
        /// Метод для выбора обучающих вопросов в базе данных
        /// </summary>
        /// <param name="idEducationTest">Идентификатор обучающего теста</param>
        /// <returns></returns>
        internal static DbDataReader SelectEducationQuestions(int idEducationTest)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id, question, description_correct_answer, type_answer FROM education_questions WHERE id_education_test = {0}", idEducationTest),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;
        }

        /// <summary>
        /// Метод для выбора обучающих ответов в базе данных
        /// </summary>
        /// <param name="idEducationQuestion">Идентификатор ответа</param>
        /// <returns></returns>
        internal static DbDataReader SelectEducationAnswers(int idEducationQuestion)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id, answer, correct_answer FROM education_answers WHERE id_education_question = {0}", idEducationQuestion),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;
        }

        /// <summary>
        /// Метод для выбора статистики обучения пользователя из базы данных
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <returns></returns>
        internal static DbDataReader SelectEducationStatistics(int idUser)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand =
                new NpgsqlCommand(
                    string.Format("SELECT id_education_test, number_of_correct_answers FROM education_statistics WHERE id_user = {0}", idUser),
                    npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            DbDataReader dbDataReader = npgsqlDataReader;*/
            DbDataReader dbDataReader = null;
            return dbDataReader;
        }
    }
}
