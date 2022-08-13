/*Para usar el servicio de Storage de firebase debemos de crear un proyecto
establecer los valores en los user-secrets, de lo que es nuestro
usuario de authenticacion para nuestro proyecto.

Para esto activamos el servicio de authenticacion con correo y password,
creamos un usuario que vamos a usar para subir los archivos,
despues activamos el servicio de storage para almacemar imagenes
y en las reglas usamos if request.auth != null,
despues de estas configuraciones ya podremos comenzar a trabajar
con nuestro proyecto*/

using _03StorageFirebase.Services;
using _03StorageFirebase.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStorageService, StorageService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();