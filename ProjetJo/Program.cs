using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjetsJo.Authentication;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.DAL.Repository;
using ProjetsJo.BLL.Interfaces;
using ProjetsJo.BLL.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

// Authentification 
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddTransient<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Services realated to DAL

builder.Services.AddScoped<IOfferData, OfferData>();
builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<ITicketingData, TicketingData>();

// Services related to BLL

builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketingService, TicketingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
