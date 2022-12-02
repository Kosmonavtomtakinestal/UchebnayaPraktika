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
using UchebnayaPraktika.Components;
using UchebnayaPraktika.Pages;
using UchebnayaPraktika;

namespace UchebnayaPraktika.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        int countAuth = Properties.Settings.Default.CountAuth;
        public AuthPage()
        {
            InitializeComponent();
            LoginTB.Text = Properties.Settings.Default.Login;
            PasswordTB.Password = Properties.Settings.Default.Password;

        }

        private void LogBTN_Click(object sender, RoutedEventArgs e)
        {
            if (countAuth < 3)
            {
                if (LoginTB.Text != "" || PasswordTB.Password != "")
                {
                    var inUser = DbConnection.db.User.ToList().Find(x => x.Login == LoginTB.Text.Trim() && x.Password == PasswordTB.Password.Trim());
                    if (inUser != null)
                    {
                        MessageBox.Show("Все удачно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        countAuth += 1;
                        Properties.Settings.Default.CountAuth = countAuth;
                        MessageBox.Show("Такого пользователя нет", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Не заполнены поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Вы ввели 3 раза неправильный пароль\nВход заблокировани на 1 минуту", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                countAuth = 0;
            }
            
        }

        private void SaveDataCB_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Login = LoginTB.Text.Trim();
            Properties.Settings.Default.Password = PasswordTB.Password.Trim();
            Properties.Settings.Default.Save();
        }

        private void RegBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Регистрация", new RegistrPage()));
        }
    }
}
