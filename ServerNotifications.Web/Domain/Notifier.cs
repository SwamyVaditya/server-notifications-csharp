using ServerNotifications.Web.Domain.Twilio;
using ServerNotifications.Web.Models.Repository;
using System;
using System.Collections.Generic;
using WebGrease.Css.Extensions;

namespace ServerNotifications.Web.Domain
{
    public class Notifier
    {
        private Uri ImageUrl = new Uri("http://howtodocs.s3.amazonaws.com/new-relic-monitor.png");

        private readonly IAdministratorsRepository _administratorsRepository;
        private readonly IRestClient _restClient;

        public Notifier(IAdministratorsRepository repository)
        {
            _administratorsRepository = repository;
            _restClient = new RestClient();
        }
        
        public void SendMessages(string message)
        {
            List<Uri> mediaUrl = new List<Uri> { ImageUrl };
            _administratorsRepository.All().ForEach(administrator =>
                _restClient.SendMessage(Credentials.TwilioPhoneNumber, administrator.PhoneNumber, message, mediaUrl));
        }
    }
}