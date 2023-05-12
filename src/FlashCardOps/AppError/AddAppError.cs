using System.Net;
using FlashCardOps.MessagePackets;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace FlashCardOps.AppError
{
    public class AddAppError
    {
        private readonly ILogger _logger;

        public AddAppError(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddAppError>();
        }

        [Function("AddAppError")]
        [OpenApiOperation(operationId: "AddAppError", tags: new[] { "AddAppError" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(AddErrorReqMsg))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AddErrorRespMsg), Description = "The OK response")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Application error received for resolution HTTP Entry point");
            var appErrorJson = new StreamReader(req.Body).ReadToEnd();
            AddErrorRespMsg nsgPacket = new()
            {
                UserProvidedErrorDetails = appErrorJson,
                Response = new()
                {
                    OperationStatus = (int)HttpStatusCode.Created,
                    Status = $"queue updated with inputs."

                }
            };

            var bodyStream = new MemoryStream();
            var writer = new StreamWriter(bodyStream);
            writer.Write(JsonConvert.SerializeObject(appErrorJson));
            writer.Flush();
            bodyStream.Position = 0;
            var response = req.CreateResponse(HttpStatusCode.Created);

            response.Body = bodyStream;
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");


            _logger.LogInformation("queue updated with inputs.");

            return response;

        }
    }
}
