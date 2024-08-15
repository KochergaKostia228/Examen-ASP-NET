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
    // <summary>
    // Логика взаимодействия для LoginMain.xaml
    // </summary>
    public partial class LoginMain : UserControl
    {
        public LoginMain()
        {
            InitializeComponent();
        }

        public bool DoesUserExist(string username, string password)
        {
            using (var context = new ApplicationContext())
            {
                return context.Users.Any(x => x.UserName == username && x.Password == password);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Міняємо дизайн Label i TextBox
            txtUsername.BorderBrush = Brushes.Black;
            lblUsername.Visibility = Visibility.Hidden;

            txtPassword.BorderBrush = Brushes.Black;
            lblPassword.Visibility = Visibility.Hidden;


            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (txtPassword.Password == "" || txtUsername.Text == "")
            {
                if (txtPassword.Password == "")
                {
                    txtPassword.BorderBrush = Brushes.Red;
                    txtPassword.Password = "";
                    lblPassword.Visibility = Visibility.Visible;
                    lblPassword.Content = "Це поле пусте";
                }

                if (txtUsername.Text == "")
                {
                    txtUsername.BorderBrush = Brushes.Red;
                    txtUsername.Text = "";
                    lblUsername.Visibility = Visibility.Visible;
                    lblUsername.Content = "Це поле пусте";
                }
            }
            else if (!DoesUserExist(username, password))
            {
                txtUsername.BorderBrush = Brushes.Red;
                txtUsername.Text = "";
                lblUsername.Visibility = Visibility.Visible;
                lblUsername.Content = "Такого користувача не існує";
            }
            else
            {
                using(var context = new ApplicationContext())
                {
                    var user = new User();
                    user = context.Users.First(x => x.UserName == username && x.Password == password);

                    if (user.Role == "User")
                    {
                        NavigatorObject.Switch(new UserHomeMain(user.Id));
                    }
                    else if (user.Role == "Teacher")
                    {
                        NavigatorObject.Switch(new TeacherHomeMain(user.Id));

                    }
                    
                }
                
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var homeMain = new RegisterMain();
            NavigatorObject.Switch(homeMain);
        }
    }
}
