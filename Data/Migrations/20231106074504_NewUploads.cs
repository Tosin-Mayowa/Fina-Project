using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class NewUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashFlows",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CouponUploads",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmortizationAmt = table.Column<double>(type: "float", nullable: false),
                    TotalImpairment = table.Column<double>(type: "float", nullable: false),
                    Staging = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyUploads",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanUpload2",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmortizationAmt = table.Column<double>(type: "float", nullable: false),
                    TotalImpairment = table.Column<double>(type: "float", nullable: false),
                    Staging = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Performance = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanUpload2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateUploads",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmortizationAmt = table.Column<double>(type: "float", nullable: false),
                    TotalImpairment = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBills",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashFlows",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CouponUploads",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DailyUploads",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoanUpload2",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StateUploads",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TBills",
                schema: "dbo");
        }
    }
}
