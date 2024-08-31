using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_description_from_offday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "master",
                table: "OffDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "master",
                table: "OffDays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
