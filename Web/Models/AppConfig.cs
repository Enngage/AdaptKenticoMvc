

using System.Collections.Generic;

namespace Web.Models
{
    public class AppConfig
    {
        public List<ProjectConfig> Projects { get; set; }
        public FileServiceConfig Files { get; set; }
        public int Depth { get; set; }
        public CorsConfig Cors { get; set; }
    }
}
