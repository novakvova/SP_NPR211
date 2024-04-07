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
        //маємо можливість блокувати роботу потоку.
        //
        private static ManualResetEvent _manualEvent = new ManualResetEvent(false); // Initialize as unsignaled
        private bool _isPause = false;
        
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

            thread.Start(); //стартуємо вториний потік, який додає користувачів в БД
            //Запускаємо, але потік поки не блокуємо.
            _manualEvent.Set();
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
            _manualEvent.WaitOne(Timeout.Infinite); //якщо потік заблоковано, то очікуємо
            //поки його буде розблоковано
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if(_isPause)
            {
                _manualEvent.Set(); // розблоковуємо потік
                btnPause.Content = "Призупинити";
            }
            else
            {
                _manualEvent.Reset(); //лочимо потік
                btnPause.Content = "Продовжити";
            }

            _isPause = !_isPause;
        }
    }
}