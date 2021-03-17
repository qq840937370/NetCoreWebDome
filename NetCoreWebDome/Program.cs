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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)  // WebHostBuilder新实例
                .UseStartup<Startup>();         // 为WebHost指定了启动类（新实例.UseStartup<启动类>()）
    }
}
