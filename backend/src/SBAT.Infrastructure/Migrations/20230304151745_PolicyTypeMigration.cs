using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SBAT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PolicyTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d41d86c-8fe5-4e69-a00b-4991b6b7f3af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5854b209-027e-42ab-a64d-8ea5613ade08");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Policies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "026c1407-4c16-42f3-a3eb-54d0598786a1", "8f43ee16-3050-400c-b866-c008a8e5080d", "User", "USER" },
                    { "aca1c768-6ac5-497d-9984-592d349a03f7", "e9f6beca-2f2f-4fcb-90d9-871401f63af1", "MainMember", "MAINMEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "026c1407-4c16-42f3-a3eb-54d0598786a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aca1c768-6ac5-497d-9984-592d349a03f7");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Policies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d41d86c-8fe5-4e69-a00b-4991b6b7f3af", "373dbdd0-fc20-4306-8720-d7c41d5c6883", "User", "USER" },
                    { "5854b209-027e-42ab-a64d-8ea5613ade08", "ec2a3ddf-ba50-421b-ada6-4dbf3ebb7487", "MainMember", "MAINMEMBER" }
                });
        }
    }
}
