﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SereneRiverFarms.Areas.Identity.Data;
using SereneRiverFarms.Models;

[assembly: HostingStartup(typeof(SereneRiverFarms.Areas.Identity.IdentityHostingStartup))]
namespace SereneRiverFarms.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { 
                services.AddDbContext<SereneRiverFarmsContext>(options =>
                     options.UseSqlServer(
                         context.Configuration.GetConnectionString("SereneRiverFarmsContextConnection")));

                services.AddDefaultIdentity<SereneRiverFarmsUser>()
                     .AddEntityFrameworkStores<SereneRiverFarmsContext>();
            });
        }
    }
}

