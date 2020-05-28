using Prism.Events;

namespace Server.Message
{
    public class SearchContactsByNameMessage : PubSubEvent<string>
    {
    }
}