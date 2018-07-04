
using System;
using System.Collections.Generic;
using Adapt.Model;
using Adapt.Model.Components;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;

namespace Adapt
{
    public class ComponentService : IComponentService
    {

        public List<BaseAdaptComponent> GetAllComponents(BlockAdapt parent, List<IBaseComponent> inputComponents, bool throwExceptionForUnsupportedTypes)
        {
            var components = new List<BaseAdaptComponent>();

            foreach (var inputComponent in inputComponents)
            {
                BaseAdaptComponent component = null;
                if (inputComponent is Text textComponent)
                {
                    component = new TextWithCodeComponentAdapt(parent.Id, textComponent);
                }
                else if (inputComponent is Graphic graphicsComponent)
                {
                    component = new GraphicComponentAdapt(parent.Id, graphicsComponent);
                }
                else if (inputComponent is Accordion accordionComponent)
                {
                    component = new AccordionComponentAdapt(parent.Id, accordionComponent);
                }
                else if (inputComponent is Video videoComponent)
                {
                    component = new MediaComponentAdapt(parent.Id, videoComponent);
                }
                else if (inputComponent is MultipleChoiceQuestionTextOnly mcqtComponent)
                {
                    component = new McqComponentAdapt(parent.Id, mcqtComponent);
                }
                else if (inputComponent is Narrative narrativeComponent)
                {
                    component = new NarrativeComponentAdapt(parent.Id, narrativeComponent);
                }
                else if (inputComponent is MultipleChoiceQuestionWithCode multipleChoiceWithCodeComponent)
                {
                    component = new CmcqComponentAdapt(parent.Id, multipleChoiceWithCodeComponent);
                }
                else if (inputComponent is NarrativeCode narrativeCodeComponent)
                {
                    component = new NarrativeWithCodeComponentAdapt(parent.Id, narrativeCodeComponent);
                }
                else
                {
                    if (throwExceptionForUnsupportedTypes)
                    {
                        throw new NotSupportedException($"Unsupported component type '{inputComponent.GetType().Name}' for component '{inputComponent.System.Codename}'");
                    }
                }

                if (component != null)
                {
                    components.Add(component);
                }
            }

            return components;
        }

    }
}
