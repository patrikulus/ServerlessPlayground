using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqsClient
{
    public class SqsQueueManager
    {
        private readonly AmazonSQSClient _client;

        public SqsQueueManager(AmazonSQSClient client)
        {
            _client = client;
        }

        public async Task<CreateQueueResponse> CreateQueueAsync(string queueName)
        {
            CreateQueueRequest createQueueRequest = new CreateQueueRequest
            {
                QueueName = queueName
            };

            return await _client.CreateQueueAsync(createQueueRequest);
        }

        public async Task<SendMessageResponse> SendMessageAsync(string queueUrl, string message)
        {
            SendMessageRequest sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = message
            };

            return await _client.SendMessageAsync(sendMessageRequest);
        }
    }
}
