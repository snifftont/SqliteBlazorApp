using WcomBlazorApp.Components;
using Microsoft.AspNetCore.Components.Web;
using WcomBlazorApp.Components.Pages.Services;
using WcomBlazorApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


// Add HttpClient for making external web API calls to the Backend server API
builder.Services.AddHttpClient();

// For prerendering purposes, register a named HttpClient for the app's
// named client component example.
builder.Services.AddHttpClient("WebAPI", client =>
    client.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7270"));


// For prerendering purposes, register the client app's typed HttpClient
// for the app's typed client component example.
//builder.Services.AddHttpClient<TodoHttpClient>(client => client.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5002"));

// Add Todo service for components adopting SSR
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();


// Add a CORS policy for the client
// Add .AllowCredentials() for apps that use an Identity Provider for authn/z
builder.Services.AddCors(
    options => options.AddPolicy(
        "server",
        policy => policy.WithOrigins(builder.Configuration["FrontendUrl"] ?? "https://localhost:7006")
            .AllowAnyMethod()
            .AllowAnyHeader()));

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

// Add NSwag services
//builder.Services.AddOpenApiDocument();
builder.Services.AddAutoMapper(typeof(EmployeeProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
