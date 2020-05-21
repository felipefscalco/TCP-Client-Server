using Common.Models;
using Newtonsoft.Json;
using Prism.Events;
using Server.Message;

namespace Server.Extensions
{
    public static class MessageExchangeExtensions
    {
        public static void CreateNewContact(this MessageExchange message, IEventAggregator eventAggregator)
        {
            var contact = JsonConvert.DeserializeObject<Contact>(message.Content);
            eventAggregator.GetEvent<CreateContactMessage>().Publish(contact);
        }
        public static void EditContact(this MessageExchange message, IEventAggregator eventAggregator)
        {
            var contact = JsonConvert.DeserializeObject<Contact>(message.Content);
            eventAggregator.GetEvent<EditContactMessage>().Publish(contact);
        }        
    }
}