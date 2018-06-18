
namespace Adapt
{
    public class AdaptModelType
    {
        public string Type { get; }

        public static readonly AdaptModelType Page = new AdaptModelType("page");
        public static readonly AdaptModelType Article = new AdaptModelType("article");
        public static readonly AdaptModelType Block = new AdaptModelType("block");
        public static readonly AdaptModelType Component = new AdaptModelType("component");

        private AdaptModelType(string type)
        {
            Type = type;
        }
    }
}
