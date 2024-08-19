using Examen_ASP_NET.Domain;
using Microsoft.EntityFrameworkCore;
using NewsApp.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Examen_ASP_NET.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestMain.xaml
    /// </summary>
    public partial class TestMain : UserControl
    {
        List<Question> Questions = new List<Question>();
        List<Answer> Answers = new List<Answer>();
        ApplicationContext _context = new ApplicationContext();

        int userId = 0;
        int testId = 0;
        int numberQuestion = 0;
        int countCorrect = 0;
        double second = 0;
        DateTime startTimeQuestion = DateTime.Now;
        public TestMain(int userId, int testId, int numberQuestion, int countCorrect, double second)
        {
            InitializeComponent();
            var questions = _context.Tests.Include(x => x.Questions).First(x => x.Id == testId);
            this.testId = testId;
            this.userId = userId;
            this.numberQuestion = numberQuestion;
            this.countCorrect = countCorrect;
            this.second = second;

            foreach (var question in questions.Questions)
            {
                Questions.Add(question);
            }


            if (Questions.Count > numberQuestion)
            {
                var answers = _context.Question.Include(x => x.Answers).First(x => x.Id == Questions[numberQuestion].Id);

                foreach (var answer in answers.Answers)
                {
                    Answers.Add(answer);
                }

                tbTextQuestion.Text = Questions[numberQuestion].Text;
                tbTest.Text = $"Test {numberQuestion + 1}/{Questions.Count}";

                LVAnswer.ItemsSource = Answers;

                //if (UrlValidator.IsValidUrl(Questions[numberQuestion].Text))
                //{
                //    //ImageQuestion.Source = new BitmapImage(new Uri(Questions[numberQuestion].Image));
                //}
            }
            else
            {
                btnNext.Content = "Показать результат теста";
            }

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            var select = LVAnswer.SelectedItem as Answer;
            DateTime endTimeQuestion = DateTime.Now;
            TimeSpan duration = endTimeQuestion - startTimeQuestion;

            second += duration.TotalSeconds;

            if (Questions.Count <= numberQuestion)
            {
                NavigatorObject.Switch(new TestResultMain(userId, testId, countCorrect, second));
            }
            else if (select != null)
            {
                if (select.IsCorrect == true)
                {
                    NavigatorObject.Switch(new TestMain(userId, testId, numberQuestion + 1, countCorrect + 1, second));
                }
                else if (select.IsCorrect == false)
                {
                    NavigatorObject.Switch(new TestMain(userId, testId, numberQuestion + 1, countCorrect, second));
                }
            }

        }
    }
}
