using Common.Models;
using Prism.Events;
using System.Collections.Generic;

namespace Client.Messages
{
    public class UpdateContactsMessage : PubSubEvent<List<Contact>>
    {
    }
}