using Kursach.Models;
using System.Windows;
using System.Windows.Controls;

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для MissionCard.xaml
    /// </summary>
    public partial class TaskCard : UserControl
    {
        private Task Task { get; set; }
        public TaskCard(Task task)
        {
            InitializeComponent();
            Task = task;
            RenderMission();
        }

        void RenderMission()
        {
            name.Text = Task.title;
            description.Text = Task.description;
            course.Text = "Курс:"+Task.course.name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
