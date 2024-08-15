using Examen_ASP_NET.Domain;
using Microsoft.EntityFrameworkCore;
using NewsApp.Navigator;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Логика взаимодействия для RegisterMain.xaml
    /// </summary>
    public partial class RegisterMain : UserControl
    {
        public RegisterMain()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Міняємо дизайн Label i TextBox
            txtUsername.BorderBrush = Brushes.Black;
            lblUsername.Visibility = Visibility.Hidden;

            txtPassword.BorderBrush = Brushes.Black;
            lblPassword.Visibility = Visibility.Hidden;

            txtConfirmPassword.BorderBrush = Brushes.Black;
            lblConfirmPassword.Visibility = Visibility.Hidden;
            
            
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            var user = new User() { UserName = username, Password = password, Role = "User" };

            if (password != txtConfirmPassword.Password)
            {
                txtPassword.BorderBrush = Brushes.Red;
                txtPassword.Password = "";
                lblPassword.Visibility = Visibility.Visible;
                lblPassword.Content = "Паролі не вспівпадають";

                txtConfirmPassword.BorderBrush = Brushes.Red;
                txtConfirmPassword.Password = "";
                lblConfirmPassword.Visibility = Visibility.Visible;
                lblConfirmPassword.Content = "Паролі не вспівпадають";


            }
            else if (txtPassword.Password == "" || txtUsername.Text == "" || txtConfirmPassword.Password == "")
            {
                if(txtPassword.Password == "")
                {
                    txtPassword.BorderBrush = Brushes.Red;
                    txtPassword.Password = "";
                    lblPassword.Visibility = Visibility.Visible;
                    lblPassword.Content = "Це поле пусте";
                }

                if(txtUsername.Text == "")
                {
                    txtUsername.BorderBrush = Brushes.Red;
                    txtUsername.Text = "";
                    lblUsername.Visibility = Visibility.Visible;
                    lblUsername.Content = "Це поле пусте";
                }

                if(txtConfirmPassword.Password == "")
                {
                    txtConfirmPassword.BorderBrush = Brushes.Red;
                    txtConfirmPassword.Password = "";
                    lblConfirmPassword.Visibility = Visibility.Visible;
                    lblConfirmPassword.Content = "Це поле пусте";
                }

            }
            else if(DoesUserExist(username, password))
            {
                txtUsername.BorderBrush = Brushes.Red;
                txtUsername.Text = "";
                lblUsername.Visibility = Visibility.Visible;
                lblUsername.Content = "Такий користувач вже існує";
            }
            else
            {                
                using (var context = new ApplicationContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    NavigatorObject.Switch(new LoginMain());
                }
            }
        }
    }
}
