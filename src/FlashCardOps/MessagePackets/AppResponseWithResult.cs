using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardOps.MessagePackets
{
    public class AppResponseWithResult<T> where T: class
    {
        public T? ObjectResult { get; set; }
        public StandardResponse? HttpResult { get; set; }   
    }
}
