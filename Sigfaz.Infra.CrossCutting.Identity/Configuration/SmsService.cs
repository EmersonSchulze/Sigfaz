using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Sigfaz.Infra.CrossCutting.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Utilizando TWILIO como SMS Provider.
            // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

            const string accountSid = "ACd5246167d5e5307aa0ae8a2b1940753b";
            const string authToken = "7d457599a6d9722a7d6f430ffe247410";

            TwilioClient.Init(accountSid, authToken);

            var messagem = MessageResource.Create("44991422256", message.Destination, message.Body);



            return Task.FromResult(0);
        }
    }
}