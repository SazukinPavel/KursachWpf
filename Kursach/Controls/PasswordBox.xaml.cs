using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для PasswordBox.xaml
    /// </summary>
    public partial class PasswordBox : UserControl
    {
        public PasswordBox()
        {
            InitializeComponent();
        }

        public string GetPassword()
        {
            return ShowPasswordCharsCheckBox.IsChecked.Value ? MyTextBox.Text:MyPasswordBox.Password;
        }

        private void ShowPasswordCharsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MyPasswordBox.Visibility = Visibility.Collapsed; 
            MyTextBox.Visibility = Visibility.Visible; 
            MyTextBox.Text = new NetworkCredential(string.Empty, MyPasswordBox.SecurePassword).Password; 
            MyTextBox.Focus();
        }

        private void ShowPasswordCharsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MyPasswordBox.Visibility = Visibility.Visible; 
            MyTextBox.Visibility = Visibility.Collapsed; 
            MyTextBox.Text = ""; 
            MyPasswordBox.Focus();
        }
    }
}
