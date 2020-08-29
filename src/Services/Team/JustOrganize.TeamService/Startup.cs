using JustOrganize.TeamService.LocationClient;
using JustOrganize.TeamService.LocationClient.Abstraction;
using JustOrganize.TeamService.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JustOrganize.TeamService
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(
            IConfiguration configuration)
        {
            this.Configuration = configuration;     
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ITeamRepository, MemoryTeamRepository>();
            services.AddSwaggerGen();

            var locationUrl = Configuration.GetSection("location:url").Value;
      
            services.AddSingleton<ILocationClient>(
                new HttpLocationClient(locationUrl)
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Team Service API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
