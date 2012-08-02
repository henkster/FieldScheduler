using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Services.Infrastructure
{
    public class Emailer
    {
        private readonly IConfigurationFinder _configurationFinder;

        public Emailer(IConfigurationFinder configurationFinder)
        {
            _configurationFinder = configurationFinder;
        }

        public void Send(string to, string subject, string htmlTemplate = null, string textTemplate = null, params object[] args)
        {
            Send(new List<string>{to}, subject, htmlTemplate, textTemplate, args);
        }

        public void Send(List<string> toList, string subject, string htmlTemplate = null, string textTemplate = null, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(htmlTemplate) && string.IsNullOrWhiteSpace(textTemplate))
            {
                throw new ArgumentException("Params 'htmlTemplate' and 'textTemplate' cannot both be null/empty");
            }

            //var client = new SmtpClient(_configurationFinder.Find("mail-host"), int.Parse(_configurationFinder.Find("mail-port")));
            var client = new SmtpClient(_configurationFinder.Find("mail-host"));
            client.Credentials = new NetworkCredential(_configurationFinder.Find("mail-user"),
                                                       _configurationFinder.Find("mail-password"));
            client.UseDefaultCredentials = false;
            //client.EnableSsl = true;
            
            foreach (string to in toList)
            {
                var message = new MailMessage(_configurationFinder.Find("mail-from"), to);

                message.Subject = subject;

                if (!string.IsNullOrWhiteSpace(textTemplate))
                {
                    message.AlternateViews.Add(
                        AlternateView.CreateAlternateViewFromString(GetEmailBody(textTemplate, args),
                                                                    null,
                                                                    MediaTypeNames.Text.Plain));
                }
                if (!string.IsNullOrWhiteSpace(htmlTemplate))
                {
                    message.AlternateViews.Add(
                        AlternateView.CreateAlternateViewFromString(GetEmailBody(textTemplate, args),
                                                                    null,
                                                                    MediaTypeNames.Text.Html));
                }

                client.Send(message);
            }
        }

        private string GetEmailBody(string bodyTemplate, params object[] args)
        {
            return string.Format(GetTemplateText(bodyTemplate), args);
        }

        private string GetTemplateText(string templateName)
        {
            if (string.IsNullOrWhiteSpace(templateName)) return string.Empty;

            return File.ReadAllText(templateName);
        }
    }
}