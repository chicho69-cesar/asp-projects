namespace _06RutasDinamicas {
    public class StartUp {
        public static WebApplication InitializeApplication(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            ConfigureMiddlewares(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder) {
            builder.Services.AddControllersWithViews();
        }

        private static void ConfigureMiddlewares(WebApplication app) {
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("Home/Error?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );


            /*Configuramos los parametros Catch All o rutas dinamicas*/
            app.MapControllerRoute(
                name: "profile",
                pattern: "{*profileUser}",
                defaults: new { controller = "Home", action = "Profile" }
            );
        }
    }
}