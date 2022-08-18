using _05_HireAndForget.Data;
using _05_HireAndForget.Repositories.Interfaces;

namespace _05_HireAndForget.Repositories {
    public class LogsRepository : ILogsRepository {
        private readonly IServiceProvider _serviceProvider;

        public LogsRepository(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task BackgroundSaveLogs(string message) {
            try {
                /*Creamos un scope mediante el service provider para crear un scope
                en el cual vamos a crear un context indepentiente al contexto 
                inyectado en el sistema de inyeccion de dependecias*/
                await using var scope = _serviceProvider.CreateAsyncScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var log = new Log {
                    Message = message
                };

                await context.AddAsync(log);

                /*Usamos el Task.Delay para simular que el servicio tarda tiempo y poder
                ver que el el resultado esta funcion se ejecuta en segundo plano*/
                await Task.Delay(500);
                Console.WriteLine("Try");

                await context.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine("Catch");
                Console.WriteLine(ex.Message);
            }
        }
    }
}