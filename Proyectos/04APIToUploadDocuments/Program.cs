using _04APIToUploadDocuments.Services;
using _04APIToUploadDocuments.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string CORS = "MyPolicy";

builder.Services.AddCors(options => {
    options.AddPolicy(name: CORS, builderCors => {
        builderCors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

app.UseCors(CORS);

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();