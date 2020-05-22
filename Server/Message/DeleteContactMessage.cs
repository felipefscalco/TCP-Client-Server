using Prism.Events;
using System;

namespace Server.Message
{
    public class DeleteContactMessage : PubSubEvent<Guid>
    {
    }
}