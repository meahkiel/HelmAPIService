using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changekminterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KmInteraval",
                schema: "HLMPMV",
                table: "ServiceLogs",
                newName: "KmInterval");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KmInterval",
                schema: "HLMPMV",
                table: "ServiceLogs",
                newName: "KmInteraval");
        }
    }
}
