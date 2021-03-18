using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreWebDome
{
    public class Program
    {
        /// <summary>
        /// Main方法，程序的入口方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)  //调用下面的方法，返回一个IWebHostBuilder对象(Web主机（承载和运行Web应用程序的常规工具）配置实例)
                .Build()                // 用上面返回的IWebHostBuilder对象创建一个IWebHost(Web主机（承载和运行Web应用程序的常规工具）实例)
                .Run();                 // 运行Web应用程序一直监听http请求
        }
        /// <summary>
        /// WebHost配置实例
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)  // WebHostBuilder新默认实例

        #region 改写默认实例-加载自定义的json
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddJsonFile("Content.json", optional: false, reloadOnChange: false)  // 加载自定义的json-可选:false;重新加载更改:false
                      .AddEnvironmentVariables();

            })
        #endregion
            .UseStartup<Startup>();         // 为WebHost指定了启动类（新实例.UseStartup<启动类>()）
    }
}
