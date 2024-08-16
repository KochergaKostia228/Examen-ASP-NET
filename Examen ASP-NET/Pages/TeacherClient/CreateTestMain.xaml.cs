using Examen_ASP_NET.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
    /// Логика взаимодействия для CreateTestMain.xaml
    /// </summary>
    public partial class CreateTestMain : UserControl
    {


        int userId = 0;

        List<Test> Tests = new List<Test>();
        ApplicationContext _context = new ApplicationContext();
        public CreateTestMain(int userId)
        {
            InitializeComponent();
            Tests = _context.Tests.ToList();
            LVTest.ItemsSource = Tests;
            this.userId = userId;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // Міняємо дизайн Label i TextBox
            txtTitle.BorderBrush = Brushes.Black;
            lblTitle.Visibility = Visibility.Collapsed;

            txtDescription.BorderBrush = Brushes.Black;
            lblDescription.Visibility = Visibility.Collapsed;


            string title = txtTitle.Text;
            string description = txtDescription.Text;

            if (txtTitle.Text == "" || txtDescription.Text == "")
            {
                if (txtTitle.Text == "")
                {
                    txtTitle.BorderBrush = Brushes.Red;
                    txtTitle.Text = "";
                    lblTitle.Visibility = Visibility.Visible;
                    lblTitle.Content = "Це поле пусте";
                }

                if (txtDescription.Text == "")
                {
                    txtDescription.BorderBrush = Brushes.Red;
                    txtDescription.Text = "";
                    lblDescription.Visibility = Visibility.Visible;
                    lblDescription.Content = "Це поле пусте";
                }
            }
            else
            {
                var test = new Test()
                {
                    Title = title,
                    Description = description,
                };
                _context.Tests.Add(test);
                _context.SaveChanges();
                NavigatorObject.Switch(new CreateTestMain(userId));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var select = LVTest.SelectedItem as Test;
            var test = _context.Tests.Include(x => x.Questions).First(x => x.Id == select.Id);

            var user = _context.Users.Include(x => x.Results).First(x => x.Id == userId);

            if (select != null)
            {
                
                if (test.Questions != null)
                {
                    foreach (var rew in test.Questions.ToList())
                    {
                        var question = _context.Question.Include(x => x.Answers).First(x => x.Id == rew.Id);

                        if (question.Answers != null)
                        {
                            foreach (var answer in question.Answers.ToList())
                            {
                                _context.Answers.Remove(answer);
                            }
                        }
                        _context.Question.Remove(rew);
                    }
                }

                if(user.Results != null)
                {
                    foreach (var answer in user.Results.ToList())
                    {
                        _context.Results.Remove(answer);
                    }
                }
                
                _context.Remove(test);
                _context.SaveChanges();
            }
            NavigatorObject.Switch(new CreateTestMain(userId));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var select = LVTest.SelectedItem as Test;
            if (select != null)
            {
                NavigatorObject.Switch(new CreateQuestionMain(select.Id, userId));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new TeacherHomeMain(userId));
        }
    }
}
