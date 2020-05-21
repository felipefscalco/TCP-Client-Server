using Common.Models;
using Prism.Events;

namespace Server.Message
{
    public class EditContactMessage : PubSubEvent<Contact>
    {
    }
}