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
using WpfApp6.DB;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            DB.MyContext myContext = new();

            var flockUser = myContext.Users.Any(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);
            var flockWorker = myContext.Workers.Any(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);

            if ( flockUser == true)
            {
                int User = myContext.Users.Single(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password).UserId;
                UserWindow userWindow = new(User.UserId);
                    userWindow.Show();
                Close();
            }
            if (flockWorker == true)
            {
                var Worker = myContext.Workers.Single(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);

                switch (Worker.job)
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
        }

        private void btShowPass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
