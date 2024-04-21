using Domain.Data;
using Infrastraction.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppThread
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private ObservableCollection<UserViewModel> _users = new ();

        public UsersWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello Peter");
            MyDataContext myDataContext = new MyDataContext();

            var model = myDataContext.Users
                .Select(x=> new UserViewModel
                {
                    Email = x.Email,
                    Name = x.LastName+ " "+ x.FistName,
                    Phone = x.PhoneNumber,
                })
                .ToList();
            _users = new(model);
            dgSimple.ItemsSource = _users;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _users.Add(new UserViewModel
            {
                Name="Test",
                Phone="+380 98 89 899",
                Email="email@test.com",
            });
        }
    }
}
