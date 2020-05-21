using Common.Models;
using Prism.Events;

namespace Common.Messages
{
    public class ClientCreateContactMessage : PubSubEvent<Contact>
    {
    }
}