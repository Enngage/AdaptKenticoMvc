using KenticoKontentModels;

namespace Web.Models
{
    public class SupportedPackageDto
    {
        public Package Package { get; set; }
        public GenerateLogModel ProdLog { get; set; }
        public GenerateLogModel PreviewLog { get; set; }
        public string ProjectId { get; set; }
    }
}
