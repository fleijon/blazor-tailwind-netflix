using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor_tailwind_netflix.Client;
using blazor_tailwind_netflix.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IAuthenticationService, AuthneticationService>();
builder.Services.AddSingleton<MovieStore.IMovies, MovieStore.Movies>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddSingleton<IInfoModalManager, InfoModalManager>();

await builder.Build().RunAsync();
