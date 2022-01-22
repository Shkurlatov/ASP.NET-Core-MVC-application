using School.Application.Interfaces;
using School.Application.Services;
using School.Core.Interfaces;
using School.Infrastructure.Logging;
using School.Infrastructure.Data;
using School.Infrastructure.Repository;
using School.Web.HealthChecks;
using School.Web.Interfaces;
using School.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Core.Repositories;
using School.Core.Repositories.Base;
using School.Core.Configuration;
using School.Infrastructure.Repository.Base;
using AutoMapper;

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
            ConfigureSchoolServices(services);            

            services.AddRazorPages();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureSchoolServices(IServiceCollection services)
        {
            services.Configure<SchoolSettings>(Configuration);

            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ICourseService, CourseService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IIndexPageService, IndexPageService>();
            services.AddScoped<IStudentPageService, StudentPageService>();
            services.AddScoped<IGroupPageService, GroupPageService>();
            services.AddScoped<ICoursePageService, CoursePageService>();

            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            //services.AddDbContext<SchoolContext>(c =>
            //    c.UseInMemoryDatabase("DefaultConnection"));

            services.AddDbContext<SchoolContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
