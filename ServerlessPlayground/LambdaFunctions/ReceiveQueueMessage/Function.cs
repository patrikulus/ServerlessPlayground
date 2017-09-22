using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.SQS.Model;
using SqsClient;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ReceiveQueueMessage
{
    public class Function
    {
        private readonly string _queueUrl = Environment.GetEnvironmentVariable("QUEUE_URL");

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            return input?.ToUpper();
        }

        public async Task ReceiveMessage()
        {
            var client = SqsClientFactory.Create();
            var manager = new SqsQueueManager(client);
            var queueResponse = await manager.CreateQueueAsync("serverless");

            var receiveResponse = manager.ReceiveMessageAsync(queueResponse.QueueUrl);

            var messages = receiveResponse.Result.Messages;

            if (messages.Count != 0)
            {
                foreach (var message in messages)
                {
                    // TODO trigger SendWelcomeEmail function
                }
            }

        }
    }
}
