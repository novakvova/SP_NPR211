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
            Dispatcher.Invoke(() => { pbStatus.Maximum = count; });
            
            for (int i = 0; i < count; i++)
            {
                Dispatcher.Invoke(() => { pbStatus.Value = i + 1; });
                
                Thread.Sleep(100);
            }

            Dispatcher.Invoke(() => { btnRun.IsEnabled = true; });
            
        }
    }
}