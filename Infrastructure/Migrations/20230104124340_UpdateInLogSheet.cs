using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateInLogSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentSMUUrl",
                schema: "HLMPMV",
                table: "LogSheetDetails");

            migrationBuilder.RenameColumn(
                name: "LvStationCode",
                schema: "HLMPMV",
                table: "LogSheets",
                newName: "StationCode");

            migrationBuilder.RenameColumn(
                name: "TankMeterUrl",
                schema: "HLMPMV",
                table: "LogSheetDetails",
                newName: "TransactionType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StationCode",
                schema: "HLMPMV",
                table: "LogSheets",
                newName: "LvStationCode");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                schema: "HLMPMV",
                table: "LogSheetDetails",
                newName: "TankMeterUrl");

            migrationBuilder.AddColumn<string>(
                name: "CurrentSMUUrl",
                schema: "HLMPMV",
                table: "LogSheetDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
