using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Agency.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AgencyDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AgencyDbContext>>()))
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            Id = Guid.Parse("71b423ff-4355-420a-aab8-5a06bedb2c77"),
                            FirstName = "Erlangga",
                            LastName = "Customer",
                            EmailAddress = "erlangga@customer.com",
                            PasswordHash = "UNkpe4Q4yFv8kShBEnBaO0yyybEaPI60PafsI3djCSiy7/TvWdUCxVoLNEFVZGBxjsuYR/mg5mPhX3JS/1wsmQ==",
                            UserOrCustomer = "CUSTOMER"
                        },
                        new User
                        {
                            Id = Guid.Parse("4df6a118-3d12-49c1-a346-b058f026187e"),
                            FirstName = "Erlangga",
                            LastName = "Staff",
                            EmailAddress = "erlangga@staff.com",
                            PasswordHash = "UNkpe4Q4yFv8kShBEnBaO0yyybEaPI60PafsI3djCSiy7/TvWdUCxVoLNEFVZGBxjsuYR/mg5mPhX3JS/1wsmQ==",
                            UserOrCustomer = "STAFF"
                        }
                    );

                    context.SaveChanges();
                }

                if (!context.Configurations.Any())
                {
                    context.Configurations.AddRange(
                        new Configuration
                        {
                            Id = Guid.Parse("d46df057-e89a-4517-8dab-de4370913c83"),
                            PropertyName = "MAX_APPOINTMENTS",
                            Value = "5"
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
