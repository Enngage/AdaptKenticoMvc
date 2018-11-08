

using System.Collections.Generic;

namespace Web.Models
{
    public class AppConfig
    {
        public List<string> ProjectIds { get; set; }
        public FileServiceConfig Files { get; set; }
        public int Depth { get; set; }
    }
}
