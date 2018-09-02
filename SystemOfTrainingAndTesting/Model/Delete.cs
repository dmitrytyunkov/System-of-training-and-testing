using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SystemOfTrainingAndTesting.Model
{
    class Delete
    {
        /// <summary>
        /// Метод для удаления пользователя в xml-файле
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        internal static int DeleteUser(int id)
        {
            XDocument xDocument = XDocument.Load("res/users.xml");
            XElement xElementRoot = xDocument.Element("users");

            foreach (XElement xe in xElementRoot.Elements("user").ToList())
            {
                if (xe.Attribute("name").Value.Equals(id.ToString()))
                {
                    xe.Remove();
                    xDocument.Save("res/users.xml");
                    return 0;
                }
            }
            return -1;
        }
    }
}
