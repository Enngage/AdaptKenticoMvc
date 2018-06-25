using System;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public class CustomTypeProvider : ICodeFirstTypeProvider
    {
        public Type GetType(string contentType)
        {
            switch (contentType)
            {
                case "accordion":
                    return typeof(Accordion);
                case "accordion_item":
                    return typeof(AccordionItem);
                case "answer__code_":
                    return typeof(AnswerCode);
                case "answer__text_only_":
                    return typeof(AnswerTextOnly);
                case "blank":
                    return typeof(Blank);
                case "block":
                    return typeof(Block);
                case "code_block":
                    return typeof(CodeBlock);
                case "graphic":
                    return typeof(Graphic);
                case "multiple_choice_question__text_only_":
                    return typeof(MultipleChoiceQuestionTextOnly);
                case "multiple_choice_question__with_code_":
                    return typeof(MultipleChoiceQuestionWithCode);
                case "narrative":
                    return typeof(Narrative);
                case "narrative_code":
                    return typeof(NarrativeCode);
                case "narrative_code_item":
                    return typeof(NarrativeCodeItem);
                case "narrative_item":
                    return typeof(NarrativeItem);
                case "package":
                    return typeof(Package);
                case "page":
                    return typeof(Page);
                case "section":
                    return typeof(Section);
                case "text":
                    return typeof(Text);
                case "video":
                    return typeof(Video);
                case "course":
                    return typeof(Course);
                default:
                    return null;
            }
        }
    }
}