using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BooksApi.Models;
using BooksApi.Interfaces;
using Microsoft.Extensions.Options;
using BooksApi.Services;
using System;
using Microsoft.OpenApi.Models;

namespace BooksApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<BookstoreDBSettings>(
                    Configuration.GetSection(nameof(BookstoreDBSettings)));

            services.AddSingleton<IBookstoreDBSettings>(sp =>
                    sp.GetRequiredService<IOptions<BookstoreDBSettings>>().Value);

            services.AddSingleton<BookService>();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddMvc();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                                   { 
                                        Version = "v1",
                                        Title = "books API",
                                        Description = "new asp net core api with swagger =]",
                                        TermsOfService = new Uri("https://localhost"),
                                        Contact = new OpenApiContact
                                        {
                                            Name = "Ademir Purin",
                                            Email = string.Empty,
                                            Url = new Uri("https://localhost"),
                                        },
                                        License = new OpenApiLicense
                                        {
                                            Name = "lic",
                                            Url = new Uri("https://localhost"),
                                        }                                
                                   });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json","api v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
