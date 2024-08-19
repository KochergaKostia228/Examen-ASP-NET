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

namespace Examen_ASP_NET.Pages.TeacherClient
{
    /// <summary>
    /// Логика взаимодействия для CreateAnswersMain.xaml
    /// </summary>
    public partial class CreateAnswersMain : UserControl
    {

        int questionId = 0;
        int testId = 0;
        int userId = 0;
        List<Answer> Answers = new List<Answer>();
        Answer SelectAnswer = new Answer();

        ApplicationContext _context = new ApplicationContext();

        public CreateAnswersMain(int testId, int questionId, int userId)
        {
            InitializeComponent();
            this.questionId = questionId;
            this.userId = userId;
            this.testId = testId;
            var answers = _context.Question.Include(x => x.Answers).First(x => x.Id == questionId);

            foreach (var answer in answers.Answers)
            {
                Answers.Add(answer);
            }

            LVAnswers.ItemsSource = Answers;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // Міняємо дизайн Label i TextBox
            txtText.BorderBrush = Brushes.Black;
            lblText.Visibility = Visibility.Collapsed;



            string text = txtText.Text;
            bool correct;
            if(cbCorrect.IsChecked == false)
            {
                correct = false;
            }
            else
            {
                correct = true;
            }

            if (txtText.Text == "")
            {

                txtText.BorderBrush = Brushes.Red;
                txtText.Text = "";
                lblText.Visibility = Visibility.Visible;
                lblText.Content = "Це поле пусте";

            }
            else
            {
                var answer = new Answer()
                {
                    Text = text,
                    IsCorrect = correct
                };
                var question = _context.Question.First(x => x.Id == questionId);
                question.Answers.Add(answer);
                _context.SaveChanges();
                NavigatorObject.Switch(new CreateAnswersMain(testId,questionId, userId));
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var select = LVAnswers.SelectedItem as Answer;
            if (select != null)
            {
                _context.Answers.Remove(select);
                _context.SaveChanges();
            }
            NavigatorObject.Switch(new CreateAnswersMain(testId, questionId, userId));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new CreateQuestionMain(testId, userId));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var select = LVAnswers.SelectedItem as Answer;
            if (select != null)
            {
                Background = Brushes.Azure;
                btCreate.Visibility = Visibility.Collapsed;
                btSave.Visibility = Visibility.Visible;
                tbText.Text = "Edit answer";
                txtText.Text = select.Text;
                cbCorrect.IsChecked = select.IsCorrect;

                SelectAnswer = select;
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Міняємо дизайн Label i TextBox
            txtText.BorderBrush = Brushes.Black;
            lblText.Visibility = Visibility.Collapsed;

            string text = txtText.Text;
            bool correct;
            if (cbCorrect.IsChecked == false)
            {
                correct = false;
            }
            else
            {
                correct = true;
            }

            if (txtText.Text == "")
            {
                txtText.BorderBrush = Brushes.Red;
                txtText.Text = "";
                lblText.Visibility = Visibility.Visible;
                lblText.Content = "Це поле пусте";
            }
            else
            {
                var model = _context.Answers.First(x => x.Id == SelectAnswer.Id);

                model.Text = text;
                model.IsCorrect = correct;
                _context.SaveChanges();
                NavigatorObject.Switch(new CreateAnswersMain(testId, questionId, userId));
            }
        }
    }
}
