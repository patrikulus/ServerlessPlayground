using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using SendWelcomeEmail;
using Microsoft.Extensions.Configuration;

namespace SendWelcomeEmail.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("secrets.json")
                .Build();
            var emailSettings = config.GetSection("email");

            Environment.SetEnvironmentVariable("USERNAME", emailSettings["username"]);
            Environment.SetEnvironmentVariable("PASSWORD", emailSettings["password"]);
            Environment.SetEnvironmentVariable("SMTP_ADDRESS", emailSettings["smtp-address"]);
            Environment.SetEnvironmentVariable("SMTP_PORT", emailSettings["smtp-port"]);
        }

        [Fact]
        public void TestSendEmailMessageFunctionAsync()
        {

            // Arrange
            var function = new Function();
            var context = new TestLambdaContext();
            var input = new Email
            {
                Body = "Hello message!",
                Subject = "Test message",
                Receiver = "plotzwi@gmail.com"
            };

            // Act
            var status = function.FunctionHandler(input, context);

            // Assert
            Assert.Equal("OK", status);
        }
    }
}
