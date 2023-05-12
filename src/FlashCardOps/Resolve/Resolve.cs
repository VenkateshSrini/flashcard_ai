using System;
using FlashCardOps.MessagePackets;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TechCards.lib.DBModel;
using TechCards.Services;

namespace FlashCardOps.Resolve
{
    public class Resolve
    {
        private readonly ILogger<Resolve> _logger;
        private readonly IChatGPTServices _chatGPTServices;
        public Resolve(IChatGPTServices chatGPTServices,
            ILogger<Resolve> logger)
        {
            _logger = logger;

            _chatGPTServices = chatGPTServices;
        }

        [Function("Resolve")]
        public async Task Run([QueueTrigger("fcqueue", Connection = "queue_conn")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var req = JsonConvert.DeserializeObject<AddErrorReqMsg>(myQueueItem);
            if (req != null)
            {
                FCDetailModel model = new()
                {
                    ApplicationName = req.ApplicationName,
                    ErrorMessage = req.ErrorMessage,
                    Framework = req.Framework,
                    Language = req.Language,
                    Version = req.Version
                };
                var isResolutionFound = await _chatGPTServices.FindResolution(model);
            }
        }
    }
}
