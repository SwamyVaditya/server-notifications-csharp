using System.Collections.Generic;
using System;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio;

namespace ServerNotifications.Web.Domain.Twilio
{
    public interface IRestClient
    {
        MessageResource SendMessage(string from, string to, string body, List<Uri> mediaUrl);
    }

    public class RestClient : IRestClient
    {   
        public RestClient()
        {
            if (Credentials.TwilioAccountSid != null && Credentials.TwilioAuthToken != null)
            {
                TwilioClient.Init(Credentials.TwilioAccountSid, Credentials.TwilioAuthToken);
            }
        }

        public MessageResource SendMessage(string from, string to, string body, List<Uri> mediaUrl)
        {
            return MessageResource.Create(
                from: new PhoneNumber(from),
                to: new PhoneNumber(to),
                body: body,
                mediaUrl: mediaUrl
                );
        }
    }
}