using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PostgresSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed Users
            migrationBuilder.InsertData(
                schema: "master",
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "EmailAddress", "PasswordHash", "UserOrCustomer" },
                values: new object[,]
                {
            { Guid.Parse("71b423ff-4355-420a-aab8-5a06bedb2c77"), "Erlangga", "Customer", "erlangga@customer.com", "UNkpe4Q4yFv8kShBEnBaO0yyybEaPI60PafsI3djCSiy7/TvWdUCxVoLNEFVZGBxjsuYR/mg5mPhX3JS/1wsmQ==", "CUSTOMER" },
            { Guid.Parse("4df6a118-3d12-49c1-a346-b058f026187e"), "Erlangga", "Staff", "erlangga@staff.com", "UNkpe4Q4yFv8kShBEnBaO0yyybEaPI60PafsI3djCSiy7/TvWdUCxVoLNEFVZGBxjsuYR/mg5mPhX3JS/1wsmQ==", "STAFF" }
                });

            // Seed Configurations
            migrationBuilder.InsertData(
                schema: "master",
                table: "Configurations",
                columns: new[] { "Id", "PropertyName", "Value" },
                values: new object[,]
                {
            { Guid.Parse("d46df057-e89a-4517-8dab-de4370913c83"), "MAX_APPOINTMENTS", "5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally remove the seeded data if rolling back
            migrationBuilder.DeleteData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[]
                {
            Guid.Parse("71b423ff-4355-420a-aab8-5a06bedb2c77"),
            Guid.Parse("4df6a118-3d12-49c1-a346-b058f026187e")
                });

            migrationBuilder.DeleteData(
                schema: "master",
                table: "Configurations",
                keyColumn: "Id",
                keyValues: new object[]
                {
            Guid.Parse("d46df057-e89a-4517-8dab-de4370913c83")
                });
        }
    }
}
