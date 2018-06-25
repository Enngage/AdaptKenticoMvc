
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Model;
using Adapt.Model.Components;
using CloudIntegration;
using CloudIntegration.Models;
using KenticoCloud.Delivery;

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
                    component = GetTextComponent(textComponent);
                }
                else if (inputComponent is Graphic graphicsComponent)
                {
                    component = GetGraphicComponent(graphicsComponent);
                }
                else
                {
                    if (throwExceptionForUnsupportedTypes)
                    {
                        throw new NotSupportedException($"Unsupported component type '{inputComponent.GetType().Name}' for component '{inputComponent.System.Codename}'");
                    }
                }

                if (throwExceptionForUnsupportedTypes && component == null)
                {
                    throw new ArgumentException(nameof(component));

                }

                if (component != null)
                {
                    // set shared data
                    component.ParentId = parent.Id;
                    component.DisplayTitle = inputComponent.DisplayTitle;
                    component.Instructions = inputComponent.Instructions;
                    component.Layout = GetLayout(inputComponent.Layout);
                    component.Title = inputComponent.Title;

                    components.Add(component);
                }

            }

            return components;
        }

        public GraphicComponentAdapt GetGraphicComponent(Graphic inputComponent)
        {
            return new GraphicComponentAdapt()
            {
                Id = inputComponent.System.Id,
                Graphic = new FullGraphic()
                {
                    Alt = inputComponent.Alt,
                    LargeSrc = inputComponent.LargeImage.FirstOrDefault()?.Url,
                    SmallSrc = inputComponent.SmallImage.FirstOrDefault()?.Url
                }
            };
        }


        public TextComponentAdapt GetTextComponent(Text inputComponent)
        {
            return new TextComponentAdapt()
            {
                Id = inputComponent.System.Id,
                Body = inputComponent.Body,
            };
        }

        private string GetLayout(IEnumerable<MultipleChoiceOption> options)
        {
            // its a radio button and we are interested only in first value
            return options?.FirstOrDefault()?.Name?.ToLower();
        }

    
    }
}
