using Kursach.Types;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        MessageWindowProps MessageWindowProps { get; set; }

        public MessageWindow(MessageWindowProps messageWindowProps)
        {
            InitializeComponent();
            MessageWindowProps = messageWindowProps;
            RenderMessageWindow();
        }

        void RenderMessageWindow()
        {
            message.Text = MessageWindowProps?.Mesasge;
            header.Text = MessageWindowProps?.Header;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
