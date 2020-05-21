using Common.Models;
using Prism.Events;

namespace Client.Messages
{
    public class CreateContactMessage : PubSubEvent<Contact>
    {
    }
}