using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Application.Services.Studies;
using School.Application.Services.Users;
using School.Application.Models.Studies;
using School.Application.Models.Users;
using School.Application.Repositories.Studies;
using School.Application.Repositories.Users;
using School.Domain.Configuration;
using School.Domain.Entities.Studies;
using School.Domain.Entities.Users;
using School.Domain.Interfaces.Studies;
using School.Domain.Interfaces.Users;
using School.Persistence.Data;
using System.Threading.Tasks;

namespace School.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SchoolContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            ConfigureSchoolServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SchoolContext schoolContext, RoleManager<IdentityRole> roleManager)
        {
            SetDatabase(schoolContext);
            SetRolesAsync(roleManager).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureSchoolServices(IServiceCollection services)
        {
            services.Configure<SchoolSettings>(Configuration);

            services.AddScoped<IStudyRepository<Course>, CourseRepository>();
            services.AddScoped<IStudyRepository<Group>, GroupRepository>();
            services.AddScoped<IStudyRepository<Student>, StudentRepository>();
            services.AddScoped<IUserRepository<Curator>, CuratorRepository>();

            services.AddScoped<IStudyService<CourseModel>, CourseService>();
            services.AddScoped<IStudyService<GroupModel>, GroupService>();
            services.AddScoped<IStudyService<StudentModel>, StudentService>();
            services.AddScoped<IUserService<CuratorModel>, CuratorService>();

            services.AddHttpContextAccessor();
        }

        private void SetDatabase(SchoolContext schoolContext)
        {
            schoolContext.Database.Migrate();
            SchoolContextSeed.SeedAsync(schoolContext).Wait();
        }

        private async Task SetRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("Curator"))
            {
                var curatorRole = new IdentityRole("Curator");
                await roleManager.CreateAsync(curatorRole);
            }
        }
    }
}
