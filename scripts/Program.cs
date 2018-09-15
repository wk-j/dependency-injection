using System;
using Microsoft.Extensions.DependencyInjection;

namespace DI {
    class SingletonService {
        public SingletonService() {
            Console.WriteLine("Singleton");
        }
    }

    class ScopeService : IDisposable {
        public ScopeService(SingletonService signleton) {
            Console.WriteLine("Scope");
        }

        public void Dispose() {
            Console.WriteLine("Destoy scope");
        }
    }

    class TransientService : IDisposable {
        public TransientService(ScopeService scope, SingletonService singleton) {
            Console.WriteLine("Transiet");
        }

        public void Dispose() {
            Console.WriteLine("Destroy transient");
        }
    }

    class UserService {
        public UserService(TransientService transient, ScopeService scope, SingletonService single) {

        }
    }

    class ContextService {
        public ContextService(TransientService transient, UserService user, ScopeService scope, SingletonService single) {

        }
    }

    class Program {
        static void Main(string[] args) {
            void go() {
                var collection = new ServiceCollection();
                collection.AddSingleton<SingletonService>();
                collection.AddScoped<ScopeService>();

                collection.AddTransient<TransientService>();
                collection.AddTransient<ContextService>();
                collection.AddTransient<UserService>();

                var provider = collection.BuildServiceProvider();

                var singleton = provider.GetService<SingletonService>();
                var scope = provider.GetService<ScopeService>();
                var transient = provider.GetService<TransientService>();
                var context = provider.GetService<ContextService>();
            }

            go();
            Console.Read();
        }
    }
}
