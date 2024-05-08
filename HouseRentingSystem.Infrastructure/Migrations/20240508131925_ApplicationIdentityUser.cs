using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class ApplicationIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63118b15-b844-4df1-b9dd-920dcd26b231", "", "", "AQAAAAEAACcQAAAAEHmnx0wYX/NrXLGDciEROFdh0NYjKDkbdVP5O6ZgXfjxMioi9xC2rNZaBRR/H+gZJw==", "023f1f31-bc83-41ae-a076-994368a34ab3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7139096e-c407-4f33-859f-cd47054c05ee", "", "", "AQAAAAEAACcQAAAAEKl7BfAnPnlrVeGwgT7pAAu5yNSjGbpvdXG0cGwFZJTIovkXRzrNURlbB8ZgofnQlQ==", "0d2f3801-6813-4d0d-9b92-33d1088f7ad5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55f835f0-146c-4fd1-9eec-81d7e1a59a73", "AQAAAAEAACcQAAAAEE2UiyggvrJfhS7zKJsouADdPe/mN2WUgvQhCAs1u7bFSyhtg3Wlw4dJnPFXapFIxg==", "bd655bfc-e436-406c-b55b-6d0aaf43afd6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a64b8f48-eb75-467f-8108-041e25eecdde", "AQAAAAEAACcQAAAAEGmxGDJgU1epG0pbNd2MnvbNbk6q2B3Y/u2hGPUSks8jjEuCgFve1mI+kVAesg0UMg==", "7e3ad1e4-7a9e-443c-90da-e642d7218790" });
        }
    }
}
