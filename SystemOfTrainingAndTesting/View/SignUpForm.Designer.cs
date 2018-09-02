namespace SystemOfTrainingAndTesting.View
{
    partial class SignUpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxPatronimic = new System.Windows.Forms.TextBox();
            this.textBoxSpeciality = new System.Windows.Forms.TextBox();
            this.textBoxGroup = new System.Windows.Forms.TextBox();
            this.checkBoxAccessLvl = new System.Windows.Forms.CheckBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPatronimic = new System.Windows.Forms.Label();
            this.labelSpeciality = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelTypeForm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(151, 12);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(189, 20);
            this.textBoxLogin.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(151, 39);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(189, 20);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(151, 66);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(189, 20);
            this.textBoxLastName.TabIndex = 2;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(151, 93);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(189, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // textBoxPatronimic
            // 
            this.textBoxPatronimic.Location = new System.Drawing.Point(151, 120);
            this.textBoxPatronimic.Name = "textBoxPatronimic";
            this.textBoxPatronimic.Size = new System.Drawing.Size(189, 20);
            this.textBoxPatronimic.TabIndex = 4;
            // 
            // textBoxSpeciality
            // 
            this.textBoxSpeciality.Location = new System.Drawing.Point(151, 147);
            this.textBoxSpeciality.Name = "textBoxSpeciality";
            this.textBoxSpeciality.Size = new System.Drawing.Size(189, 20);
            this.textBoxSpeciality.TabIndex = 5;
            // 
            // textBoxGroup
            // 
            this.textBoxGroup.Location = new System.Drawing.Point(151, 173);
            this.textBoxGroup.Name = "textBoxGroup";
            this.textBoxGroup.Size = new System.Drawing.Size(189, 20);
            this.textBoxGroup.TabIndex = 6;
            // 
            // checkBoxAccessLvl
            // 
            this.checkBoxAccessLvl.AutoSize = true;
            this.checkBoxAccessLvl.Location = new System.Drawing.Point(12, 200);
            this.checkBoxAccessLvl.Name = "checkBoxAccessLvl";
            this.checkBoxAccessLvl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxAccessLvl.Size = new System.Drawing.Size(153, 17);
            this.checkBoxAccessLvl.TabIndex = 7;
            this.checkBoxAccessLvl.Text = "Администратор системы";
            this.checkBoxAccessLvl.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(151, 223);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "Сохранить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(42, 15);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(103, 13);
            this.labelLogin.TabIndex = 9;
            this.labelLogin.Text = "Имя пользователя";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(100, 42);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(45, 13);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "Пароль";
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(89, 69);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(56, 13);
            this.labelLastName.TabIndex = 11;
            this.labelLastName.Text = "Фамилия";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(116, 96);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(29, 13);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Имя";
            // 
            // labelPatronimic
            // 
            this.labelPatronimic.AutoSize = true;
            this.labelPatronimic.Location = new System.Drawing.Point(91, 123);
            this.labelPatronimic.Name = "labelPatronimic";
            this.labelPatronimic.Size = new System.Drawing.Size(54, 13);
            this.labelPatronimic.TabIndex = 13;
            this.labelPatronimic.Text = "Отчество";
            // 
            // labelSpeciality
            // 
            this.labelSpeciality.AutoSize = true;
            this.labelSpeciality.Location = new System.Drawing.Point(60, 150);
            this.labelSpeciality.Name = "labelSpeciality";
            this.labelSpeciality.Size = new System.Drawing.Size(85, 13);
            this.labelSpeciality.TabIndex = 14;
            this.labelSpeciality.Text = "Специальность";
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(103, 177);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(42, 13);
            this.labelGroup.TabIndex = 15;
            this.labelGroup.Text = "Группа";
            // 
            // labelTypeForm
            // 
            this.labelTypeForm.AutoSize = true;
            this.labelTypeForm.Location = new System.Drawing.Point(232, 228);
            this.labelTypeForm.Name = "labelTypeForm";
            this.labelTypeForm.Size = new System.Drawing.Size(76, 13);
            this.labelTypeForm.TabIndex = 16;
            this.labelTypeForm.Text = "labelTypeForm";
            this.labelTypeForm.Visible = false;
            // 
            // SignUpForm
            // 
            this.AcceptButton = this.buttonAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 260);
            this.Controls.Add(this.labelTypeForm);
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.labelSpeciality);
            this.Controls.Add(this.labelPatronimic);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.checkBoxAccessLvl);
            this.Controls.Add(this.textBoxGroup);
            this.Controls.Add(this.textBoxSpeciality);
            this.Controls.Add(this.textBoxPatronimic);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SignUpForm";
            this.Text = "Добавление пользователя";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPatronimic;
        private System.Windows.Forms.TextBox textBoxSpeciality;
        private System.Windows.Forms.TextBox textBoxGroup;
        private System.Windows.Forms.CheckBox checkBoxAccessLvl;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPatronimic;
        private System.Windows.Forms.Label labelSpeciality;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.Label labelTypeForm;
    }
}