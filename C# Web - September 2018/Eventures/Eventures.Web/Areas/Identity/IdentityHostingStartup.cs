using System;
using Eventures.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Eventures.Web.Areas.Identity.IdentityHostingStartup))]
namespace Eventures.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) =>
            //{
            //    services.AddDbContext<EventuresDbContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("DefaultConnection")));

            //    services
            //        .AddIdentity<IdentityUser, IdentityRole>(config =>
            //        {
            //            config.SignIn.RequireConfirmedEmail = true;
            //        })
            //        .AddRoleManager<RoleManager<IdentityRole>>()
            //        .AddDefaultUI()
            //        .AddDefaultTokenProviders()
            //        .AddEntityFrameworkStores<EventuresDbContext>();
            //});
        }
    }
}