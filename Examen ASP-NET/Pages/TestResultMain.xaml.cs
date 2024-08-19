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

namespace Examen_ASP_NET.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestResultMain.xaml
    /// </summary>
    public partial class TestResultMain : UserControl
    {

        List<Question> Questions = new List<Question>();
        ApplicationContext _context = new ApplicationContext();

        int userId = 0;
        int testId = 0;
        int countCorrect = 0;
        float score = 0;
        double second = 0;
        TimeSpan timeSpan = new TimeSpan();
        public TestResultMain(int userId, int testId, int countCorrect, double second)
        {
            InitializeComponent();
            var questions = _context.Tests.Include(x => x.Questions).First(x => x.Id == testId);

            foreach (var question in questions.Questions)
            {
                Questions.Add(question);
            }

            this.userId = userId;
            this.testId = testId;
            this.countCorrect = countCorrect;
            this.score = (float)countCorrect / Questions.Count * 12;
            this.second = second;
            timeSpan = TimeSpan.FromSeconds(second);

            tbTrueQuestion.Text = $"Питань пройшов вірно: {countCorrect}";
            tbFalseQuestion.Text = $"Питань пройшов не вірно: {Questions.Count - countCorrect}";
            tbScore.Text = $"Ваша оцінка: {(int)this.score}";
            tbSecond.Text = $"Ви пройшли тест за: {(int)second / 60} хвилин та {(int)second % 60} секунд";

            var user = _context.Users.First(x => x.Id == userId);

            var result = new Result()
            {
                Test = _context.Tests.First(x => x.Id == testId),
                Score = (int)score,
                StartTime = DateTime.Now - timeSpan,
                Second = (int)second,
            };

            user.Results.Add(result);
            _context.SaveChanges();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = _context.Users.First(x => x.Id == userId);

            if (user.Role == "User")
            {
                NavigatorObject.Switch(new UserHomeMain(userId));
            }
            else if (user.Role == "Teacher")
            {
                NavigatorObject.Switch(new TeacherHomeMain(userId));
            }
        }
    }
}
