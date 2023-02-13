using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SBAT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectMemeberName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06fc884e-9f09-43a1-8dc2-7ae5bcd4c90d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f78bc85-f8ca-442b-ada1-2571ee0b83d2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d41d86c-8fe5-4e69-a00b-4991b6b7f3af", "373dbdd0-fc20-4306-8720-d7c41d5c6883", "User", "USER" },
                    { "5854b209-027e-42ab-a64d-8ea5613ade08", "ec2a3ddf-ba50-421b-ada6-4dbf3ebb7487", "MainMember", "MAINMEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d41d86c-8fe5-4e69-a00b-4991b6b7f3af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5854b209-027e-42ab-a64d-8ea5613ade08");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06fc884e-9f09-43a1-8dc2-7ae5bcd4c90d", "281b71c0-921d-47f8-86ee-41ba0bb4db2e", "User", "USER" },
                    { "9f78bc85-f8ca-442b-ada1-2571ee0b83d2", "62abc4e4-27ca-4b30-9e97-ff4454d24793", "MainMemeber", "MAINMEMEBER" }
                });
        }
    }
}
