using BikeShop.Blazor;
using BikeShop.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddBlazorServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();


//Blazor UI:
//[Authorize] на.razor
//решает, показывать страницу или отправить на /login

//API:
//[Authorize] на контроллерах
//реально защищает данные и операции

//.AllowAnonymous() на Blazor endpoint
//говорит ASP.NET Core:
//  "сам endpoint приложения не защищай,
//   пусть доступом к страницам рулит Blazor"
// то есть по аттрибуту   [Authorize] уже сам ASP .NET как и Blazor, пытается сделать ASP.NET Core server authorization
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    // Blazor handles page authorization via AuthorizeRouteView.
    // API endpoints remain protected by JWT authentication.
    .AllowAnonymous();

app.Run();
