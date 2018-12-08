using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccount.Migrations
{
    public partial class fourMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Users",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
