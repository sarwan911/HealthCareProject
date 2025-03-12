using System.Text;
using HealthCareProject.Authentication;
using HealthCareProject.Data;
using HealthCareProject.Models;
using HealthCareProject.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebapiProject.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

var key = builder.Configuration["JwtSettings:Key"];
builder.Services.AddScoped<IAuth>(provider =>
{
    var context = provider.GetRequiredService<Context>();
    return new Auth(key, context);
});
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
builder.Services.AddScoped<IDocAvailabilityRepository, DocAvailabilityRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
