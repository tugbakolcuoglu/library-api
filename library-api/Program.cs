using Microsoft.EntityFrameworkCore; 
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repository;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services;
using WebApplication2.Services.Interfaces;

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

builder.Services.AddAutoMapper(typeof(MappingProfile));// AutoMapper registration


//LibraryService'ı bağımlılık enjeksiyonuna ekliyoruz, böylece controller'larda kullanabiliriz.

builder.Services.AddScoped<ILibraryService,LibraryService>();
builder.Services.AddScoped<IAssignmentHistoryRepository, AssignmentHistoryRepository>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();


builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
