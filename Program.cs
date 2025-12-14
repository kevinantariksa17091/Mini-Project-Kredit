using Microsoft.EntityFrameworkCore;
using Mini_Project_Kredit.Components;
using Mini_Project_Kredit.Models;
using Mini_Project_Kredit.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddAuthorizationCore();


builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<CreditRegistrationService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<VillageService>();

var app = builder.Build();


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
