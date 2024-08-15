using Examen_ASP_NET.Domain;
using Examen_ASP_NET.Pages.TeacherClient;
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

namespace Examen_ASP_NET.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomeMain.xaml
    /// </summary>
    public partial class TeacherHomeMain : UserControl
    {
        User user = new User();
        ApplicationContext _context = new ApplicationContext();
        public TeacherHomeMain(int userId)
        {
            InitializeComponent();
            user = _context.Users.First(x => x.Id == userId);
            tbUser.Text = user.UserName;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new CreateTestMain(user.Id));
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new PlayMain(user.Id));
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new ResultMain(user.Id));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new LoginMain());
        }
    }
}
