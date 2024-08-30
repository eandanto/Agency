using Agency.Application.Interfaces;
using Agency.Application.Services;
using Agency.Infrastructure;
using Agency.Infrastructure.Interfaces;
using Agency.Infrastructure.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AgencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Register services
    containerBuilder.RegisterType<UserService>().As<IUserService>();
    containerBuilder.RegisterType<ConfigurationService>().As<IConfigurationService>();
    containerBuilder.RegisterType<OffDayService>().As<IOffDayService>();
    containerBuilder.RegisterType<AppointmentService>().As<IAppointmentService>();

    // Register repositories
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
    containerBuilder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>();
    containerBuilder.RegisterType<OffDayRepository>().As<IOffDayRepository>();
    containerBuilder.RegisterType<AppointmentRepository>().As<IAppointmentRepository>();
});

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable Swagger in development mode
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agency API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
