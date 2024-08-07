using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnIdAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAuth",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAuth",
                table: "Users");
        }
    }
}
