using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class NewTab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanUploads1",
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
                    Performance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Performance1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Performance2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Performance3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Performance4 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanUploads1", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanUploads1",
                schema: "dbo");
        }
    }
}
