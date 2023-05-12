using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCards.lib.DBModel
{
    public class FCModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? DetailId { get; set; }
        public string CardDisplayText { get; set; } = string.Empty;

    }
    public class FCDetailModel
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string ApplicationName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Framework { get; set; }=string.Empty;
        public string Version { get; set; }= string.Empty;
        public string Resolution { get; set; }=string.Empty;
    }
}
