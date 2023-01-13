using FirstAuthentication.Handler;
using FirstAuthentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/UserLog";
    opt.LogoutPath = "/Logout";
    opt.AccessDeniedPath = "/Denied";
    opt.Cookie.Name = "ck_auth_ent";
    opt.ReturnUrlParameter = "backUrl";
});

builder.Services.AddSingleton<IAuthorizationHandler, LevelAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationPolicyProvider, LevelAuthorizationPolicyProvider>();
//builder.Services.AddAuthorizationBuilder().AddPolicy(LevelAuthorizationHandler.POLICY_NAME, pb =>
//{
//    pb.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
//    pb.AddRequirements(new LevelAuthorizationRequirement(3));
//});
builder.WebHost.UseUrls("http://*:9009");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/Denied", 
    () => "·ÃÎÊ±»¾Ü¾ø"
    );
app.MapGet("/Logout", async (HttpContext context) =>
{
    await context.SignOutAsync();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=UserLog}/{id?}");
app.Run();
