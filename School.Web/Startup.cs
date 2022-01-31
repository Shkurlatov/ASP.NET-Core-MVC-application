using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Application.Services;
using School.Application.Models;
using School.Application.Repositories;
using School.Domain.Configuration;
using School.Domain.Entities;
using School.Persistence.Data;

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

            services.AddScoped<Repository<Course>, CourseRepository>();
            services.AddScoped<Repository<Group>, GroupRepository>();
            services.AddScoped<Repository<Student>, StudentRepository>();

            services.AddScoped<IService<CourseModel>, CourseService>();
            services.AddScoped<IService<GroupModel>, GroupService>();
            services.AddScoped<IService<StudentModel>, StudentService>();

            services.AddHttpContextAccessor();
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
