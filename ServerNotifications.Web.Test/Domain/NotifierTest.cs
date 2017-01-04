using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ServerNotifications.Web.Domain;
using ServerNotifications.Web.Models;
using ServerNotifications.Web.Models.Repository;
using Twilio.Http;
using Twilio;
using Twilio.Clients;

namespace ServerNotifications.Web.Test.Domain
{
    public class NotifierTest
    {
        [Test]
        public void SendMessages_sends_a_message_to_each_administrator()
        {
            var mockRepository = new Mock<IAdministratorsRepository>();
            mockRepository.Setup(x => x.All())
                .Returns(new List<Administrator>
                {
                    new Administrator {Name = "Bob"},
                    new Administrator {Name = "Alice"}
                });

            var twilioClientMock = new Mock<ITwilioRestClient>();
            twilioClientMock.Setup(c => c.AccountSid).Returns("AccountSID");
            twilioClientMock.Setup(c => c.Request(It.IsAny<Request>()))
                            .Returns(new Response(System.Net.HttpStatusCode.Created, ""));

            TwilioClient.SetRestClient(twilioClientMock.Object);
            new Notifier(mockRepository.Object).SendMessages("some error message");

            twilioClientMock.Verify(
                c => c.Request(It.IsAny<Request>()), Times.Exactly(2));            
        }
    }
}
