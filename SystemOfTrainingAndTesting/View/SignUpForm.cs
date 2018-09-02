using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SystemOfTrainingAndTesting.View
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string loginString = textBoxLogin.Text.Trim();
            string passwordString = textBoxPassword.Text.Trim();
            string nameString = textBoxName.Text.Trim();
            string lastNameString = textBoxLastName.Text.Trim();
            string patronimicString = textBoxPatronimic.Text.Trim();
            string specialityString = textBoxSpeciality.Text.Trim();
            string groupString = textBoxGroup.Text.Trim();
            int accessLvl = checkBoxAccessLvl.Checked ? 0 : 1;

            if (labelTypeForm.Text.Equals("SignUp"))
            {
                int status = Controller.Insert.InsertUser(loginString, MD5Hash.Compute(passwordString), nameString, lastNameString, patronimicString,
                                    specialityString, groupString, accessLvl);
                if (status == 0)
                {
                    MessageBox.Show(@"Пользователь успешно добавлен", @"Sucsesful",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (status == -1)
                    MessageBox.Show(@"Пользователь с таким именем пользователя уже существует", @"Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (status == 1)
                    MessageBox.Show(@"Не удалось добавить нового пользователя", @"Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
            }
            else if (labelTypeForm.Text.Contains("EditUserInfo"))
            {
                string id = labelTypeForm.Text.Substring(15);
                int status = Controller.Update.UpdateUser(id, loginString, MD5Hash.Compute(passwordString), nameString, lastNameString, patronimicString,
                                    specialityString, groupString, accessLvl);
                if (status == 0)
                {
                    MessageBox.Show(@"Информация о пользователе успешно обновлена", @"Sucsesful",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (status == -1)
                    MessageBox.Show(@"Не удалось сохранить новую информацию о пользователе", @"Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
