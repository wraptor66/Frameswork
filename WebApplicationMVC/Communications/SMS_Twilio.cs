using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Reflection;


namespace Communications
{
    public class SMS_Twilio
    {
        readonly string SID = string.Empty;
        readonly string Token = string.Empty;
        public SMS_Twilio()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build();
            SID = configuration.GetValue<string>("Twilio_AccountSid");
            Token = configuration.GetValue<string>("Twilio_AuthToken");
        }
        public void SendSMS(string phoneNumberTo, string messageContent)
        {
            try
            {
                var accountSid = SID;
                var authToken = Token;
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber($"+1{phoneNumberTo}"));
                messageOptions.From = new PhoneNumber("+18559213500");
                messageOptions.Body = $"{messageContent}";
                var message = MessageResource.Create(messageOptions);
            }
            catch (Exception ex)
            {            
                throw;
            }
        }
    }
}
