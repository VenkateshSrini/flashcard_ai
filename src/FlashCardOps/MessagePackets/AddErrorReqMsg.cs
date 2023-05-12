using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardOps.MessagePackets
{
    public class AddErrorReqMsg
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Framework { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
