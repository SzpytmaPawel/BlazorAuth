// Program.cs
using BlazorAuth.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlazorAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login"; 
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.MapPost("/process-login", async context =>
            {
                var form = await context.Request.ReadFormAsync();
                string username = form["username"];
                string password = form["password"];
                string returnUrl = form["ReturnUrl"].FirstOrDefault() ?? "/";

                if (username == "user" && password == "password")
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    context.Response.Redirect(returnUrl);
                }
                else
                {
                    context.Response.Redirect($"/login?error=Invalid+username+or+password&ReturnUrl={System.Net.WebUtility.UrlEncode(returnUrl)}");
                }
            });

            app.MapGet("/logout", async context =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Redirect("/login");
            });

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
