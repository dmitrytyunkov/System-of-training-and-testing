using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemOfTrainingAndTesting.Controller
{
    class Update
    {
        /// <summary>
        /// Метод для обновления данных пользователя
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
            int status = Model.Update.UpdateUser(id, loginString, passwordString, nameString,
                                                        lastNameString, patronimicString, specialityString,
                                                        groupString, accessLvl);
            return status;
        }
    }
}
