using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.ChatCompletions;
using Forge.OpenAI.Models.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCards.lib.DBModel;
using TechCards.Repository;

namespace TechCards.Services
{
    public class ChatGPTServices : IChatGPTServices
    {
        private readonly IFCDetailRepo _fcDetailRepo;
        private readonly IOpenAIService openAIService;
        private readonly ILogger<ChatGPTServices> logger;
        public ChatGPTServices(IFCDetailRepo fcDetailRepo,
            IOpenAIService openAIService,
            ILogger<ChatGPTServices> logger)
        {
            _fcDetailRepo = fcDetailRepo;
            this.openAIService = openAIService;
            this.logger = logger;
        }
        public async Task<bool> FindResolution(FCDetailModel fCDetail)
        {
            var messageConstruct = $"{fCDetail.ErrorMessage} {fCDetail.Language} {fCDetail.Framework} {fCDetail.Version}";
            ChatCompletionRequest request = new ChatCompletionRequest(ChatMessage.CreateFromUser(messageConstruct));

            HttpOperationResult<ChatCompletionResponse> response = await openAIService.ChatCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                fCDetail.Resolution = response.Result!.Choices[0].Message.Content;
                bool result = false;
                var fcDetailFromDB = await _fcDetailRepo.GetFCDetailsByIdAsync(fCDetail.Id);
                if (fcDetailFromDB != default)
                 result = await _fcDetailRepo.UpdateFCDetailsAsync(fCDetail.Id,
                    fCDetail.Resolution);
                else
                {
                    fcDetailFromDB = await _fcDetailRepo.InsertFCDetailsAsync(fCDetail);
                    result = (fcDetailFromDB != default);
                }
                return result;
            }
            return false;
        }
    }
}
