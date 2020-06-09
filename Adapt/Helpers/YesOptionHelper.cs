using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery.Abstractions;

namespace Adapt.Helpers
{
    public static class YesOptionHelper
    {
        public const string IsRequiredYesOption = "yes";

        public static bool IsYesOptionChecked(IEnumerable<IMultipleChoiceOption> options)
        {
            return options?.FirstOrDefault()?.Codename.Equals(IsRequiredYesOption, StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}
