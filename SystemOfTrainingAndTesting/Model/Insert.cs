using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
//using Npgsql;

namespace SystemOfTrainingAndTesting.Model
{
    /// <summary>
    /// Класс реализующий сохранение записей в базе данных
    /// </summary>
    static class Insert
    {
        /// <summary>
        /// Метод для сохранения статистики пользователя в базе данных
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <param name="idTest">Идентификатор теста</param>
        /// <param name="numberOfCorrectAnswer">Количество верных ответов</param>
        /// <returns></returns>
        internal static int InsertStatistic(int idUser, int idTest, int numberOfCorrectAnswer)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(string.Format("INSERT INTO statistics(id_user, id_test, number_of_correct_answers) VALUES ({0}, {1}, {2})", idUser, idTest, numberOfCorrectAnswer), npgsqlConnection);*/
            int count;
            try
            {
                count = 0;//npgsqlCommand.ExecuteNonQuery();
            }
            catch (NotSupportedException)
            {
                count = 0;
            }
            return count;
        }

        /// <summary>
        /// Метод для сохранения статистики пользователя в базе данных
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <param name="idEducationTest">Идентификатор теста</param>
        /// <param name="numberOfCorrectAnswer">Количество верных ответов</param>
        /// <returns></returns>
        internal static int InsertEducationStatistic(int idUser, int idEducationTest, int numberOfCorrectAnswer)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(string.Format("INSERT INTO education_statistics(id_user, id_education_test, number_of_correct_answers) VALUES ({0}, {1}, {2})", idUser, idEducationTest, numberOfCorrectAnswer), npgsqlConnection);*/
            int count;
            try
            {
                count = 0;//npgsqlCommand.ExecuteNonQuery();
            }
            catch (NotSupportedException)
            {
                count = 0;
            }
            return count;
        }

        /// <summary>
        /// Метод для сохранения нового пользователя в xml-файле
        /// </summary>
        /// <param name="loginString">Имя пользователя</param>
        /// <param name="passwordString">Пароль</param>
        /// <param name="nameString">Имя</param>
        /// <param name="lastNameString">Фамилия</param>
        /// <param name="patronimicString">Отчество</param>
        /// <param name="specialityString">Специальность</param>
        /// <param name="groupString">Группа</param>
        /// <param name="accessLvl">Уровень доступа</param>
        /// <returns>Статус добавления в xml-файл</returns>
        internal static int InsertUser(string loginString, string passwordString, string nameString,
                                            string lastNameString, string patronimicString,
                                            string specialityString, string groupString, int accessLvl)
        {
            if (!String.IsNullOrEmpty(loginString) && !String.IsNullOrEmpty(passwordString) &&
                    !String.IsNullOrEmpty(nameString) && !String.IsNullOrEmpty(lastNameString) &&
                    !String.IsNullOrEmpty(specialityString) && !String.IsNullOrEmpty(groupString))
            {
                XDocument xDocument = XDocument.Load("res/users.xml");
                XElement xElementRoot = xDocument.Element("users");

                foreach (XElement xe in xElementRoot.Elements("user").ToList())
                {
                    if (xe.Element("login").Value.ToLower().Equals(loginString.ToLower()))
                    {
                        return -1;
                    }
                }

                int lastId = Convert.ToInt32(xElementRoot.Elements("user").ToList()[xElementRoot.Elements("user").ToList().Count-1].Attribute("name").Value);

                xElementRoot.Add(new XElement("user",
                                                    new XAttribute("name", ++lastId),
                                                    new XElement("login", loginString),
                                                    new XElement("password", passwordString),
                                                    new XElement("firstName", nameString),
                                                    new XElement("lastName", lastNameString),
                                                    new XElement("patronimic", patronimicString),
                                                    new XElement("speciality", specialityString),
                                                    new XElement("group", groupString),
                                                    new XElement("accessLvl", accessLvl)));
                xDocument.Save("res/users.xml");
                return 0;
            }
            return 1;
        }
    }
}
