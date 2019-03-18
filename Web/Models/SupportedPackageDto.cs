using CloudIntegration.Models.Cloud;

namespace Web.Models
{
    public class SupportedPackageDto
    {
        public Package Package { get; set; }
        public GenerateLogModel Log { get; set; }
        public string ProjectId { get; set; }
    }
}
