using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Data.Migrations
{
    public partial class newTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanDate",
                schema: "dbo",
                table: "U_Daily");

            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "dbo",
                table: "TBills");

            migrationBuilder.DropColumn(
                name: "SectorCode",
                schema: "dbo",
                table: "TBills");

            migrationBuilder.DropColumn(
                name: "SectorName",
                schema: "dbo",
                table: "TBills");

            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "AmortizationAmt",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "IndustryCode",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "SectorCode",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "TotalImpairment",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "dbo",
                table: "DailyUploads");

            migrationBuilder.DropColumn(
                name: "IndustryCode",
                schema: "dbo",
                table: "DailyUploads");

            migrationBuilder.DropColumn(
                name: "SectorCode",
                schema: "dbo",
                table: "DailyUploads");

            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "AmortizationAmt",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "IndustryCode",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "SectorCode",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "Staging",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "TotalImpairment",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "dbo",
                table: "CashFlows");

            migrationBuilder.DropColumn(
                name: "SectorCode",
                schema: "dbo",
                table: "CashFlows");

            migrationBuilder.RenameColumn(
                name: "UploadDate",
                schema: "dbo",
                table: "U_Daily",
                newName: "Date");

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                schema: "dbo",
                table: "U_Daily",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "TBills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                schema: "dbo",
                table: "TBills",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "StateUploads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                schema: "dbo",
                table: "StateUploads",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "DailyUploads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                schema: "dbo",
                table: "DailyUploads",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "CouponUploads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                schema: "dbo",
                table: "CouponUploads",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "CashFlows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                schema: "dbo",
                table: "CashFlows",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "dbo",
                table: "U_Daily");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "TBills");

            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "dbo",
                table: "TBills");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "dbo",
                table: "StateUploads");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "DailyUploads");

            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "dbo",
                table: "DailyUploads");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "dbo",
                table: "CouponUploads");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "CashFlows");

            migrationBuilder.DropColumn(
                name: "Rate",
                schema: "dbo",
                table: "CashFlows");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "dbo",
                table: "U_Daily",
                newName: "UploadDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LoanDate",
                schema: "dbo",
                table: "U_Daily",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "dbo",
                table: "TBills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectorCode",
                schema: "dbo",
                table: "TBills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectorName",
                schema: "dbo",
                table: "TBills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "dbo",
                table: "StateUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "AmortizationAmt",
                schema: "dbo",
                table: "StateUploads",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                schema: "dbo",
                table: "StateUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectorCode",
                schema: "dbo",
                table: "StateUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalImpairment",
                schema: "dbo",
                table: "StateUploads",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "dbo",
                table: "DailyUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                schema: "dbo",
                table: "DailyUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectorCode",
                schema: "dbo",
                table: "DailyUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "dbo",
                table: "CouponUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "AmortizationAmt",
                schema: "dbo",
                table: "CouponUploads",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                schema: "dbo",
                table: "CouponUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectorCode",
                schema: "dbo",
                table: "CouponUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Staging",
                schema: "dbo",
                table: "CouponUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalImpairment",
                schema: "dbo",
                table: "CouponUploads",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "dbo",
                table: "CashFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectorCode",
                schema: "dbo",
                table: "CashFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
