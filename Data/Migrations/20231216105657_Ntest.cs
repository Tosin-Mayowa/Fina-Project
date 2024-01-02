using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class Ntest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MBR300",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColNum = table.Column<int>(type: "int", nullable: false),
                    LineCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MBR300", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MBR302",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cash_in_Vault_Coins = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cash_out_Vault_Notes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Teller_Coins = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Teller_Notes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Foreign_Coins = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Foreign_Notes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ATM_Balances = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Others = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MBR302", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MBR300",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MBR302",
                schema: "dbo");
        }
    }
}
