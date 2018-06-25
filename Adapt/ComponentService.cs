
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

        public const string IsRequiredYesOption = "yes";

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
                else if (inputComponent is Accordion accordionComponent)
                {
                    component = GetAccordionComponent(accordionComponent);
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
                    component.IsOptional = IsYesOptionChecked(inputComponent.IsOptional);
                    component.Classes = string.Join(" ", inputComponent.ComponentClasses.Select(m => m.Name)); // take name because codename might ruin class name
                    component.PageLevelProgress = new PageLevelProgressAdapt()
                    {
                        IsEnabled = IsYesOptionChecked(inputComponent.IncludeInProgress)
                    };

                    components.Add(component);
                }

            }

            return components;
        }

        public bool IsYesOptionChecked(IEnumerable<MultipleChoiceOption> options)
        {
            return options?.FirstOrDefault()?.Codename.Equals(IsRequiredYesOption, StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public SimpleGraphic GetSimpleGraphic(IEnumerable<Asset> assets)
        {
            // take only one asset
            var asset = assets.FirstOrDefault();

            if (asset == null)
            {
                return null;
            }

            return new SimpleGraphic()
            {
                Alt = asset.Name,
                Src = asset.Url
            };
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

        public AccordionComponentAdapt GetAccordionComponent(Accordion inputComponent)
        {
            return new AccordionComponentAdapt()
            {
                Id = inputComponent.System.Id,
                Body = inputComponent.Description,
                Items = inputComponent.AccordionItems.Select(m => new AccordionItemAdapt()
                {
                    Graphic = GetSimpleGraphic(m.Image),
                    Title = m.Title,
                    Body = m.Text
                }).ToList(),
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
