using Amazon.SQS;
using System;

namespace SqsClient
{
    public class SqsClientFactory
    {
        public static AmazonSQSClient Create()
        {
            AmazonSQSConfig amazonSQSConfig = new AmazonSQSConfig();

            amazonSQSConfig.ServiceURL = "http://sqs.eu-central-1.amazonaws.com";

            return new AmazonSQSClient(amazonSQSConfig);
        }
    }
}
