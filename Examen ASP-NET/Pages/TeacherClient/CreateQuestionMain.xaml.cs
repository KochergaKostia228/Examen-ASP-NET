using Examen_ASP_NET.Domain;
using Microsoft.EntityFrameworkCore;
using NewsApp.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
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
using static System.Net.Mime.MediaTypeNames;

namespace Examen_ASP_NET.Pages.TeacherClient
{
    /// <summary>
    /// Логика взаимодействия для CreateQuestionMain.xaml
    /// </summary>
    public partial class CreateQuestionMain : UserControl
    {


        int testId = 0;
        int userId = 0;
        List<Question> Questions = new List<Question>();
        ApplicationContext _context = new ApplicationContext();
        public CreateQuestionMain(int testId, int userId)
        {
            InitializeComponent();
            this.testId = testId;
            var questions = _context.Tests.Include(x => x.Questions).First(x => x.Id == testId);

            foreach(var question in questions.Questions) {
                Questions.Add(question);
            }

            LVQuestions.ItemsSource = Questions;
            this.userId = userId;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // Міняємо дизайн Label i TextBox
            txtText.BorderBrush = Brushes.Black;
            lblText.Visibility = Visibility.Collapsed;

            txtImage.BorderBrush = Brushes.Black;


            string text = txtText.Text;
            string image = txtImage.Text;

            if (txtText.Text == "")
            {

                txtText.BorderBrush = Brushes.Red;
                txtText.Text = "";
                txtImage.Text = "";
                lblText.Visibility = Visibility.Visible;
                lblText.Content = "Це поле пусте";

            }
            else
            {
                var test = _context.Tests.First(x => x.Id == testId);
                var question = new Question();
                
                question.Text = text;
                question.Image = image;

                test.Questions.Add(question);
                _context.SaveChanges();
                NavigatorObject.Switch(new CreateQuestionMain(testId, userId));
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            var select = LVQuestions.SelectedItem as Question;
            var question = _context.Question.Include(x=>x.Answers).First(x => x.Id == select.Id);

            if (select != null)
            {

                
                if (question.Answers != null)
                {
                    foreach (var answer in question.Answers.ToList())
                    {
                        question.Answers.Remove(answer);
                    }
                }
                       
                _context.Remove(question);
                _context.SaveChanges();
            }
            NavigatorObject.Switch(new CreateQuestionMain(testId, userId));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new CreateTestMain(userId));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var select = LVQuestions.SelectedItem as Question;
            if (select != null)
            {
                NavigatorObject.Switch(new CreateAnswersMain(testId, select.Id, userId));
            }
        }
    }
}
