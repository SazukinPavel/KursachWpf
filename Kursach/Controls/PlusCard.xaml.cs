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

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class PlusCard : UserControl
    {
        public delegate void onPlusClick();
        
        event onPlusClick onPlusClickEvent;

        public PlusCard(onPlusClick onPlusClick)
        {
            InitializeComponent();
            onPlusClickEvent += onPlusClick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            onPlusClickEvent();
        }
    }
}
