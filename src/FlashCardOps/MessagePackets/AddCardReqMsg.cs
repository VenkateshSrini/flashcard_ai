using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardOps.MessagePackets
{
    public class AddCardReqMsg
    {
        public string? DetailId { get; set; }
        public string CardDisplayText { get; set; } = string.Empty;
    }
}
