using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SereneRiverFarms.Areas.Authentication.Data;


namespace SereneRiverFarms.Models
{
    public class SereneRiverFarmsContext : IdentityDbContext<SereneRiverFarmsUser>
    {
        public SereneRiverFarmsContext(DbContextOptions<SereneRiverFarmsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Add any customizations to ASP.NET Identity model and override defaults here if needed.
        }
    }
}
