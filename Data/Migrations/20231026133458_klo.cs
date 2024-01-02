using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class klo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APP_Reports2",
                schema: "dbo",
                columns: table => new
                {
                    NO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Report_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Checkbox = table.Column<int>(type: "int", nullable: true),
                    Datalink1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datalink2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_Reports2", x => x.NO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APP_Reports2",
                schema: "dbo");
        }
    }
}
