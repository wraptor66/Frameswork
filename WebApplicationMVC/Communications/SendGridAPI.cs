using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Reflection;
using JsonMapping.JsonFieldFinder;
using Newtonsoft.Json.Linq;

namespace Communications
{
    public class SendGridAPI
    {
        readonly string APIKey = string.Empty;
        public SendGridAPI()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build();
            APIKey = configuration.GetValue<string>("SendGrid_APIKey");
        }

        public dynamic SendEmail(string jObject)
        {
            try
            {
                JObject jobject = JObject.Parse(jObject);
                /// the line below creates every permutation of wildcards for parsing json attributes 2 levels deep
                var permutations = JsonFieldFinder.GetJPaths(2);

                var apiKey = APIKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("mitch@fundamental.technology", "Mitch Maynard");
                var subject = JsonFieldFinder.FindFieldValue("title", jobject, permutations).ToString(); ;
                var to = new EmailAddress(JsonFieldFinder.FindFieldValue("email", jobject, permutations).ToString());
                var plainTextContent = "Please view in HTML Viewer";
                var htmlContent = JsonFieldFinder.FindFieldValue("message", jobject, permutations).ToString();
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var result = client.SendEmailAsync(msg).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
