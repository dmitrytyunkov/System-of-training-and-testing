using System.Windows.Forms;

namespace SystemOfTrainingAndTesting.Controller
{
    /// <summary>
    /// Класс реализующий сохранение записей
    /// </summary>
    static class Insert
    {
        /// <summary>
        /// Метод для сохранения статистики пользователя
        /// </summary>
        /// <param name="idTest">Идентификатор теста</param>
        /// <param name="numberOfCorrectAnswer">Количество верных ответов</param>
        internal static void InsertStatistic(int idTest, int numberOfCorrectAnswer)
        {
            int insertCount = Model.Insert.InsertStatistic(ObjectModel.User.Id, idTest, numberOfCorrectAnswer);
            if (insertCount == 1)
            {
                return;
            }
            int updateCount = Model.Update.UpdateStatistics(ObjectModel.User.Id, idTest, numberOfCorrectAnswer);
            if (updateCount == 1)
            {
                return;
            }
            MessageBox.Show(@"Не удалось сохранить результат", @"Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Метод для сохранения статистики обучения пользователя
        /// </summary>
        /// <param name="idEducationTest">Идентификатор обучающего теста</param>
        /// <param name="numberOfCorrectAnswer">Количество верных ответов</param>
        internal static void InsertEducationStatistic(int idEducationTest, int numberOfCorrectAnswer)
        {
            int insertCount = Model.Insert.InsertEducationStatistic(ObjectModel.User.Id, idEducationTest, numberOfCorrectAnswer);
            if (insertCount == 1)
            {
                return;
            }
            int updateCount = Model.Update.UpdateEducationStatistics(ObjectModel.User.Id, idEducationTest, numberOfCorrectAnswer);
            if (updateCount == 1)
            {
                return;
            }
            MessageBox.Show(@"Не удалось сохранить результат", @"Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Метод для сохратения нового пользователя
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
        internal static int InsertUser(string loginString, string passwordString, string nameString,
                                            string lastNameString, string patronimicString,
                                            string specialityString, string groupString, int accessLvl)
        {
            int status = Model.Insert.InsertUser(loginString, passwordString, nameString,
                                                        lastNameString, patronimicString, specialityString,
                                                        groupString, accessLvl);
            return status;
        }
    }
}
