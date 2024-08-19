using Examen_ASP_NET.Domain;
using Examen_ASP_NET.Pages.TeacherClient;
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
    /// Логика взаимодействия для PlayMain.xaml
    /// </summary>
    public partial class PlayMain : UserControl
    {
        int userId = 0;

        List<Test> Tests = new List<Test>();
        ApplicationContext _context = new ApplicationContext();
        public PlayMain(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            Tests = _context.Tests.ToList();
            LVTest.ItemsSource = Tests;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
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

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            var select = LVTest.SelectedItem as Test;
            if (select != null)
            {
                NavigatorObject.Switch(new TestMain(userId, select.Id, 0, 0, 0));
            }
        }
    }
}
