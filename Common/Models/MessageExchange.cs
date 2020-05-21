using Common.Enums;
using System;

namespace Common.Models
{
    public class MessageExchange
    {
        public ActionType Action { get; set; }
        public string Content { get; set; }

        public MessageExchange(ActionType action, string content = null)
        {
            Action = action;
            Content = content;
        }
    }
}