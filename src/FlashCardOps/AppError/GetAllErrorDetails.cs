using System.Net;
using FlashCardOps.MessagePackets;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TechCards.lib.DBModel;
using TechCards.Repository;
using Newtonsoft.Json;


namespace FlashCardOps.AppError
{
    public class GetAllErrorDetails
    {
        private readonly ILogger<GetAllErrorDetails> _logger;
        private readonly IFCDetailRepo _fCDetailRepo;

        public GetAllErrorDetails(ILogger<GetAllErrorDetails> logger, IFCDetailRepo fCDetailRepo)
        {
            _logger = logger;
            _fCDetailRepo = fCDetailRepo;
        }

        [Function("GetAllErrorDetails")]
        [OpenApiOperation(operationId: "GetAllErrorDetails", tags: new[] { "GetAllErrorDetails" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AppResponseWithResult<List<FCDetailModel>>), Description = "The OK response")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {


            var appErrorDetails = await _fCDetailRepo.GetAllFCDetails();
            AppResponseWithResult<List<FCDetailModel>> respMsg = new()
            {
                HttpResult = new()
                {
                    OperationStatus = 200,
                    Status = "Objects Fecthed"
                },
                ObjectResult = appErrorDetails
            };
            var bodyStream = new MemoryStream();
            var writer = new StreamWriter(bodyStream);
            writer.Write(JsonConvert.SerializeObject(appErrorDetails));
            writer.Flush();
            bodyStream.Position = 0;
            var response = req.CreateResponse(HttpStatusCode.Created);

            response.Body = bodyStream;
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            return response;
        }
    }
}
