using Common.Enums;

namespace Common.Models
{
    public class MessageExchange
    {
        public ActionType Action { get; set; }
        public string Content { get; set; }

        public MessageExchange(ActionType action, string content)
        {
            Action = action;
            Content = content;
        }
    }
}