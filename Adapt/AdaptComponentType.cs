namespace Adapt
{
    public class AdaptComponentType
    {
        public string Type { get; }

        public static readonly AdaptComponentType Text = new AdaptComponentType("text");
        public static readonly AdaptComponentType Accordion = new AdaptComponentType("accordion");
        public static readonly AdaptComponentType Graphic = new AdaptComponentType("graphic");
        public static readonly AdaptComponentType Media = new AdaptComponentType("media");
        public static readonly AdaptComponentType Mcq = new AdaptComponentType("mcq");

        private AdaptComponentType(string type)
        {
            Type = type;
        }
    }
}
