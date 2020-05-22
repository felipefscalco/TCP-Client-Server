using Prism.Events;
using System;

namespace Client.Messages
{
    public class DeleteContactMessage : PubSubEvent<Guid>
    {
    }
}