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

        public static MessageWindow CreateMessageWindow(string header)
        {
            return new MessageWindow(new MessageWindowProps(header));
        }

        public static MessageWindow CreateMessageWindow(string header,string message)
        {
            return new MessageWindow(new MessageWindowProps(header,message));
        }

        public static ConfirmWindow CreateConfirmWindow(string message)
        {
            return new ConfirmWindow(new ConfirmWindowProps(message));
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

        public static AddSolutionWindow CreateAddSolutionWindow(Task task)
        {
            return new AddSolutionWindow(task);
        }

        public static EditSolutionWindow CreateEditSolutionWindow(Task task)
        {
            return new EditSolutionWindow(task);
        }
        public static AddReviewWindow CreateAddReviewWindow(Solution solution)
        {
            return new AddReviewWindow(solution);
        }
        public static ReadReviewWindow CreateReadReviewWindow(Review review)
        {
            return new ReadReviewWindow(review);
        }
        public static EditReviewWindow CreateEditSolutionWindow(AuthorSolution solution)
        {
            return new EditReviewWindow(solution);
        }
    }
}
