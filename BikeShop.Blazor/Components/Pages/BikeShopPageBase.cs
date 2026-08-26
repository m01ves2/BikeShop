using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services;
using BikeShop.Blazor.Services.Models;
using Microsoft.AspNetCore.Components;

namespace BikeShop.Blazor.Components.Pages
{
    //класс с общей для всех страниц функциональностью!
    public abstract class BikeShopPageBase : ComponentBase
    {
        //property injection, который Blazor умеет делать специально для компонентов!
        //Но Razor-компоненты создаются фреймворком, и для них Blazor поддерживает специальный механизм:
        //когда создашь этот компонент, возьми объект этого типа из DI-контейнера и присвой его этому свойству.
        //А в .razor привычный синтаксис: @inject SomeService Service
        //по сути компилируется примерно в такое свойство с[Inject]
        //Почему не constructor injection? Потому что Razor-компоненты Blazor создаёт и инициализирует сам,
        //и компонентная модель исторически ориентирована именно на property injection через[Inject].
        //В нашем BikeShopPageBase это особенно удобно, потому что тогда все наследники автоматически получают:
        //BreadcrumbService без повторного "@inject BreadcrumbService BreadcrumbService" на каждой странице.

        [Inject]
        protected BreadcrumbService BreadcrumbService { get; set; } = null!;
        protected string? ErrorMessage { get; private set; }

        protected void ShowError(ApiErrorResponseModel error)
        {
            ErrorMessage = error.Message;
        }

        protected void ShowError(string message)
        {
            ErrorMessage = message;
        }

        protected void ClearError()
        {
            ErrorMessage = null;
        }

        protected void SetBreadcrumbs(params (string Text, string? Url)[] items)
        {
            BreadcrumbService.SetItems(
                items.Select(x => new BreadcrumbItem
                {
                    Text = x.Text,
                    Url = x.Url
                }));
        }
    }
}
