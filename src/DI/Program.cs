using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DI {

    class MyContext : DbContext {
        public MyContext(DbContextOptions options) : base(options) {

        }
    }

    class MyServie {
        private readonly ILogger<MyServie> logger;
        private readonly MyContext context;
        public MyServie(ILogger<MyServie> logger, MyContext context) {
            this.logger = logger;
            this.context = context;
        }

        public void Method1() {
            logger.LogInformation("call MyService.Method1");
        }

        public void Method2() {
            logger.LogInformation("call MyService.Method1");
        }
    }

    static class Program {
        static void Main(string[] args) {
            var collection = new ServiceCollection();
            collection.AddLogging(builder => {

                builder.AddConsole(options => {
                    options.IncludeScopes = true;
                });
            });
            collection.AddDbContext<MyContext>(options => {
                options.UseNpgsql("Host=localhost;User Id=postgres;Password=1234;Database=DI");
            });
            collection.AddTransient<MyServie>();

            var provider = collection.BuildServiceProvider();
            var service = provider.GetService<MyServie>();
            service.Method1();
            service.Method2();

            Console.Read();
        }
    }
}