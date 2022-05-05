using Kursach.Types;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        ConfirmWindowProps ConfirmWindowProps { get; set; }
        public ConfirmWindow(ConfirmWindowProps confirmWindowProps)
        {
            InitializeComponent();
            this.ConfirmWindowProps = confirmWindowProps;
            RenderConfirmWindow();
        }

        public void RenderConfirmWindow()
        {
            message.Text = ConfirmWindowProps.ConfirmMessage;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
