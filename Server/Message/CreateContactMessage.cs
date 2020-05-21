using Common.Models;
using Prism.Events;

namespace Server.Message
{
    public class CreateContactMessage : PubSubEvent<Contact>
    {
    }
}