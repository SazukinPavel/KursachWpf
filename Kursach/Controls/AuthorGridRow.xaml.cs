using Kursach.Models;
using Kursach.Types;
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
using static Kursach.Types.AuthorGridRowProps;

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для AuthorGridRow.xaml
    /// </summary>
    public partial class AuthorGridRow : UserControl
    {

        event DeleteClick onDelete;

        User User { get; set; }
     
        public AuthorGridRow(AuthorGridRowProps authorGridRowProps)
        {
            InitializeComponent();
            SetProps(authorGridRowProps);
        }

        void SetProps(AuthorGridRowProps authorGridRowProps)
        {
            User = authorGridRowProps.Author;
            author.Text = User.name;
            deleteButton.Visibility=authorGridRowProps.IsButtonHide?Visibility.Hidden:Visibility.Visible;
            onDelete += authorGridRowProps.deleteClick;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            onDelete(User.id);
        }
    }
}
