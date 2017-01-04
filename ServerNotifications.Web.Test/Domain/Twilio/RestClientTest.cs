using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Clients;
using Twilio.Http;

namespace ServerNotifications.Web.Test.Domain.Twilio
{
    public class RestClientTest
    {
        [Test]
        public void SendMessage()
        {
            var twilioClientMock = new Mock<ITwilioRestClient>();
            twilioClientMock.Setup(c => c.AccountSid).Returns("AccountSID");
            twilioClientMock.Setup(c => c.Request(It.IsAny<Request>()))
                            .Returns(new Response(System.Net.HttpStatusCode.Created, ""));

            const string body = "Alert message";
            List<Uri> mediaUrl = new List<Uri> { new Uri("http://example.com/image") };
            var client = new Web.Domain.Twilio.RestClient();
            TwilioClient.SetRestClient(twilioClientMock.Object);
            client.SendMessage("from", "to", body, mediaUrl);

            twilioClientMock.Verify(
                c => c.Request(It.IsAny<Request>()), Times.Once);
        }
    }
}
