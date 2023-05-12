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
    public class CreateCards
    {
        private readonly ILogger<CreateCards> _logger;
        private readonly IFCRepo _fCRepo;

        public CreateCards(ILogger<CreateCards> logger, IFCRepo fCRepo)
        {
            _logger = logger;
            _fCRepo = fCRepo;
        }

        [Function("CreateCards")]
        [OpenApiOperation(operationId: "CreateCards", tags: new[] { "CreateCards" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(AddCardReqMsg))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AppResponseWithResult<string>), Description = "The OK response")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var cardJson = new StreamReader(req.Body).ReadToEnd();
            var addCardMsg = JsonConvert.DeserializeObject<AddCardReqMsg>(cardJson);
            FCModel fcModel = new()
            {
                CardDisplayText = addCardMsg.CardDisplayText,
                DetailId = addCardMsg?.DetailId
            };
            var fcModelDB = await _fCRepo.InsertFCDetailsAsync(fcModel);
            AppResponseWithResult<string>? result = default;
            if (fcModelDB != null)
                result = new()
                {
                    HttpResult = new StandardResponse
                    {
                        OperationStatus = (int)HttpStatusCode.OK,
                        Status = "Created"
                    },
                    ObjectResult = "Card created sucessfully"
                };
            else
                result = new()
                {
                    HttpResult = new StandardResponse
                    {
                        OperationStatus = (int)HttpStatusCode.InternalServerError,
                        Status = "Creation failed"
                    },
                    ObjectResult = "Card DB issue"
                };
            var bodyStream = new MemoryStream();
            var writer = new StreamWriter(bodyStream);
            writer.Write(JsonConvert.SerializeObject(result));
            writer.Flush();
            bodyStream.Position = 0;
            var response = req.CreateResponse((HttpStatusCode)result.HttpResult.OperationStatus);
            response.Body = bodyStream;

            response.Headers.Add("Content-Type", "application/json; charset=utf-8");



            return response;
        }
    }
}
