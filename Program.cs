using Microsoft.EntityFrameworkCore;
using Mini_Project_Kredit.Components;
using Mini_Project_Kredit.Models;
using Mini_Project_Kredit.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register services (WAJIB sebelum Build)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Authorization (untuk AuthorizeView / CascadingAuthenticationState)
builder.Services.AddAuthorizationCore();

// ✅ EF Core DbContextFactory
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ✅ App services
builder.Services.AddScoped<CreditRegistrationService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// ✅ Middleware pipeline (setelah Build)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// ✅ Map components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
