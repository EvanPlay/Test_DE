using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApp6.DB;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int CapchaHide = 0;

        Capcha capcha = null;
        public List<Capcha> capchas = new List<Capcha>
        {
            new Capcha{IsTry="D19", path = "/CapchaImage/D19.png" }
        };

        public MainWindow()
        {
            InitializeComponent();
            SpCapcha.Visibility = Visibility.Hidden;
        }


        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            DB.MyContext myContext = new();

            if (tbLogin.Text.Length > 0)
            { 
                if (pbPassword.Password.Length >0) 
                {
                    var flockUser = myContext.Users.Any(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);
                    var flockWorker = myContext.Workers.Any(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);

                    if ( flockUser == true)
                    {
                        int UserId = myContext.Users.Single(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password).UserId;
                        UserWindow userWindow = new(UserId);
                            userWindow.Show();
                     Close();
                    }
                    if (flockWorker == true)
                    {
                        var Worker = myContext.Workers.Single(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);

                        switch (Worker.job)//Изменить "1", "2", "3" под актуальные должности
                        {
                            case "1":
                                MessageBox.Show($"Welcome {Worker.job} {Worker.WorkerName}");
                                SellerWindow sellerWindow = new(Worker.WorkerId);
                                sellerWindow.Show();
                                Close();
                                break;
                            case "2":
                                MessageBox.Show($"Welcome {Worker.job} {Worker.WorkerName}");
                                BossWindow bossWindow = new(Worker.WorkerId);
                                bossWindow.Show();
                                Close();
                                break;
                            case "3":
                                MessageBox.Show($"Welcome {Worker.job} {Worker.WorkerName}");
                                AdministratorWindow administratorWindow = new(Worker.WorkerId);
                                administratorWindow.Show();
                                Close();
                                break;
                        }
                    }
                    else
                    {
                    MessageBox.Show("Логин или пароль не верен.");
                        CapchaHide++;
                    }
                    if (CapchaHide == 2)
                    {
                        MessageBox.Show("Логин или пароль не верен.");
                        SpCapcha.Visibility = Visibility.Visible;
                        RandomCapcha();
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите имя пользовтеля");
            }
        }

        private void RandomCapcha()
        {
            Random random = new();
            var newcapcha = capchas[random.Next(capchas.Count)];

            if (capcha==null)
            {
                capcha = newcapcha;
                imageCapcha.Source = new BitmapImage(new Uri(capcha.path, UriKind.Relative));
                return;
            }
            if (capcha.IsTry != newcapcha.IsTry)
            {
                capcha = newcapcha;
                imageCapcha.Source = new BitmapImage(new Uri(capcha.path, UriKind.Relative));
                return;
            }
            if (capcha.IsTry == newcapcha.IsTry)
            {
                RandomCapcha();
            }
        }

        private void btShowPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pbPassword.Password);
        }

        private void btCapchaGo_Click(object sender, RoutedEventArgs e)
        {
            if (tbCapcha.Text != capcha.IsTry)
            {
                MessageBox.Show("Введите капчу");
                for (int i =10; i > 0; i--)
                {
                    this.Title = $"Блокировка на {i} секунд";
                    Thread.Sleep(1000);
                }
                RandomCapcha();
                tbCapcha.Clear();
                Title = "Выход";
                return;
            }
        }

        private void btRecreateCapcha_Click(object sender, RoutedEventArgs e)
        {
            RandomCapcha();
        }

        public class Capcha
        {
            public string path { get; set; }
            public string IsTry { get; set; }
        }
    }
}
