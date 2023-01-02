using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SBAT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialRolesNormalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d25cb39c-c205-4091-a519-0926cb26b14a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6df6296-a7c7-409b-a8c3-048b86b667ca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2abaa385-af28-47d7-8e13-4066e7f39913", "13155046-8190-472f-ac7c-1ec4f6ce7acd", "User", "USER" },
                    { "79942054-7c48-4dc7-8261-d9c6adbad2e9", "718f4e57-60b8-422b-8828-11fa87f0985e", "MainMemeber", "MAINMEMEBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2abaa385-af28-47d7-8e13-4066e7f39913");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79942054-7c48-4dc7-8261-d9c6adbad2e9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d25cb39c-c205-4091-a519-0926cb26b14a", null, "User", null },
                    { "d6df6296-a7c7-409b-a8c3-048b86b667ca", null, "MainMemeber", null }
                });
        }
    }
}
