using Kursach.Models;
using Kursach.Types;
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
        public TaskCard(TaskCardProps taskCardProps)
        {
            InitializeComponent();
            SetProps(taskCardProps);
            RenderMission();
        }

        void SetProps(TaskCardProps taskCardProps)
        {
            Task = taskCardProps.Task;
            button.Content = taskCardProps.ButtonMessage;
            button.Click += (object sender, RoutedEventArgs e) =>
            {
                taskCardProps.buttonClick(Task);
            };
        }

        void RenderMission()
        {
            name.Text = Task.title;
            description.Text = Task.description;
            course.Text = "Курс:" + Task.course.name;
        }
    }
}
