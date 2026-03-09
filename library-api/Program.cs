using Microsoft.EntityFrameworkCore; 
using WebApplication2.Data;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; 
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//AppDbContext'i SQL Server veritabanına bağlamak için yapılandırıyoruz, bağlantı dizesi appsettings.json dosyasından alınır.

builder.Services.AddScoped<LibraryService>();
//LibraryService'ı bağımlılık enjeksiyonuna ekliyoruz, böylece controller'larda kullanabiliriz.

builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
