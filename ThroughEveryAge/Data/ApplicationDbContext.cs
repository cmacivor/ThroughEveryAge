using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThroughEveryAge.Models;

namespace ThroughEveryAge.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<LessonContent> LessonContents { get; set; }

        public DbSet<Journal> Journals { get; set; }

        public DbSet<Contact> Contacts { get; set; }


        public IConfiguration Configuration { get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string conString = ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");

            string conString = ConfigurationExtensions.GetConnectionString(Configuration, "DefaultConnection");

            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ThroughEveryAgeDB;Integrated Security=True");
            optionsBuilder.UseSqlServer(conString);

            //ConfigurationManager
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
