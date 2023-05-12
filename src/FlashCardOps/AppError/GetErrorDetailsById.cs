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

namespace FlashCardOps.AppError
{
    public class GetErrorDetailsById
    {
        private readonly ILogger<GetErrorDetailsById> _logger;
        private readonly IFCDetailRepo _fCDetailRepo;
        public GetErrorDetailsById(ILogger<GetErrorDetailsById> logger,
       IFCDetailRepo fCDetailRepo)
        {
            _logger = logger;
            _fCDetailRepo = fCDetailRepo;
        }

        [Function("GetErrorDetailsById")]
        [OpenApiOperation(operationId: "GetErrorDetailsById", tags: new[] { "GetErrorDetailsById" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter("Id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Id of the detailed error info")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AppResponseWithResult<FCDetailModel>), Description = "The OK response")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "ErrorDetails/{Id}")] HttpRequestData req, string Id)
        {
            var fcDetails = await _fCDetailRepo.GetFCDetailsByIdAsync(Id);
            AppResponseWithResult<FCDetailModel> results = new()
            {
                HttpResult = new()
                {
                    OperationStatus = 200,
                    Status = "Fc Detail retrieved succesfully"
                },
                ObjectResult = fcDetails
            };
            var bodyStream = new MemoryStream();
            var writer = new StreamWriter(bodyStream);
            writer.Write(JsonConvert.SerializeObject(fcDetails));
            writer.Flush();
            bodyStream.Position = 0;
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Body = bodyStream;
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");


            return response;
        }
    }
}
