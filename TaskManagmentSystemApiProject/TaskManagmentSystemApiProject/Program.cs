using Microsoft.EntityFrameworkCore;
using TaskManagmentSystemApiProject.Data;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevCors", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
    options.AddPolicy("ProdCors", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("https://myproductionsite.com")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
builder.Services.AddDbContext<TaskDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
}
else
{
    app.UseCors("ProdCors");
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
