using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardOps.MessagePackets
{
    public class StandardResponse
    {
        public int OperationStatus { get; set; }
        public string Status { get; set; }=string.Empty;
       
    }
}
