using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemOfTrainingAndTesting.Controller
{
    class Delete
    {
        /// <summary>
        /// Метод для удаления пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        internal static int DeleteUser(int id)
        {
            int status = Model.Delete.DeleteUser(id);
            return status;
        }
    }
}
