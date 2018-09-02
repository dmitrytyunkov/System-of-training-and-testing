using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SystemOfTrainingAndTesting.View
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Строка информации о пользователе
        /// </summary>
        private string _userString;

        /// <summary>
        /// Строка с названием и описанием выбранного теста
        /// </summary>
        private string _selectedTestString;

        /// <summary>
        /// Строка с названием и описанием выбранной инструкции
        /// </summary>
        //private string _selectedInstructionString;

        /// <summary>
        /// Строка с названием и описанием выбранного обучающего теста
        /// </summary>
        private string _selectedEducationTestString;

        /// <summary>
        /// Описание верного ответа
        /// </summary>
        private string _descriptionCorrectAnswer = "";

        /// <summary>
        /// Номер вопроса
        /// </summary>
        private int _questionNumber;

        /// <summary>
        /// Выбранная вкладка
        /// </summary>
        private int _selectedTab = 0;

        /// <summary>
        /// Выбранная строка таблицы пользователей
        /// </summary>
        private int _selectedUserRow = 0;

        private int _j = 1;

        //private int _i = 0;

        /// <summary>
        /// Список выбранных ответов
        /// </summary>
        private readonly List<string> _selectedAnswerString = new List<string>();

        /// <summary>
        /// Список верных ответов
        /// </summary>
        private readonly List<int> _correctAnswers = new List<int>();

        /// <summary>
        /// Список ответов
        /// </summary>
        private readonly List<int> _answerId = new List<int>();


        private int _testId = 0;


        private int _themeId = 0;


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для создания главного окна
        /// </summary>
        private void CreateMainWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            #region Размещаем нужные элементы на форму
            #region Добавляем label для отображения информации о пользователе
            Label labelUser = new Label
            {
                Parent = this,
                Text = _userString,
                AutoSize = true,
                Visible = true
            };
            labelUser.Location = new Point(ClientSize.Width - (labelUser.Width + 13), 13);
            Controls.Add(labelUser);
            #endregion
            #region Добавляем label для отображения заголовка
            Label labelTitle = new Label
            {
                Parent = this,
                Text = @"Выбирите необходимый режим",
                Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                AutoSize = true,
                Visible = true
            };
            labelTitle.Location = new Point(ClientSize.Width / 2 - labelTitle.Width / 2,
                labelUser.Height + labelUser.Location.Y + 13);
            Controls.Add(labelTitle);
            #endregion
            #region Добавляем кнопку "Обучение"
            Button buttonTraining = new Button
            {
                Parent = this,
                Text = @"Обучение",
                Font = new Font("Microsoft Sans Serif", 14.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Size = new Size(75 * 3, 23 * 3),
                Visible = true,
                TabIndex = 0
            };
            buttonTraining.Location = new Point(ClientSize.Width / 2 - buttonTraining.Width / 2,
                labelTitle.Height + labelTitle.Location.Y + 13);
            Controls.Add(buttonTraining);
            buttonTraining.Click += buttonTraining_Click;
            #endregion
            #region Добавляем кнопку "Тестирование"
            Button buttonTesting = new Button
            {
                Parent = this,
                Text = @"Тестирование",
                Font = new Font("Microsoft Sans Serif", 14.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Size = new Size(75 * 3, 23 * 3),
                Visible = true,
                TabIndex = 1
            };
            buttonTesting.Location = new Point(ClientSize.Width / 2 - buttonTesting.Width / 2,
                buttonTraining.Height + buttonTraining.Location.Y + 13);
            Controls.Add(buttonTesting);
            buttonTesting.Click += buttonTesting_Click;
            #endregion
            #region Добавление кнопки в зависимости от привелегий пользователя
            switch (ObjectModel.User.AccessLevel)
            {
                case 0:
                    #region Добавляем кнопку "Управление"
                    Button buttonControl = new Button
                    {
                        Parent = this,
                        Text = @"Управление",
                        Size = new Size(75 * 3, 23 * 3),
                        Font = new Font("Microsoft Sans Serif", 14.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Location = new Point(ClientSize.Width / 2 - buttonTraining.Width / 2,
                            buttonTesting.Height + buttonTesting.Location.Y + 13),
                        Visible = true,
                        TabIndex = 2
                    };
                    Controls.Add(buttonControl);
                    buttonControl.Click += buttonControl_Click;
                    #endregion
                    break;
                case 1:
                    #region Добавляем кнопку "Статистика"
                    Button buttonStatistics = new Button
                    {
                        Parent = this,
                        Text = @"Статистика",
                        Size = new Size(75 * 3, 23 * 3),
                        Font = new Font("Microsoft Sans Serif", 14.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Location = new Point(ClientSize.Width / 2 - buttonTraining.Width / 2,
                            buttonTesting.Height + buttonTesting.Location.Y + 13),
                        Visible = true,
                        TabIndex = 2
                    };
                    Controls.Add(buttonStatistics);
                    buttonStatistics.Click += buttonStatistics_Click;
                    #endregion
                    break;
                default:
                    throw new Exception("Неверный уровень прав пользователя", null);
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// Метод для создания окна обучения
        /// </summary>
        private void CreateTrainingWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            #region Размещаем нужные элементы на форме
            #region Добавляем label для отображения информации о пользователе
            Label labelUser = new Label
            {
                Parent = this,
                Text = _userString,
                AutoSize = true,
                Visible = true
            };
            labelUser.Location = new Point(ClientSize.Width - (labelUser.Width + 13), 13);
            Controls.Add(labelUser);
            #endregion
            #region Добавляем label для отображения заголовка
            Label labelTitle = new Label
            {
                Parent = this,
                Text = @"Выбирите необходимую тему",
                Font = new Font("Microsoft Sans Serif", 21.75F,
                    FontStyle.Regular, GraphicsUnit.Point, 204),
                AutoSize = true,
                Visible = true
            };
            labelTitle.Location = new Point(ClientSize.Width / 2 - labelTitle.Width / 2,
                labelUser.Height + labelUser.Location.Y + 13);
            Controls.Add(labelTitle);
            #endregion
            #region Добавляем listBox с доступными темами
            ListBox listBoxThemes = new ListBox
            {
                Parent = this,
                TabIndex = 0,
                Size = new Size(ClientSize.Width - 26,
                    ClientSize.Height - labelTitle.Location.Y - labelTitle.Height - 13 * 3 - 23),
                Visible = true,
                Location = new Point(13, labelTitle.Height + labelTitle.Location.Y + 13)
            };
            foreach (string item in ObjectModel.Themes./*TitleAndDescription*/Title)
            {
                listBoxThemes.Items.Add(item);
            }
            Controls.Add(listBoxThemes);
            listBoxThemes.SelectedIndexChanged += listBoxTests_SelectedIndexChanged;
            listBoxThemes.SelectedIndex = 0;
            #endregion
            #region Добавляем кнопку "Выбрать"
            Button buttonSelect = new Button
            {
                Parent = this,
                Text = @"Выбрать",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 2
            };
            buttonSelect.Location = new Point(ClientSize.Width - buttonSelect.Width - 13,
                listBoxThemes.Height + listBoxThemes.Location.Y + 13);
            Controls.Add(buttonSelect);
            buttonSelect.Click += buttonSelectTheme_Click;
            #endregion
            #region Добавить кнопку "Назад"
            Button buttonBack = new Button
            {
                Parent = this,
                Text = @"Назад",
                Size = new Size(75, 23),
                Location = new Point(13, listBoxThemes.Height + listBoxThemes.Location.Y + 13),
                Visible = true,
                TabIndex = 1
            };
            Controls.Add(buttonBack);
            buttonBack.Click += buttonBack_Click;
            #endregion
            #endregion
        }

        /// <summary>
        /// Метод для создания окна тестирования
        /// </summary>
        private void CreateTestingWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            #region Размещаем нужные элементы на форме
            #region Добавляем label для отображения информации о пользователе
            Label labelUser = new Label
            {
                Parent = this,
                Text = _userString,
                AutoSize = true,
                Visible = true
            };
            labelUser.Location = new Point(ClientSize.Width - labelUser.Width - 13, 13);
            Controls.Add(labelUser);
            #endregion
            #region Добавляем label для отображения заголовка
            Label labelTitle = new Label
            {
                Parent = this,
                Text = @"Выбирите необходимый тест",
                Font = new Font("Microsoft Sans Serif", 21.75F,
                    FontStyle.Regular, GraphicsUnit.Point, 204),
                AutoSize = true,
                Visible = true
            };
            labelTitle.Location = new Point(ClientSize.Width / 2 - labelTitle.Width / 2,
                labelUser.Height + labelUser.Location.Y + 13);
            Controls.Add(labelTitle);
            #endregion
            #region Добавляем listBox с доступными тестами
            ListBox listBoxTests = new ListBox
            {
                Parent = this,
                TabIndex = 0,
                Size = new Size(ClientSize.Width - 26,
                    ClientSize.Height - labelTitle.Location.Y - labelTitle.Height - 13 * 3 - 23),
                Visible = true,
                Location = new Point(13, labelTitle.Height + labelTitle.Location.Y + 13),
                SelectionMode = SelectionMode.One
            };
            for (int i = 0; i < ObjectModel.Tests.Id.Count; i++)
                if (ObjectModel.Tests.Type[i] == 1)
                    listBoxTests.Items.Add(ObjectModel.Tests.Title[i]);
            Controls.Add(listBoxTests);
            listBoxTests.SelectedIndexChanged += listBoxTests_SelectedIndexChanged;
            listBoxTests.SelectedIndex = 0;
            #endregion
            #region Добавляем кнопку "Выбрать"
            Button buttonSelect = new Button
            {
                Parent = this,
                Text = @"Выбрать",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 2
            };
            buttonSelect.Location = new Point(ClientSize.Width - buttonSelect.Width - 13,
                listBoxTests.Height + listBoxTests.Location.Y + 13);
            Controls.Add(buttonSelect);
            buttonSelect.Click += buttonSelect_Click;
            #endregion
            #region Добавить кнопку "Назад"
            Button buttonBack = new Button
            {
                Parent = this,
                Text = @"Назад",
                Size = new Size(75, 23),
                Location = new Point(13, listBoxTests.Height + listBoxTests.Location.Y + 13),
                Visible = true,
                TabIndex = 1
            };
            Controls.Add(buttonBack);
            buttonBack.Click += buttonBack_Click;
            #endregion
            #endregion
        }

        /// <summary>
        /// Метод для создания окна статистики
        /// </summary>
        private void CreateStatisticsWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            #region Размещаем нужные элементы на форме
            #region Добавляем label для отображения информации о пользователе
            Label labelUser = new Label
            {
                Parent = this,
                Text = _userString,
                AutoSize = true,
                Visible = true
            };
            labelUser.Location = new Point(ClientSize.Width - (labelUser.Width + 13), 13);
            Controls.Add(labelUser);
            #endregion
            #region Добавление вкладок
            TabControl tabControl = new TabControl
            {
                Parent = this,
                Location = new Point(0, labelUser.Height),
                TabIndex = 0,
                Size = new Size(ClientSize.Width, ClientSize.Height - labelUser.Height - 23 - 13 * 2),
                Visible = true
            };
            Controls.Add(tabControl);
            TabPage tabPageTraining = new TabPage
            {
                Location = new Point(4, 22),
                Padding = new Padding(3),
                Size = new Size(tabControl.Size.Width - 8, tabControl.Size.Width - 26),
                TabIndex = 0,
                Text = @"Обучение"
            };
            tabControl.Controls.Add(tabPageTraining);
            TabPage tabPageTesting = new TabPage
            {
                Location = new Point(4, 22),
                Padding = new Padding(3),
                Size = new Size(tabControl.Size.Width - 8, tabControl.Size.Width - 26),
                TabIndex = 1,
                Text = @"Тестирование"
            };
            tabControl.Controls.Add(tabPageTesting);
            #endregion
            #region Добавляем таблицу с доступными тестами
            DataGridView dataGridViewTests = new DataGridView
            {
                Location = new Point(13, 13),
                Size = new Size(tabPageTesting.Size.Width - 26,
                    tabPageTesting.Size.Height - 13 * 2),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                Visible = true,
                ReadOnly = true
            };
            dataGridViewTests.Columns.Add("tests", "Название теста");
            dataGridViewTests.Columns.Add("number_of_correct_answers", "Количество верных ответов");
            dataGridViewTests.Columns.Add("number_of_question", "Количество вопросов");
            dataGridViewTests.Columns.Add("percent", "Процент");
            foreach (int idTest in ObjectModel.Tests.Id)
            {
                int index = ObjectModel.Tests.Id.IndexOf(idTest);
                int numberOfQuestion = 0;//Info.Tests.NumberOfQuestion[index];
                object[] row = new object[4];
                row[0] = ObjectModel.Tests.Title[index];
                index = ObjectModel.Statistics.IdTest.IndexOf(idTest);
                if (index == -1)
                {
                    row[1] = "не пройдено";
                    row[2] = numberOfQuestion.ToString();
                    row[3] = null;
                }
                else
                {
                    int numberOfCorrectAnswer = ObjectModel.Statistics.NumberOfCorrectAnswer[index];
                    double percent = Convert.ToDouble(numberOfCorrectAnswer) / numberOfQuestion;
                    row[1] = numberOfCorrectAnswer.ToString();
                    row[2] = numberOfQuestion.ToString();
                    row[3] = percent.ToString("P");
                }
                dataGridViewTests.Rows.Add(row);
            }
            tabPageTesting.Controls.Add(dataGridViewTests);
            #endregion
            #region Добавляем таблицу с доступными темами
            DataGridView dataGridViewThemes = new DataGridView
            {
                Location = new Point(13, 13),
                Size = new Size(tabPageTesting.Size.Width - 26,
                    tabPageTesting.Size.Height - 13 * 2),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                Visible = true,
                ReadOnly = true
            };
            dataGridViewThemes.Columns.Add("themes", "Название темы");
            dataGridViewThemes.Columns.Add("tests", "Название теста");
            dataGridViewThemes.Columns.Add("number_of_correct_answers", "Количество верных ответов");
            dataGridViewThemes.Columns.Add("number_of_question", "Количество вопросов");
            dataGridViewThemes.Columns.Add("percent", "Процент");
            foreach (int idTheme in ObjectModel.Themes.Id)
            {
                int index = ObjectModel.Themes.Id.IndexOf(idTheme);
                List<int> tests = ObjectModel.EducationTests.IdTheme.FindAll(p => p == idTheme);
                foreach (int idTest in tests)
                {
                    int indexTest = ObjectModel.EducationTests.IdTheme.IndexOf(idTest);
                    int numberOfQuestion = ObjectModel.EducationTests.NumberOfQuestion[indexTest];
                    object[] row = new object[5];
                    row[0] = ObjectModel.Themes.Title[index];
                    row[1] = ObjectModel.EducationTests.Title[indexTest];
                    indexTest = ObjectModel.EducationStatistics.IdTest.IndexOf(idTest);
                    if (indexTest == -1)
                    {
                        row[2] = "не пройдено";
                        row[3] = numberOfQuestion.ToString();
                        row[4] = null;
                    }
                    else
                    {
                        int numberOfCorrectAnswer = ObjectModel.EducationStatistics.NumberOfCorrectAnswer[indexTest];
                        double percent = Convert.ToDouble(numberOfCorrectAnswer) / numberOfQuestion;
                        row[2] = numberOfCorrectAnswer.ToString();
                        row[3] = numberOfQuestion.ToString();
                        row[4] = percent.ToString("P");
                    }
                    dataGridViewThemes.Rows.Add(row);
                }
            }
            tabPageTraining.Controls.Add(dataGridViewThemes);
            #endregion
            #region Добавить кнопку "Назад"
            Button buttonBack = new Button
            {
                Parent = this,
                Text = @"Назад",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 1
            };
            buttonBack.Location = new Point(ClientSize.Width / 2 - buttonBack.Width / 2,
                tabControl.Height + tabControl.Location.Y + 13);
            Controls.Add(buttonBack);
            buttonBack.Click += buttonBack_Click;
            #endregion
            #endregion
        }

        /// <summary>
        /// Метод для создания окна вопроса
        /// </summary>
        private void CreateQuestionWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            _selectedAnswerString.Clear();
            if (_testId == ObjectModel.Questions.IdTest[_questionNumber])
            {
                List<int> __idAnswer = new List<int>();
                int __i = 0;
                foreach (var idQuestion in ObjectModel.Answers.IdQuestion)
                {
                    if (idQuestion == ObjectModel.Questions.Id[_questionNumber])
                        __idAnswer.Add(__i);
                    __i++;
                }
                #region Размещаем нужные элементы на форме
                #region Добавляем label для отображения информации о пользователе
                Label labelUser = new Label
                {
                    Parent = this,
                    Text = _userString,
                    AutoSize = true,
                    Visible = true
                };
                labelUser.Location = new Point(ClientSize.Width - (labelUser.Width + 13), 13);
                Controls.Add(labelUser);
                #endregion
                #region Добавляем label для отображения вопроса
                Label labelQuestion = new Label
                {
                    Parent = this,
                    Text = ObjectModel.Questions.Question[_questionNumber],
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Microsoft Sans Serif", 21.75F,
                        FontStyle.Regular, GraphicsUnit.Point, 204),
                    Visible = true
                };
                #region Разбиение вопроса на несколько строк
                int position = 0;
                while (position < labelQuestion.Text.Length - 30)
                {
                    position = labelQuestion.Text.IndexOf(" ", position + 30, StringComparison.Ordinal);
                    if (position > 0)
                    {
                        labelQuestion.Text = labelQuestion.Text.Insert(position, "\r\n");
                    }
                    else
                    {
                        position = labelQuestion.Text.Length;
                    }
                }
                #endregion
                labelQuestion.AutoSize = true;
                labelQuestion.Location = new Point(ClientSize.Width / 2 - labelQuestion.Width / 2,
                    labelUser.Height + labelUser.Location.Y + 13);
                Controls.Add(labelQuestion);
                #endregion
                #region Добавляем ответы
                Point answerLocation = new Point();
                switch (ObjectModel.Questions.TypeAnswer[_questionNumber])
                {
                    #region Выбор одного варианта ответа
                    case 1:
                        for (int i = 0; i < __idAnswer.Count; i++)
                        {
                            RadioButton radioButtonAnswer = new RadioButton
                            {
                                Parent = this,
                                Text = ObjectModel.Answers.Answer[__idAnswer[i]],
                                Visible = true,
                                AutoSize = true,
                                Location = new Point(13, labelQuestion.Height + labelQuestion.Location.Y + 13 + i * 3 * 13)
                            };
                            Controls.Add(radioButtonAnswer);
                            answerLocation = radioButtonAnswer.Location;
                            radioButtonAnswer.CheckedChanged += radioButtonAnswer_CheckedChanged;
                            if (_answerId.Contains(ObjectModel.Answers.Id[__idAnswer[i]]))
                            {
                                radioButtonAnswer.Checked = true;
                            }
                        }
                        break;
                    #endregion
                    #region Выбор нескольких вариантов ответа
                    case 2:
                        for (int i = 0; i < __idAnswer.Count; i++)
                        {
                            CheckBox checkBoxAnswer = new CheckBox
                            {
                                Parent = this,
                                Text = ObjectModel.Answers.Answer[__idAnswer[i]],
                                Visible = true,
                                AutoSize = true,
                                Location = new Point(13, labelQuestion.Height + labelQuestion.Location.Y + 13 + i * 3 * 13),
                                TabIndex = i
                            };
                            Controls.Add(checkBoxAnswer);
                            answerLocation = checkBoxAnswer.Location;
                            checkBoxAnswer.CheckStateChanged += checkBoxAnswer_CheckStateChanged;
                            if (_answerId.Contains(ObjectModel.Answers.Id[__idAnswer[i]]))
                            {
                                checkBoxAnswer.CheckState = CheckState.Checked;
                            }
                        }
                        break;
                    #endregion
                }
                #endregion
                #region Добавляем label для отображения описания верного ответа
                Label labelDescriptionCorrectAnswer = new Label
                {
                    Parent = this,
                    Text = _descriptionCorrectAnswer,
                    TextAlign = ContentAlignment.TopLeft,
                    Visible = true,
                    ForeColor = Color.Red
                };
                #region Разбиение описания на несколько строк
                int pos = 0;
                while (pos < labelDescriptionCorrectAnswer.Text.Length - 50)
                {
                    pos = labelDescriptionCorrectAnswer.Text.IndexOf(" ", pos + 50, StringComparison.Ordinal);
                    if (pos > 0)
                    {
                        labelDescriptionCorrectAnswer.Text = labelDescriptionCorrectAnswer.Text.Insert(pos, "\r\n");
                    }
                    else
                    {
                        pos = labelDescriptionCorrectAnswer.Text.Length;
                    }
                }
                #endregion
                labelDescriptionCorrectAnswer.Location = new Point(13, answerLocation.Y + 13 * 3);
                labelDescriptionCorrectAnswer.AutoSize = true;
                Controls.Add(labelDescriptionCorrectAnswer);
                #endregion
                #region Добавляем кнопку "Следующий"
                Button buttonNext = new Button
                {
                    Parent = this,
                    Text = _questionNumber == ObjectModel.Questions.Id.Count - 1 ? "Завершить" : "Следующий",
                    Size = new Size(85, 23),
                    Visible = true,
                    TabIndex = ObjectModel.Answers.Answer.Count + 2
                };
                buttonNext.Location = new Point(ClientSize.Width - buttonNext.Width - 13,
                    answerLocation.Y + 13 * 3);
                Controls.Add(buttonNext);
                buttonNext.Click += buttonNext_Click;
                #endregion
                #region Добавить кнопку "Предыдущий"
                /*Button buttonPrevious = new Button
                {
                    Parent = this,
                    Text = @"Предыдущий",
                    Size = new Size(85, 23),
                    Location = new Point(13, labelDescriptionCorrectAnswer.Location.Y + 13 * 3),
                    Visible = true,
                    Enabled = _questionNumber != 0,
                    TabIndex = Info.Answers.Answer.Count + 1,
                };
                Controls.Add(buttonPrevious);
                buttonPrevious.Click += buttonPrevious_Click;*/
                #endregion
                #endregion
            }
            else
            {
                _questionNumber++;
                if (_questionNumber == ObjectModel.Questions.Id.Count)
                {
                    MessageBox.Show(@"Верных ответов: " + _correctAnswers.Count, @"Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int index = ObjectModel.Tests./*TitleAndDescription*/Title.IndexOf(_selectedTestString);
                    Controller.Insert.InsertStatistic(ObjectModel.Tests.Id[index], _correctAnswers.Count);
                    CreateMainWindow();
                }
                else
                {
                    CreateQuestionWindow();
                }
            }
        }

        /// <summary>
        /// Метод для создания окна выбора обучающего теста или инструкции
        /// </summary>
        private void CreateSelectWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            #region Размещаем нужные элементы на форме
            #region Добавляем label для отображения информации о пользователе
            Label labelUser = new Label
            {
                Parent = this,
                Text = _userString,
                AutoSize = true,
                Visible = true
            };
            labelUser.Location = new Point(ClientSize.Width - labelUser.Width - 13, 13);
            Controls.Add(labelUser);
            #endregion
            #region Добавляем label для отображения заголовка
            Label labelTitle = new Label
            {
                Parent = this,
                Text = @"Выбирите необходимый обучающий тест" + Environment.NewLine + @"или инструкцию",
                Font = new Font("Microsoft Sans Serif", 21.75F,
                    FontStyle.Regular, GraphicsUnit.Point, 204),
                AutoSize = true,
                Visible = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            labelTitle.Location = new Point(ClientSize.Width / 2 - labelTitle.Width / 2,
                labelUser.Height + labelUser.Location.Y + 13);
            Controls.Add(labelTitle);
            #endregion
            #region Добавляем listBox с доступными тестами
            ListBox listBoxTests = new ListBox
            {
                Parent = this,
                TabIndex = 0,
                Size = new Size(ClientSize.Width - 26,
                    ClientSize.Height - labelTitle.Location.Y - labelTitle.Height - 13 * 3 - 23),
                Visible = true,
                Location = new Point(13, labelTitle.Height + labelTitle.Location.Y + 13),
                SelectionMode = SelectionMode.One
            };
            /*foreach (string item in Info.Tests.Title/*EducationTests.TitleAndDescription*//*)
            {
                listBoxTests.Items.Add("Тест. " + item);
            }*/
            for (int i = 0; i < ObjectModel.Tests.Id.Count; i++)
                if (ObjectModel.Tests.Type[i] == 2 && ObjectModel.Tests.IdTheme[i] == _themeId)
                    listBoxTests.Items.Add("Тест. " + ObjectModel.Tests.Title[i]);
            /*foreach (string item in Info.Instructions.TitleAndDescription)
            {
                listBoxTests.Items.Add("Инструкция. " + item);
            }*/
            Controls.Add(listBoxTests);
            if (ObjectModel.Tests.Title.Count/*.EducationTests.TitleAndDescription.Count*/ != 0)
            {
                listBoxTests.SelectedIndexChanged += listBoxTests_SelectedIndexChanged;
                listBoxTests.SelectedIndex = 0;
            }
            #endregion
            #region Добавляем кнопку "Выбрать"
            Button buttonSelect = new Button
            {
                Parent = this,
                Text = @"Выбрать",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 2
            };
            buttonSelect.Location = new Point(ClientSize.Width - buttonSelect.Width - 13,
                listBoxTests.Height + listBoxTests.Location.Y + 13);
            Controls.Add(buttonSelect);
            buttonSelect.Click += buttonSelect_Click;
            #endregion
            #region Добавить кнопку "Назад"
            Button buttonBack = new Button
            {
                Parent = this,
                Text = @"Назад",
                Size = new Size(75, 23),
                Location = new Point(13, listBoxTests.Height + listBoxTests.Location.Y + 13),
                Visible = true,
                TabIndex = 1
            };
            Controls.Add(buttonBack);
            buttonBack.Click += buttonTraining_Click;
            #endregion
            #endregion
        }

        /// <summary>
        /// Метод для создания окна управления
        /// </summary>
        private void CreateControlWindow()
        {
            #region Убираем элементы с формы
            Controls.Clear();
            #endregion
            #region Размещаем нужные элементы на форме
            #region Добавляем label для отображения информации о пользователе
            Label labelUser = new Label
            {
                Parent = this,
                Text = _userString,
                AutoSize = true,
                Visible = true
            };
            labelUser.Location = new Point(ClientSize.Width - (labelUser.Width + 13), 13);
            Controls.Add(labelUser);
            #endregion
            #region Добавление вкладок
            TabControl tabControl = new TabControl
            {
                Parent = this,
                Location = new Point(0, labelUser.Location.Y),
                TabIndex = 0,
                Size = new Size(ClientSize.Width, ClientSize.Height - labelUser.Height - 23 - 13 * 2),
                Visible = true
            };
            tabControl.Selected += tabControl_Selected;
            Controls.Add(tabControl);
            TabPage tabPageUsers = new TabPage
            {
                Location = new Point(4, 22),
                Padding = new Padding(3),
                Size = new Size(tabControl.Size.Width, tabControl.Size.Width - 26),
                TabIndex = 1,
                Text = @"Пользователи"
            };
            tabControl.Controls.Add(tabPageUsers);
            TabPage tabPageTraining = new TabPage
            {
                Location = new Point(4, 22),
                Padding = new Padding(3),
                Size = new Size(tabControl.Size.Width, tabControl.Size.Width - 26),
                TabIndex = 1,
                Text = @"Обучение"
            };
            tabControl.Controls.Add(tabPageTraining);
            TabPage tabPageTesting = new TabPage
            {
                Location = new Point(4, 22),
                Padding = new Padding(3),
                Size = new Size(tabControl.Size.Width - 8, tabControl.Size.Width - 26),
                TabIndex = 2,
                Text = @"Тестирование"
            };
            tabControl.Controls.Add(tabPageTesting);
            #endregion
            #region Добавляем таблицу с доступными пользователями
            DataGridView dataGridViewUsers = new DataGridView
            {
                Location = new Point(13, 13),
                Size = new Size(tabPageTesting.Size.Width - 26,
                    tabPageTesting.Size.Height - 13 * 2),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                Visible = true,
                ReadOnly = true,
                MultiSelect = false
            };
            dataGridViewUsers.SelectionChanged += dataGridViewUsers_SelectionChanged;
            dataGridViewUsers.Columns.Add("login", "Имя пользователя");
            dataGridViewUsers.Columns.Add("last_name", "Фамилия");
            dataGridViewUsers.Columns.Add("name", "Имя");
            dataGridViewUsers.Columns.Add("middle_name", "Отчество");
            //dataGridViewUsers.Columns.Add("birthday", "Дата рождения");
            dataGridViewUsers.Columns.Add("speciality", "Специальность");
            dataGridViewUsers.Columns.Add("group", "Группа");
            foreach (int id in ObjectModel.Users.Id)
            {
                int index = ObjectModel.Users.Id.IndexOf(id);
                object[] row = new object[6];
                row[0] = ObjectModel.Users.Login[index];
                row[1] = ObjectModel.Users.LastName[index];
                row[2] = ObjectModel.Users.Name[index];
                row[3] = ObjectModel.Users.Patronimic[index];
                //row[4] = Info.Users.Birthday[index].ToString("D");
                row[4] = ObjectModel.Users.Speciality[index];
                row[5] = ObjectModel.Users.Group[index];
                dataGridViewUsers.Rows.Add(row);
            }
            tabPageUsers.Controls.Add(dataGridViewUsers);
            #endregion
            #region Добавляем таблицу с доступными тестами
            DataGridView dataGridViewTests = new DataGridView
            {
                Location = new Point(13, 13),
                Size = new Size(tabPageTesting.Size.Width - 26,
                    tabPageTesting.Size.Height - 13 * 2),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                Visible = true,
                ReadOnly = true,
                MultiSelect = false
            };
            dataGridViewTests.Columns.Add("title", "Название");
            dataGridViewTests.Columns.Add("descriotion", "Описание");
            dataGridViewTests.Columns.Add("number_of_question", "Количество вопросов");
            foreach (int id in ObjectModel.Tests.Id)
            {
                int index = ObjectModel.Tests.Id.IndexOf(id);
                object[] row = new object[3];
                row[0] = ObjectModel.Tests.Title[index];
                row[1] = ObjectModel.Tests.Description[index];
                //row[2] = Info.Tests.NumberOfQuestion[index];
                dataGridViewTests.Rows.Add(row);
            }
            tabPageTesting.Controls.Add(dataGridViewTests);
            #endregion
            #region Добавляем таблицу с доступными темами
            DataGridView dataGridViewThemes = new DataGridView
            {
                Location = new Point(13, 13),
                Size = new Size(tabPageTesting.Size.Width - 26,
                    tabPageTesting.Size.Height - 13 * 2),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                Visible = true,
                ReadOnly = true,
                MultiSelect = false
            };
            dataGridViewThemes.Columns.Add("title_theme", "Название темы");
            dataGridViewThemes.Columns.Add("descriotion_theme", "Описание темы");
            dataGridViewThemes.Columns.Add("type", "Тип");
            dataGridViewThemes.Columns.Add("title", "Название");
            dataGridViewThemes.Columns.Add("descriotion", "Описание");
            dataGridViewThemes.Columns.Add("number_of_question", "Количество вопросов");
            foreach (int id in ObjectModel.Themes.Id)
            {
                int index = ObjectModel.Themes.Id.IndexOf(id);
                object[] row = new object[6];
                List<int> tests = ObjectModel.EducationTests.IdTheme.FindAll(p => p == id);
                List<int> instructions = ObjectModel.Instructions.IdTheme.FindAll(p => p == id);
                if ((tests.Count == 0) && (instructions.Count == 0))
                {
                    row[0] = ObjectModel.Themes.Title[index];
                    row[1] = ObjectModel.Themes.Description[index];
                    row[2] = null;
                    row[3] = null;
                    row[4] = null;
                    row[5] = null;
                    dataGridViewThemes.Rows.Add(row);
                }
                else
                {
                    foreach (var idTheme in tests)
                    {
                        int indexTests = ObjectModel.EducationTests.IdTheme.IndexOf(idTheme);
                        row[0] = ObjectModel.Themes.Title[index];
                        row[1] = ObjectModel.Themes.Description[index];
                        row[2] = "Т";
                        row[3] = ObjectModel.EducationTests.Title[indexTests];
                        row[4] = ObjectModel.EducationTests.Description[indexTests];
                        row[5] = ObjectModel.EducationTests.NumberOfQuestion[indexTests];
                        dataGridViewThemes.Rows.Add(row);
                    }
                    foreach (var idTheme in instructions)
                    {
                        int indexInstruction = ObjectModel.Instructions.IdTheme.IndexOf(idTheme);
                        row[0] = ObjectModel.Themes.Title[index];
                        row[1] = ObjectModel.Themes.Description[index];
                        row[2] = "И";
                        row[3] = ObjectModel.Instructions.Title[indexInstruction];
                        row[4] = ObjectModel.Instructions.Description[indexInstruction];
                        row[5] = null;
                        dataGridViewThemes.Rows.Add(row);
                    }
                }
            }
            tabPageTraining.Controls.Add(dataGridViewThemes);
            #endregion
            #region Добавить кнопку "Назад"
            Button buttonBack = new Button
            {
                Parent = this,
                Text = @"Назад",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 1
            };
            buttonBack.Location = new Point(ClientSize.Width / 2 - buttonBack.Width / 2,
                tabControl.Height + tabControl.Location.Y + 13);
            Controls.Add(buttonBack);
            buttonBack.Click += buttonBack_Click;
            #endregion
            #region Добавить кнопку "Добавить"
            Button buttonAdd = new Button
            {
                Parent = this,
                Text = @"Добавить",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 1
            };
            buttonAdd.Location = new Point(buttonBack.Location.X - buttonAdd.Width - 13,
                tabControl.Height + tabControl.Location.Y + 13);
            Controls.Add(buttonAdd);
            buttonAdd.Click += buttonAdd_Click;
            #endregion
            #region Добавить кнопку "Обновить"
            Button buttonEdit = new Button
            {
                Parent = this,
                Text = @"Обновить",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 1
            };
            buttonEdit.Location = new Point(buttonAdd.Location.X - buttonEdit.Width - 13,
                tabControl.Height + tabControl.Location.Y + 13);
            Controls.Add(buttonEdit);
            buttonEdit.Click += buttonEdit_Click;
            #endregion
            #region Добавить кнопку "Удалить"
            Button buttonDelete = new Button
            {
                Parent = this,
                Text = @"Удалить",
                Size = new Size(75, 23),
                Visible = true,
                TabIndex = 1
            };
            buttonDelete.Location = new Point(buttonEdit.Location.X - buttonDelete.Width - 13,
                tabControl.Height + tabControl.Location.Y + 13);
            Controls.Add(buttonDelete);
            buttonDelete.Click += buttonDelete_Click;
            #endregion
            #endregion
        }

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            if (Controller.Authorization.Auth(textBoxLogin.Text, MD5Hash.Compute(textBoxPassword.Text), out _userString))
            {
                #region Изменяем размеры и параметры формы
                ClientSize = new Size(640, 480);
                MaximizeBox = true;
                FormBorderStyle = FormBorderStyle.Sizable;
                MinimumSize = new Size(640, 480);
                #endregion
                CreateMainWindow();
            }
            else
            {
                MessageBox.Show(_userString, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTraining_Click(object sender, EventArgs e)
        {
            Controller.Select.SelectThemes();
            CreateTrainingWindow();
        }

        private void buttonTesting_Click(object sender, EventArgs e)
        {
            Controller.Select.SelectThemes();
            CreateTestingWindow();
        }

        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            Controller.Select.SelectStatistics();
            Controller.Select.SelectThemes();
            CreateStatisticsWindow();
        }

        private void buttonControl_Click(object sender, EventArgs e)
        {
            Controller.Select.SelectUsers();
            Controller.Select.SelectThemes();
            CreateControlWindow();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (_selectedTestString != null)
            {
                int index = ObjectModel.Tests.Title.IndexOf(_selectedTestString);
                #region Сброс в начальное значение счетчика и очистка списков
                _questionNumber = 0;
                _correctAnswers.Clear();
                _answerId.Clear();
                #endregion
                _testId = index + 1;
                CreateQuestionWindow();
            }
            else if (_selectedEducationTestString != null)
            {
                int index = ObjectModel.Tests.Title.IndexOf(_selectedEducationTestString);
                #region Сброс в начальное значение счетчика и очистка списков
                _questionNumber = 0;
                _correctAnswers.Clear();
                _answerId.Clear();
                #endregion
                _testId = index + 1;
                CreateQuestionWindow();
            }
        }

        private void buttonSelectTheme_Click(object sender, EventArgs e)
        {
            int index = ObjectModel.Themes.Title.IndexOf(_selectedTestString);
            _themeId = index + 1;
            CreateSelectWindow();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            CreateMainWindow();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            int i = 0; //Счетчик количества данных верных ответов на вопрос
            List<bool> answer = new List<bool>();
            for (int j = 0; j < ObjectModel.Answers.Id.Count; j++)
                if (ObjectModel.Answers.IdQuestion[j] == ObjectModel.Questions.Id[_questionNumber] && ObjectModel.Answers.CorrectAnswer[j])
                    answer.Add(ObjectModel.Answers.CorrectAnswer[j]); //Для определения количества верных ответов на вопрос
            #region Удаляем все сохраненные ответы на текущий вопрос
            foreach (int id in ObjectModel.Answers.Id)
            {
                if (_answerId.Contains(id))
                {
                    _answerId.Remove(id);
                }
            }
            #endregion
            #region Добавление очередного ответа в список и проверка на правильность
            if (ObjectModel.Tests.Type[_testId-1] == 1)
            {
                foreach (string selectedAnswerString in _selectedAnswerString)
                {
                    int index = ObjectModel.Answers.Answer.IndexOf(selectedAnswerString);
                    _answerId.Add(ObjectModel.Answers.Id[index]);
                    if (ObjectModel.Answers.CorrectAnswer[index])
                    {
                        if (!_correctAnswers.Contains(ObjectModel.Questions.Id[_questionNumber]))
                        {
                            i++;
                            if (i == answer.Count)
                            {
                                _correctAnswers.Add(ObjectModel.Questions.Id[_questionNumber]);
                            }
                        }
                    }
                    else
                    {
                        if (_correctAnswers.Contains(ObjectModel.Questions.Id[_questionNumber]))
                        {
                            _correctAnswers.Remove(ObjectModel.Questions.Id[_questionNumber]);
                        }
                        //break;
                    }
                }
                _questionNumber++;
            #endregion
            }
            else if (ObjectModel.Tests.Type[_testId-1] == 2)
            {
                #region Добавление очередного ответа в список и проверка на правильность
                foreach (string selectedAnswerString in _selectedAnswerString)
                {
                    int index = ObjectModel.Answers.Answer.IndexOf(selectedAnswerString);
                    _answerId.Add(ObjectModel.Answers.Id[index]);
                    if (ObjectModel.Answers.CorrectAnswer[index])
                    {
                        if (!_correctAnswers.Contains(ObjectModel.Questions.Id[_questionNumber]))
                        {
                            i++;
                            if (i == answer.Count)
                            {
                                _descriptionCorrectAnswer = "";
                                _j = 1;
                                _correctAnswers.Add(ObjectModel.Questions.Id[_questionNumber]);
                            }
                        }
                    }
                    else
                    {
                        if (_correctAnswers.Contains(ObjectModel.Questions.Id[_questionNumber]))
                        {
                            _correctAnswers.Remove(ObjectModel.Questions.Id[_questionNumber]);
                        }
                        //break;
                    }
                }
                #endregion
                #region Вывод описания верного ответа в случае неправильного ответа
                if ((i != answer.Count)/* && (_j == 1) && (_i == 0)*/)
                {
                    _j = 0;
                    _descriptionCorrectAnswer = ObjectModel.Questions.Description[_questionNumber];
                    //_i = 1;
                }
                /*else if ((i != answer.Count) && (_j == 0) && (_i == 0))
                {
                    _j = 1;
                    _descriptionCorrectAnswer = "";
                    _i = 1;
                }
                else if ((i != answer.Count) && (_j == 0) && (_i == 1))
                {
                    _j = 1;
                    _descriptionCorrectAnswer = "";
                    _i = 0;
                }
                else if ((i != answer.Count) && (_j == 1) && (_i == 1))
                {
                    _j = 0;
                    _descriptionCorrectAnswer = "";
                    _i = 0;
                }*/
                _questionNumber += _j;
                #endregion
            }
            #region Выбор действия в зависимости от типа кнопки
            if (_questionNumber == ObjectModel.Questions.Id.Count)
            {
                MessageBox.Show(@"Верных ответов: " + _correctAnswers.Count, @"Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int index = ObjectModel.Tests./*TitleAndDescription*/Title.IndexOf(_selectedTestString);
                Controller.Insert.InsertStatistic(ObjectModel.Tests.Id[index], _correctAnswers.Count);
                CreateMainWindow();
            }
            else
            {
                CreateQuestionWindow();
            }
            #endregion
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            #region Удаляем все сохраненные ответы на текущий вопрос
            foreach (int id in ObjectModel.Answers.Id)
            {
                if (_answerId.Contains(id))
                {
                    _answerId.Remove(id);
                }
            }
            #endregion
            #region Добавление очередного ответа в список
            foreach (string selectedAnswerString in _selectedAnswerString)
            {
                int index = ObjectModel.Answers.Answer.IndexOf(selectedAnswerString);
                _answerId.Add(ObjectModel.Answers.Id[index]);
            }
            #endregion
            _questionNumber--;
            CreateQuestionWindow();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (_selectedTab == 0)
            {
                Form signUpForm = new SignUpForm();
                signUpForm.Controls["labelTypeForm"].Text = "SignUp";
                signUpForm.ShowDialog();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Form signUpForm = new SignUpForm();
            signUpForm.Controls["textBoxLogin"].Text = ObjectModel.Users.Login[_selectedUserRow];
            signUpForm.Controls["textBoxPassword"].Text = ObjectModel.Users.Password[_selectedUserRow];
            signUpForm.Controls["textBoxName"].Text = ObjectModel.Users.Name[_selectedUserRow];
            signUpForm.Controls["textBoxLastName"].Text = ObjectModel.Users.LastName[_selectedUserRow];
            signUpForm.Controls["textBoxPatronimic"].Text = ObjectModel.Users.Patronimic[_selectedUserRow];
            signUpForm.Controls["textBoxSpeciality"].Text = ObjectModel.Users.Speciality[_selectedUserRow];
            signUpForm.Controls["textBoxGroup"].Text = ObjectModel.Users.Group[_selectedUserRow];
            CheckBox checkBox = signUpForm.Controls["checkBoxAccessLvl"] as CheckBox;
            checkBox.Checked = ObjectModel.Users.AccessLvl[_selectedUserRow] == 0 ? true : false;
            signUpForm.Controls["labelTypeForm"].Text = "EditUserInfo = " + ObjectModel.Users.Id[_selectedUserRow];
            signUpForm.ShowDialog();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int status = Controller.Delete.DeleteUser(ObjectModel.Users.Id[_selectedUserRow]);
            if (status == 0)
                MessageBox.Show(@"Информация о выбранном пользователе успешно удалена", @"Sucsesful",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (status == -1)
                MessageBox.Show(@"Не удалось удалить информацию о выбранном пользователе", @"Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tabControl_Selected(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            _selectedTab = tabControl.SelectedIndex;
        }

        private void dataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.SelectedCells.Count != 0)
                _selectedUserRow = dataGridView.CurrentRow.Index;
        }

        private void listBoxTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null)
            {
                return;
            }
            if (listBox.SelectedItem.ToString().StartsWith("Тест. "))
            {
                //_selectedTestString = null;
                //_selectedInstructionString = null;
                //_selectedEducationTestString = listBox.SelectedItem.ToString().Replace("Тест. ", "");
                _selectedTestString = listBox.SelectedItem.ToString().Replace("Тест. ", "");
            }
            else
            {
                _selectedEducationTestString = null;
                //_selectedInstructionString = null;
                _selectedTestString = listBox.SelectedItem.ToString();
            }
        }

        private void radioButtonAnswer_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null)
            {
                return;
            }
            if (radioButton.Checked)
            {
                _selectedAnswerString.Add(radioButton.Text);
            }
            if (!radioButton.Checked && _selectedAnswerString.Contains(radioButton.Text))
            {
                _selectedAnswerString.Remove(radioButton.Text);
            }
        }

        private void checkBoxAnswer_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
            {
                return;
            }
            if (checkBox.CheckState == CheckState.Checked)
            {
                _selectedAnswerString.Add(checkBox.Text);
            }
            if (checkBox.CheckState == CheckState.Unchecked && _selectedAnswerString.Contains(checkBox.Text))
            {
                _selectedAnswerString.Remove(checkBox.Text);
            }
        }
    }
}
