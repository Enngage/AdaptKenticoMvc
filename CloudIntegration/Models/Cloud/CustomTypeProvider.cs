using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentModels
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            {typeof(Accordion), "accordion"},
            {typeof(AccordionItem), "accordion_item"},
            {typeof(AnswerCode), "answer__code_"},
            {typeof(AnswerTextOnly), "answer__text_only_"},
            {typeof(Blank), "blank"},
            {typeof(Block), "block"},
            {typeof(CodeBlock), "code_block"},
            {typeof(Graphic), "graphic"},
            {typeof(Infobox), "infobox"},
            {typeof(MultipleChoiceQuestionTextOnly), "multiple_choice_question__text_only_"},
            {typeof(MultipleChoiceQuestionWithCode), "multiple_choice_question__with_code_"},
            {typeof(Narrative), "narrative"},
            {typeof(NarrativeCode), "narrative_code"},
            {typeof(NarrativeCodeItem), "narrative_code_item"},
            {typeof(NarrativeItem), "narrative_item"},
            {typeof(Package), "package"},
            {typeof(Page), "page"},
            {typeof(Section), "section"},
            {typeof(Text), "text"},
            {typeof(Video), "video"}
        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}