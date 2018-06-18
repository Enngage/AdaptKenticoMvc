namespace Adapt
{
    public class AdaptComponentType
    {
        public string Type { get; }

        public static readonly AdaptComponentType Text = new AdaptComponentType("text");

        private AdaptComponentType(string type)
        {
            Type = type;
        }
    }
}
