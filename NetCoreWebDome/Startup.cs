using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreWebDome.Models;

namespace NetCoreWebDome
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 使用此方法向容器添加服务(此方法由运行时调用)
        /// </summary>
        /// <param name="services">服务</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>  // 确定请求是否需要非必需cookie的用户同意
            {
                // 此lambda确定给定请求是否需要非必需cookie的用户同意。
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.Configure<Content>(Configuration.GetSection("Content"));  // 从appsettings.json配置文件中的Content节点匹配到Content对象

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);  // 设置兼容性

        }

        /// <summary>
        /// 使用此方法配置HTTP请求管道(此方法由运行时调用)
        /// </summary>
        /// <param name="app">应用程序</param>
        /// <param name="env">宿主环境</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())  // 宿主环境
            {
                app.UseDeveloperExceptionPage();  // 注入应用程序
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  // 应用程序异常处理页
                // 默认的HSTS值是30天。您可能需要为生产场景更改此设置，请参阅 https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();  // Http协议
            app.UseStaticFiles();       // 静态文件
            app.UseCookiePolicy();      // Cookie策略

            // 指定启动页
            app.UseMvc(routes => 
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
