using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SendWelcomeEmail
{
    public class Function
    {
        private readonly string _username = Environment.GetEnvironmentVariable("USERNAME");
        private readonly string _password = Environment.GetEnvironmentVariable("PASSWORD");
        private readonly string _smtpAddress = Environment.GetEnvironmentVariable("SMTP_ADDRESS");
        private readonly int _smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(Email input, ILambdaContext context)
        {
            var message = BuildMessage(input);
            return SendEmail(message);
        }

        private string SendEmail(MimeMessage email)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpAddress, _smtpPort, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_username, _password);
                    client.Send(email);
                    client.Disconnect(true);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> SendEmailAsync(MimeMessage email)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpAddress, _smtpPort, false).ConfigureAwait(false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_username, _password).ConfigureAwait(false);
                    await client.SendAsync(email).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private MimeMessage BuildMessage(Email email)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_username));
            emailMessage.To.Add(new MailboxAddress(email.Receiver));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart("plain") { Text = email.Body };

            return emailMessage;
        }
    }
}
