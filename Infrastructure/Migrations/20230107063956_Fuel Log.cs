using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FuelLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelLog",
                schema: "HLMPMV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNo = table.Column<int>(type: "int", nullable: false),
                    DocumentNo = table.Column<int>(type: "int", nullable: false),
                    OpeningMeter = table.Column<float>(type: "real", nullable: false),
                    OpeningBalance = table.Column<float>(type: "real", nullable: false),
                    ShiftStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShiftEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartShiftTankerKm = table.Column<int>(type: "int", nullable: true),
                    EndShiftTankerKm = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fueler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Post_IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    Post_PostedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelTransactions",
                schema: "HLMPMV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuelStation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    AssetCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousReading = table.Column<int>(type: "int", nullable: false),
                    Reading = table.Column<int>(type: "int", nullable: false),
                    Driver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverQatarIdUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelTransactions_FuelLog_FuelLogId",
                        column: x => x.FuelLogId,
                        principalSchema: "HLMPMV",
                        principalTable: "FuelLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelTransactions_FuelLogId",
                schema: "HLMPMV",
                table: "FuelTransactions",
                column: "FuelLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelTransactions",
                schema: "HLMPMV");

            migrationBuilder.DropTable(
                name: "FuelLog",
                schema: "HLMPMV");
        }
    }
}
