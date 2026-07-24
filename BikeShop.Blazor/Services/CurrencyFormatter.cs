using System.Globalization;

namespace BikeShop.Blazor.Services
{
    public class CurrencyFormatter
    {
        private static readonly CultureInfo RussianCulture = new("ru-RU");

        public string Format(decimal amount)
        {
            return $"{amount.ToString("N0", RussianCulture)} ₽";
        }
    }
}
