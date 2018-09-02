using System;
using System.Data.Common;
using System.Collections.Generic;

namespace SystemOfTrainingAndTesting.Controller
{
    /// <summary>
    /// Класс реализующий выбор и сохранение объектов
    /// </summary>
    static class Select
    {
        /// <summary>
        /// Метод для выбора и сохраненния данных текущего пользователя
        /// </summary>
        /// <param name="loginString">Имя пользователя</param>
        /// <param name="passwordString">Пароль</param>
        /// <returns></returns>
        internal static bool SelectUser(string loginString, string passwordString)
        {
            if (!String.IsNullOrEmpty(loginString) && !String.IsNullOrEmpty(passwordString))
            {
                var dbDataReader = Model.Select.SelectUser(loginString, passwordString);
                if (dbDataReader.Count != 0)
                {
                    {
                        #region Сохранение информации о пользователя
                        ObjectModel.User.Id = Convert.ToInt32(dbDataReader["id"]);
                        ObjectModel.User.Login = dbDataReader["login"];
                        ObjectModel.User.LastName = dbDataReader["lastName"];
                        ObjectModel.User.Name = dbDataReader["firstName"];
                        ObjectModel.User.Patronimic = dbDataReader["patronimic"];
                        ObjectModel.User.Speciality = dbDataReader["speciality"];
                        //Info.User.Birthday = Convert.ToDateTime(dbDataReader["birthday"]);
                        ObjectModel.User.AccessLevel = Convert.ToInt32(dbDataReader["accessLvl"]);
                        ObjectModel.User.Group = dbDataReader["group"];
                        #endregion
                    }
                    return true;
                }
            }
            ObjectModel.User.AccessLevel = 3;
            return false;
        }

        /// <summary>
        /// Метод для выбора пользователей
        /// </summary>
        internal static void SelectUsers()
        {
            #region Удаление старой информации о пользователях
            //Info.Users.Birthday.Clear();
            ObjectModel.Users.Id.Clear();
            ObjectModel.Users.LastName.Clear();
            ObjectModel.Users.Login.Clear();
            ObjectModel.Users.Patronimic.Clear();
            ObjectModel.Users.Name.Clear();
            ObjectModel.Users.Speciality.Clear();
            #endregion
            List<Dictionary<string, string>> dbDataReader = Model.Select.SelectUsers();
            if (dbDataReader.Count != 0)
            {
                foreach (var dbDataRecord in dbDataReader)
                {
                    #region Сохранение информации о пользователях
                    ObjectModel.Users.Id.Add(Convert.ToInt32(dbDataRecord["id"]));
                    ObjectModel.Users.LastName.Add(dbDataRecord["lastName"]);
                    ObjectModel.Users.Login.Add(dbDataRecord["login"]);
                    ObjectModel.Users.Patronimic.Add(dbDataRecord["patronimic"]);
                    ObjectModel.Users.Name.Add(dbDataRecord["firstName"]);
                    ObjectModel.Users.Speciality.Add(dbDataRecord["speciality"]);
                    ObjectModel.Users.Group.Add(dbDataRecord["group"]);
                    ObjectModel.Users.Password.Add(dbDataRecord["password"]);
                    ObjectModel.Users.AccessLvl.Add(Convert.ToInt32(dbDataRecord["accessLvl"]));
                    #endregion
                }
            }
        }

        /// <summary>
        /// Метод для выбора тем
        /// </summary>
        internal static void SelectThemes()
        {
            #region Удаление старой информации о темах
            ObjectModel.Themes.Id.Clear();
            ObjectModel.Themes.Title.Clear();
            ObjectModel.Themes.Description.Clear();
            //Info.Themes.TitleAndDescription.Clear();
            #endregion
            #region Удаление старой информации о тестах
            ObjectModel.Tests.Id.Clear();
            //Info.Tests.NumberOfQuestion.Clear();
            //Info.Tests.TitleAndDescription.Clear();
            ObjectModel.Tests.Description.Clear();
            ObjectModel.Tests.Title.Clear();
            ObjectModel.Tests.IdTheme.Clear();
            #endregion
            #region Удаление старой информации о вопросах
            ObjectModel.Questions.Id.Clear();
            ObjectModel.Questions.Question.Clear();
            ObjectModel.Questions.TypeAnswer.Clear();
            ObjectModel.Questions.Description.Clear();
            ObjectModel.Questions.IdTest.Clear();
            #endregion
            #region Удаление старой информации об ответах
            ObjectModel.Answers.Id.Clear();
            ObjectModel.Answers.Answer.Clear();
            ObjectModel.Answers.CorrectAnswer.Clear();
            ObjectModel.Answers.IdQuestion.Clear();
            #endregion
            List<Dictionary<string, Object>> dbDataReader = Model.Select.SelectThemes();
            if (dbDataReader.Count != 0)
            {
                foreach (var dbDataRecord in dbDataReader)
                {
                    #region Сохранение информации о темах
                    ObjectModel.Themes.Id.Add(Convert.ToInt32(dbDataRecord["id"]));
                    ObjectModel.Themes.Title.Add(dbDataRecord["title"].ToString());
                    ObjectModel.Themes.Description.Add(dbDataRecord["description"].ToString());
                    //Info.Themes.TitleAndDescription.Add(dbDataRecord["concat"].ToString());
                    #endregion
                }
            }
        }

