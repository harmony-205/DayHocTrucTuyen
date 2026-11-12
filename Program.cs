using DayHocTrucTuyen.Models;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Register EF Core 9 DbContext
builder.Services.AddDbContext<DayHocTrucTuyenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DayHocTrucTuyen")));

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/account/login";
        options.LogoutPath = "/account/login";
        options.AccessDeniedPath = "/access/denied";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

// Using custom authorization policy handler
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

// Using SignalR real-time chat
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Using custom status code error pages (403, 404)
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// SignalR ChatHub endpoint
app.MapHub<ChatHub>($"/models/{nameof(ChatHub)}");

// Area route mapping
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default route mapping
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
