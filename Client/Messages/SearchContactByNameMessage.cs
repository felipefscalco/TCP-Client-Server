using Prism.Events;

namespace Client.Messages
{
    public class SearchContactByNameMessage : PubSubEvent<string>
    {
    }
}