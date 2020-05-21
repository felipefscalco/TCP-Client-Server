using Prism.Events;

namespace Server.Message
{
    public class SendMessageToClientMessage : PubSubEvent<string>
    {
    }
}