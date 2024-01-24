using System.Collections.Generic;

namespace Shop
{
    public static class GlobalVariables
    {
        public static Dictionary<CurrencyType, string> CURRENCY_ICONS_PATH = new Dictionary<CurrencyType, string>()
        {
            { CurrencyType.Gold, "Sprites/gold_currency" },
            { CurrencyType.Gem, "Sprites/gems_currency" },
            { CurrencyType.Time, "Sprites/time_currency" },
        };
    }
}