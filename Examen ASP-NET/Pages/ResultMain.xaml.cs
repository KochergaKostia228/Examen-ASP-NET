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
    /// Логика взаимодействия для ResultMain.xaml
    /// </summary>
    public partial class ResultMain : UserControl
    {

        List<Result> Results = new List<Result>();
        ApplicationContext _context = new ApplicationContext();
        int userId = 0;
        public ResultMain(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            var user = _context.Users.First(X => X.Id == userId);
            var results = _context.Users.Include(x => x.Results).First(X => X.Id == userId);

            if (user.Role == "User")
            {
                foreach (var res in results.Results.ToList())
                {
                    var result = _context.Results.Include(x => x.Test).Include(x => x.User).First(X => X.Id == res.Id);

                    if (result.Test != null && result.User != null)
                    {
                        Results.Add(result);
                    }
                }
            }
            else if (user.Role == "Teacher")
            {
                foreach (var res in _context.Results.ToList())
                {
                    var result = _context.Results.Include(x => x.Test).Include(x => x.User).First(X => X.Id == res.Id);

                    if(result.Test != null && result.User != null)
                    {
                        Results.Add(result);
                    }
                }
            }

            

            LVResult.ItemsSource = Results;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var user = _context.Users.First(X => X.Id == userId);
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
