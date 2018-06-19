namespace Adapt
{
    public class AdaptComponentType
    {
        public string Type { get; }

        public static readonly AdaptComponentType Text = new AdaptComponentType("text");

        public static readonly AdaptComponentType Graphic = new AdaptComponentType("graphic");

        private AdaptComponentType(string type)
        {
            Type = type;
        }
    }
}
