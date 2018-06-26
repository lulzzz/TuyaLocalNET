namespace TuyaLocal.Api
{
    using System;
    using System.IO;
    using System.Reflection;
    using Akka.Actor;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Swashbuckle.AspNetCore.Swagger;
    using Utils;

    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options => options.SerializerSettings.Converters.Add(
                        new IpAddressConverter()));

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new Info
                        {
                            Title = "TuyaLocal API",
                            Version = "v1",
                            Description =
                                "API to control smart devices without connecting to chinese tuya servers and using tons of third party programs",
                            Contact = new Contact
                            {
                                Name = "Jonathan Berg",
                                Email = "jberg@netik.de",
                                Url = "http://github.com/ektooo"
                            },
                            License = new License
                            {
                                Name =
                                    "Use under Mozilla Public License Version 2.0",
                                Url =
                                    "https://github.com/ektooo/TuyaLocalNET/blob/master/LICENSE"
                            }
                        });

                    var xmlFile =
                        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                    var xmlPath =
                        Path.Combine(AppContext.BaseDirectory, xmlFile);

                    c.IncludeXmlComments(xmlPath);
                });

            services.AddSingleton(
                new ActorManager(ActorSystem.Create("TuyaLocalApi")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "TuyaLocal API V1");
                });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
