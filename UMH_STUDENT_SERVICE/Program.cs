using Microsoft.EntityFrameworkCore;
using UMH_STUDENT_SERVICE._02_LogicLayer;
using UMH_STUDENT_SERVICE._03_BussinesLayer;
using UMH_STUDENT_SERVICE.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



builder.Services.AddEntityFrameworkNpgsql()
.AddDbContext<appDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Registrer interfaces
builder.Services.AddScoped<ILlUser, LlUser>();
builder.Services.AddScoped<IBlUser, BlUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

// Scaffold-DbContext "Server=localhost;port=5433;Username=postgres;Password=root;Database=control_class_student_umh" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -ContextDir Context -Context  appDbContext -Force
