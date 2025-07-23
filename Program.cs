using EmployeeSync.Data;
using EmployeeSync.Interfaces;
using EmployeeSync.Repositories;
using EmployeeSync.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using EmployeeSync.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDTOValidator>(); // Reflection done here
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


