using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SqsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreFramework.Models;

namespace WebAppCoreFramework.Services
{
    public class SqsService
    {
        public async Task<string> SendMessage(Email message)
        {
            var client = SqsClientFactory.Create();
            var manager = new SqsQueueManager(client);

            var queueResponse = await manager.CreateQueueAsync("serverless");
            var messageResponse = await manager.SendMessageAsync(
                queueResponse.QueueUrl, 
                JsonConvert.SerializeObject(
                    message, 
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));

            return messageResponse.HttpStatusCode.ToString();
        }
    }
}
