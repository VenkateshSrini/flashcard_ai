using System.Net;
using FlashCardOps.MessagePackets;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TechCards.lib.DBModel;
using TechCards.Repository;

namespace FlashCardOps.Cards
{
    public class GetAllCards
    {
        private readonly ILogger<GetAllCards> _logger;
        private readonly IFCRepo _fCRepo;

        public GetAllCards(ILogger<GetAllCards> logger, IFCRepo fCRepo)
        {
            _logger = logger;
            _fCRepo = fCRepo;
        }

        [Function("GetAllCards")]
        [OpenApiOperation(operationId: "GetAllCards", tags: new[] { "GetAllCards" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AppResponseWithResult<List<FCModel>>), Description = "The OK response")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var cards = await _fCRepo.GetAllFCDetails();
            AppResponseWithResult<List<FCModel>> respMsg = new()
            {
                HttpResult = new()
                {
                    OperationStatus = 200,
                    Status = "Objects Fecthed"
                },
                ObjectResult = cards
            };
            var bodyStream = new MemoryStream();
            var writer = new StreamWriter(bodyStream);
            writer.Write(JsonConvert.SerializeObject(cards));
            writer.Flush();
            bodyStream.Position = 0;
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Body = bodyStream;
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return response;
        }
    }
}
