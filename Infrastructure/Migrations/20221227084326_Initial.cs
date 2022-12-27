using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HLMPMV");

            migrationBuilder.CreateTable(
                name: "LogSheets",
                schema: "HLMPMV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNo = table.Column<int>(type: "int", nullable: false),
                    ShiftStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartShiftTankerKm = table.Column<int>(type: "int", nullable: false),
                    EndShiftTankerKm = table.Column<int>(type: "int", nullable: true),
                    StartShiftMeterReading = table.Column<int>(type: "int", nullable: false),
                    EndShiftMeterReading = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fueler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LvStationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Post_IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    Post_PostedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAlerts",
                schema: "HLMPMV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAlerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogSheetDetails",
                schema: "HLMPMV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorDriver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reading = table.Column<int>(type: "int", nullable: false),
                    PreviousReading = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    DriverQatarIdUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentSMUUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TankMeterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogSheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSheetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogSheetDetails_LogSheets_LogSheetId",
                        column: x => x.LogSheetId,
                        principalSchema: "HLMPMV",
                        principalTable: "LogSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAlertDetails",
                schema: "HLMPMV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KmAlert = table.Column<int>(type: "int", nullable: false),
                    KmInterval = table.Column<int>(type: "int", nullable: false),
                    ServiceAlertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAlertDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAlertDetails_ServiceAlerts_ServiceAlertId",
                        column: x => x.ServiceAlertId,
                        principalSchema: "HLMPMV",
                        principalTable: "ServiceAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogSheetDetails_LogSheetId",
                schema: "HLMPMV",
                table: "LogSheetDetails",
                column: "LogSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAlertDetails_ServiceAlertId",
                schema: "HLMPMV",
                table: "ServiceAlertDetails",
                column: "ServiceAlertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogSheetDetails",
                schema: "HLMPMV");

            migrationBuilder.DropTable(
                name: "ServiceAlertDetails",
                schema: "HLMPMV");

            migrationBuilder.DropTable(
                name: "LogSheets",
                schema: "HLMPMV");

            migrationBuilder.DropTable(
                name: "ServiceAlerts",
                schema: "HLMPMV");
        }
    }
}
