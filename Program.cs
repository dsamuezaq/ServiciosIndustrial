using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaemonIndustialMolinera.Base;
using DaemonIndustialMolinera.baseChariot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DaemonIndustialMolinera
{
    public class Program
    {
        public static void Main(string[] args)
        {
                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        { 
        var conn = "Server = tcp:mardisenginetestbd.database.windows.net,1433; Initial Catalog = MardisEngine_Test; Persist Security Info = False; User ID = mardisengine; Password = Mard!s3ngin3; Encrypt = True; TrustServerCertificate = False; MultipleActiveResultSets = True; App = EntityFramework; Connection Lifetime = 240; Timeout = 60; Max Pool Size = 200; Min Pool Size = 10; Pooling = True; ";// Configuration.GetConnectionString("DefaultConnection");
        var connEngv2 = "Server = tcp:mardisenginetestbd.database.windows.net,1433; Initial Catalog = EngineV2; Persist Security Info = False; User ID = mardisengine; Password = Mard!s3ngin3; Encrypt = True; TrustServerCertificate = False; MultipleActiveResultSets = True; App = EntityFramework; Connection Lifetime = 240; Timeout = 60; Max Pool Size = 200; Min Pool Size = 10; Pooling = True; ";// Configuration.GetConnectionString("DefaultConnection");

            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
                    optionsBuilder.UseSqlServer(conn);//,
                    services.AddScoped<TestContext>(s => new TestContext(optionsBuilder.Options));


                    var optionsBuilderEngV2 = new DbContextOptionsBuilder<EngineV2Context>();
                    optionsBuilderEngV2.UseSqlServer(connEngv2);//,
                    services.AddScoped<EngineV2Context>(s => new EngineV2Context(optionsBuilderEngV2.Options));

                    services.AddLogging();
                    services.AddHostedService<Worker>();
                });

            return host;
        }


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            services.AddControllersWithViews();
        //            services.AddSession();
        //            var conn = "Server = tcp:mardisenginetestbd.database.windows.net,1433; Initial Catalog = MardisEngine_Test; Persist Security Info = False; User ID = mardisengine; Password = Mard!s3ngin3; Encrypt = True; TrustServerCertificate = False; MultipleActiveResultSets = True; App = EntityFramework; Connection Lifetime = 240; Timeout = 60; Max Pool Size = 200; Min Pool Size = 10; Pooling = True; ";// Configuration.GetConnectionString("DefaultConnection");

        //            services.AddDbContext<TestContext>(options => options.UseSqlServer(conn));

        //            IConfiguration configuration = hostContext.Configuration;
        //            services.AddMemoryCache();
        //            services.AddHostedService<Worker>();
        //            services.AddLogging();

        //        });

    }
}
