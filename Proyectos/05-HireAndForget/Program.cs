using _05_HireAndForget;
using _05_HireAndForget.Data;
using _05_HireAndForget.Repositories.Interfaces;

var app = StartUp.ItializeApplication(args);

/*----- ENDPOINTS -----*/

app.MapGet("/", () => "Hello World!");

app.MapPost("/people", async (Person person, AppDbContext context, ILogsRepository logsRepository) => {
    await context.AddAsync(person);
    await context.SaveChangesAsync();

    /*Usamos la operacion de descarte para que la ejecucion del hilo se ejecute
    en background mediante otro hilo el cual no vamos a esperar su resultado*/
    _ = Task.Run(async () => {
        await logsRepository.BackgroundSaveLogs($"Ha sido insertado el registro Persona con Id { person.Id } y Nombre = { person.Name }");
    });

    return Results.Ok();
});

app.Run();