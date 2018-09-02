using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace SystemOfTrainingAndTesting.Model
{
    /// <summary>
    /// Класс реализующий обновление записей в базе данных
    /// </summary>
    static class Update
    {
        /// <summary>
        /// Метод для обновления данных пользователя в xml-файле
        /// </summary>
        /// <param name="loginString">Имя пользователя</param>
        /// <param name="passwordString">Пароль</param>
        /// <param name="nameString">Имя</param>
        /// <param name="lastNameString">Фамилия</param>
        /// <param name="patronimicString">Отчество</param>
        /// <param name="specialityString">Специальность</param>
        /// <param name="groupString">Группа</param>
        /// <param name="accessLvl">Уровень доступа</param>
        /// <returns>Статус добавления</returns>
        internal static int UpdateUser(string id, string loginString, string passwordString, string nameString,
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
                    if (xe.Attribute("name").Value.Equals(id))
                    {
                        xe.Attribute("name").Value = id;
                        xe.Element("login").Value = loginString;
                        xe.Element("password").Value = passwordString;
                        xe.Element("firstName").Value = nameString;
                        xe.Element("lastName").Value = lastNameString;
                        xe.Element("patronimic").Value = patronimicString;
                        xe.Element("speciality").Value = specialityString;
                        xe.Element("group").Value = groupString;
                        xe.Element("accessLvl").Value = accessLvl.ToString();
                        xDocument.Save("res/users.xml");
                        return 0;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Метод для обновления записи о статистике пользователя
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <param name="idTest">Идентификатор теста</param>
        /// <param name="numberOfCorrectAnswer">Количество верных ответов</param>
        /// <returns></returns>
        internal static int UpdateStatistics(int idUser, int idTest, int numberOfCorrectAnswer)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(string.Format("UPDATE statistics SET number_of_correct_answers = {2} WHERE id_user = {0} AND id_test = {1}", idUser, idTest, numberOfCorrectAnswer), npgsqlConnection);*/
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
        /// Метод для обновления записи о статистике пользователя
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <param name="idEducationTest">Идентификатор теста</param>
        /// <param name="numberOfCorrectAnswer">Количество верных ответов</param>
        /// <returns></returns>
        internal static int UpdateEducationStatistics(int idUser, int idEducationTest, int numberOfCorrectAnswer)
        {
            /*NpgsqlConnection npgsqlConnection = ConnectionToDb.Connection();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(string.Format("UPDATE education_statistics SET number_of_correct_answers = {2} WHERE id_user = {0} AND id_education_test = {1}", idUser, idEducationTest, numberOfCorrectAnswer), npgsqlConnection);*/
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

        
    }
}
