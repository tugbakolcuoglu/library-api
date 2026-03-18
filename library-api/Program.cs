using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models.Entities;
using WebApplication2.Repository;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services;
using WebApplication2.Services.Interfaces;
using System.Linq;
using WebApplication2.Entities;

var builder = WebApplication.CreateBuilder(args);

// CONTROLLERS + JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// SWAGGER
builder.Services.AddEndpointsApiExplorer();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AUTOMAPPER
builder.Services.AddAutoMapper(typeof(Program));

// SERVICES
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStudentService, StudentService>();

// REPOSITORIES
builder.Services.AddScoped<IAssignmentHistoryRepository, AssignmentHistoryRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// CORS (frontend için)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// MIDDLEWARE
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();