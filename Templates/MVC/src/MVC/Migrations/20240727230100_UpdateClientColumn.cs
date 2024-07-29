using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Clients",
                type: "int",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(2)",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Clients",
                type: "CHAR(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 2);
        }
    }
}
