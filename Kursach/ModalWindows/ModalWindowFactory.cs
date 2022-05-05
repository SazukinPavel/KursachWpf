using Kursach.Models;
using Kursach.Types;
using System.Collections.Generic;

namespace Kursach.ModalWindows
{
    public class ModalWindowFactory
    {

        public static MessageWindow CreateMessageWindow(MessageWindowProps messageWindowProps)
        {
            return new MessageWindow(messageWindowProps);
        }
        
        public static ConfirmWindow CreateConfirmWindow(ConfirmWindowProps confirmWindowProps)
        {
            return new ConfirmWindow(confirmWindowProps);
        }

        public static ChooseAuthorWindow CreateChooseAuthorWindow(List<User> authors)
        {
            return new ChooseAuthorWindow(authors);
        }

        public static EditTaskWindow CreateEditTaskWindow(Task task)
        {
            return new EditTaskWindow(task);
        }
    }
}
