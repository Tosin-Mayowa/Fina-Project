using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APP_Reports",
                schema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APP_Reports",
                schema: "dbo",
                columns: table => new
                {
                    NO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Checkbox = table.Column<int>(type: "int", nullable: true),
                    Datalink1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datalink2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report_ID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_Reports", x => x.NO);
                });
        }
    }
}
