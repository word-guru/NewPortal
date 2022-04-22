#nullable enable
using NewPortal.Server.Lib.Models;

namespace NewPortal.Server
{
    public class Request
    {
        public string Type { get; set; }
        public int? Id { get; set; }
        
        public News? News { get; set; }
    }
}