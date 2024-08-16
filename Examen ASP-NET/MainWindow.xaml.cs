using Examen_ASP_NET.Domain;
using Examen_ASP_NET.Pages;
using NewsApp.Navigator;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Examen_ASP_NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext _context = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
            var user = new User() 
            {
                Id = 1,
                UserName = "Teacher",
                Password = "123",
                Role = "Teacher"
            };

            if(!_context.Users.Any(x => x.Id == user.Id))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            NavigatorObject.pageSwitcher = this;
            var homeMain = new LoginMain();
            NavigatorObject.Switch(homeMain);
        }

        public Action? CloseAction { get; set; }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            INavigator? s = nextPage as INavigator;
            if (s != null)
            {
                s.UtilizeStatr(state);
            }
            else
            {
                throw new ArgumentException("NextPage is not Navigator: " + nextPage.Name.ToString());
            }
        }
    }
}