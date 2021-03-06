using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHouse.BackgroundWorkers;
using SmartHouse.EntityCore.Context;
using SmartHouse.EntityCore.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace SmartHouse
{
    public class Startup
    {
        #region Creditionals
        private string Login { get => Configuration["AdminLogin"]; }
        private string Password { get => Configuration["AdminPassword"]; }
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            //services.AddTransient<DbHelper>();

            services.AddEntityFrameworkSqlite().AddDbContext<SensorDataContext>();
            services.AddDbContext<SensorDataContext>(options => options.UseSqlite(Configuration["DefaultConnection"]));

            services.AddHostedService<ArduinoDataGetter>();

            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication( options => {
                          options.Events = new BasicAuthenticationEvents
                          {
                              OnValidatePrincipal = context =>
                              {
                                    if ((context.UserName.Equals(Login)) && (context.Password.Equals(Password)))
                                    {
                                        var claims = new List<Claim>
                                        {
                                          new Claim(ClaimTypes.Name, 
                                                    context.UserName, 
                                                    context.Options.ClaimsIssuer)
                                        };
 
                                        var ticket = new AuthenticationTicket(
                                          new ClaimsPrincipal(new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme)),
                                          new AuthenticationProperties(), BasicAuthenticationDefaults.AuthenticationScheme
                                        );
                                        return Task.FromResult(AuthenticateResult.Success(ticket));
                                    }
                                    return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
                              }
                          };
                    });

            void Injects(){
                
            };
            Injects();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            using (var serviceScope = app.ApplicationServices
                .GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SensorDataContext>();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }


    }
}
