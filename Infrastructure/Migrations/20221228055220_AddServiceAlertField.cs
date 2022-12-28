using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddServiceAlertField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Assigned",
                schema: "HLMPMV",
                table: "ServiceAlerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "HLMPMV",
                table: "ServiceAlerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "InActive",
                schema: "HLMPMV",
                table: "ServiceAlerts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assigned",
                schema: "HLMPMV",
                table: "ServiceAlerts");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "HLMPMV",
                table: "ServiceAlerts");

            migrationBuilder.DropColumn(
                name: "InActive",
                schema: "HLMPMV",
                table: "ServiceAlerts");
        }
    }
}
