using Agency.Application.Interfaces;
using Agency.Application.Services;
using Agency.Infrastructure;
using Agency.Infrastructure.Interfaces;
using Agency.Infrastructure.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AgencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
