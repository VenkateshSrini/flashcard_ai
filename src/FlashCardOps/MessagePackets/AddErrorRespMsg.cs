using Microsoft.Azure.Functions.Worker;

namespace FlashCardOps.MessagePackets
{
    public class AddErrorRespMsg
    {
        [QueueOutput("fcqueue", Connection = "queue_conn")]
        public string UserProvidedErrorDetails { get; set; }=string.Empty;
        public StandardResponse? Response { get; set; }
    }
}
