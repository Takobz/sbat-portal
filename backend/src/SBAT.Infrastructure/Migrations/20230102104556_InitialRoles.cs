﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SBAT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d25cb39c-c205-4091-a519-0926cb26b14a", null, "User", null },
                    { "d6df6296-a7c7-409b-a8c3-048b86b667ca", null, "MainMemeber", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d25cb39c-c205-4091-a519-0926cb26b14a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6df6296-a7c7-409b-a8c3-048b86b667ca");
        }
    }
}