        /// <summary>
        /// Метод для выбора и сохраненния данных о тестах
        /// </summary>
        internal static void SelectTests(string xmlStr, int themeId)
        {
            Dictionary<string, Object> dbDataReader = Model.Select.SelectTests(xmlStr, themeId);
            if (dbDataReader.Count != 0)
            {
                #region Сохранение информации о тестах
                ObjectModel.Tests.Id.Add(Convert.ToInt32(dbDataReader["id"]));
                //Info.Tests.NumberOfQuestion.Add(Convert.ToInt32(dbDataReader["number_of_questions"]));
                //Info.Tests.TitleAndDescription.Add(dbDataReader["concat"].ToString());
                ObjectModel.Tests.Title.Add(dbDataReader["title"].ToString());
                ObjectModel.Tests.Description.Add(dbDataReader["description"].ToString());
                ObjectModel.Tests.IdTheme.Add(Convert.ToInt32(dbDataReader["themeId"]));
                ObjectModel.Tests.Type.Add(Convert.ToInt32(dbDataReader["type"]));
                #endregion
            }
        }

        /// <summary>
        /// Метод для выбора и сохраненния данных о вопросах
        /// </summary>
        /// <param name="idTest">Идентификатор теста</param>
        internal static void SelectQuestions(string xmlStr, int idTest)
        {
            Dictionary<string, Object> dbDataReader = Model.Select.SelectQuestions(xmlStr, idTest);
            if (dbDataReader.Count != 0)
            {
                #region Сохранение информации о вопросах
                ObjectModel.Questions.Id.Add(Convert.ToInt32(dbDataReader["id"]));
                ObjectModel.Questions.Question.Add(dbDataReader["text"].ToString());
                ObjectModel.Questions.TypeAnswer.Add(Convert.ToInt32(dbDataReader["type"]));
                ObjectModel.Questions.IdTest.Add(Convert.ToInt32(dbDataReader["testId"]));
                ObjectModel.Questions.Description.Add(dbDataReader["description"].ToString());
                #endregion
            }
        }

        /// <summary>
        /// Метод для выбора и сохраненния данных об ответах
        /// </summary>
        /// <param name="idQuestion">Идентификатор вопроса</param>
        internal static void SelectAnswers(string xmlStr, int idQuestion)
        {
            Dictionary<string, Object> dbDataReader = Model.Select.SelectAnswers(xmlStr, idQuestion);
            if (dbDataReader.Count != 0)
            {
                #region Сохранение информации об ответах
                ObjectModel.Answers.Id.Add(Convert.ToInt32(dbDataReader["id"]));
                ObjectModel.Answers.Answer.Add(dbDataReader["text"].ToString());
                ObjectModel.Answers.CorrectAnswer.Add(Convert.ToBoolean(dbDataReader["correction"]));
                ObjectModel.Answers.IdQuestion.Add(Convert.ToInt32(dbDataReader["questionId"]));
                #endregion
            }
        }

        /// <summary>
        /// Метод для выбора статистики пользователя
        /// </summary>
        internal static void SelectStatistics()
        {
            #region Удаление старой информации о статистике
            ObjectModel.Statistics.IdTest.Clear();
            ObjectModel.Statistics.NumberOfCorrectAnswer.Clear();
            #endregion
            DbDataReader dbDataReader = Model.Select.SelectStatistics(ObjectModel.User.Id);
            if (dbDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in dbDataReader)
                {
                    #region Сохранение информации о статистике
                    ObjectModel.Statistics.IdTest.Add(Convert.ToInt32(dbDataRecord["id_test"]));
                    ObjectModel.Statistics.NumberOfCorrectAnswer.Add(Convert.ToInt32(dbDataRecord["number_of_correct_answers"]));
                    #endregion
                }
            }
        }
    }
}
