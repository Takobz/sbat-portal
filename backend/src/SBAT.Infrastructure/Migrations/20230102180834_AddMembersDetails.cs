using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SBAT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMembersDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2abaa385-af28-47d7-8e13-4066e7f39913");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79942054-7c48-4dc7-8261-d9c6adbad2e9");

            migrationBuilder.AddColumn<string>(
                name: "Cellphone",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdentityNumber",
                table: "Members",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StreetLine",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SuburbOrTownLine",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkTelephone",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06fc884e-9f09-43a1-8dc2-7ae5bcd4c90d", "281b71c0-921d-47f8-86ee-41ba0bb4db2e", "User", "USER" },
                    { "9f78bc85-f8ca-442b-ada1-2571ee0b83d2", "62abc4e4-27ca-4b30-9e97-ff4454d24793", "MainMemeber", "MAINMEMEBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06fc884e-9f09-43a1-8dc2-7ae5bcd4c90d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f78bc85-f8ca-442b-ada1-2571ee0b83d2");

            migrationBuilder.DropColumn(
                name: "Cellphone",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StreetLine",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SuburbOrTownLine",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "WorkTelephone",
                table: "Members");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2abaa385-af28-47d7-8e13-4066e7f39913", "13155046-8190-472f-ac7c-1ec4f6ce7acd", "User", "USER" },
                    { "79942054-7c48-4dc7-8261-d9c6adbad2e9", "718f4e57-60b8-422b-8828-11fa87f0985e", "MainMemeber", "MAINMEMEBER" }
                });
        }
    }
}
