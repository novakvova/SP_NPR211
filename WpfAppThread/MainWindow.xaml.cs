using Infrastraction.Services;
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

namespace WpfAppThread
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

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Add items " + txtCount.Text);
            btnRun.IsEnabled = false;
            double count = double.Parse(txtCount.Text);
            Thread thread = new Thread(() =>
                InsertItems(count));

            thread.Start();
        }

        private void InsertItems(double count)
        {
            UserService userService = new UserService();
            userService.InsertUserEvent += UserService_InsertUserEvent;

            Dispatcher.Invoke(() => { pbStatus.Maximum = count; });
            userService.InsertRandomUser((int)count);


            Dispatcher.Invoke(() => { btnRun.IsEnabled = true; });
            
        }

        private void UserService_InsertUserEvent(int count)
        {
            Dispatcher.Invoke(() => { pbStatus.Value = count; });
        }
    }
}