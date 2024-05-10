using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class AdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80a4ea59-9947-4e7f-837f-2ec3f3d187c8", "AQAAAAEAACcQAAAAEFiK4u+CSlIMl5icUQW8QV/mwXwTyu/p0TYZuirj34APP6d+rjAhG1FkghNNX/GgyQ==", "85c1a210-fa6b-4240-8478-7f033d4f138d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4a8939a-bee2-4907-a21e-28d730cc4430", "AQAAAAEAACcQAAAAEJO72jyVHaEn9NhpspFKu5VkIgKYyoePhvb6S89Og3xNqVBbfdK1Nj9J+nTcXYkQMQ==", "ba4a8763-e808-453d-8645-5bb38ef84bfd" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e43ce836-997d-4927-ac59-74e8c41bbfd3", 0, "68238099-1119-4732-a763-abdd1d1f7d81", "admin@gmail.com", false, "Great", "Admin", false, null, "ADMIN@MAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEAqBrAZ6812f2cwOyo64uzr4+wNbBhhxnVBg0GOwHudlcomKCX5704MagCqiQ/Z5SA==", null, false, "3ac659a5-f15e-425b-969a-b638c35e216b", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 9, "+359888888887", "e43ce836-997d-4927-ac59-74e8c41bbfd3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e43ce836-997d-4927-ac59-74e8c41bbfd3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63118b15-b844-4df1-b9dd-920dcd26b231", "AQAAAAEAACcQAAAAEHmnx0wYX/NrXLGDciEROFdh0NYjKDkbdVP5O6ZgXfjxMioi9xC2rNZaBRR/H+gZJw==", "023f1f31-bc83-41ae-a076-994368a34ab3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7139096e-c407-4f33-859f-cd47054c05ee", "AQAAAAEAACcQAAAAEKl7BfAnPnlrVeGwgT7pAAu5yNSjGbpvdXG0cGwFZJTIovkXRzrNURlbB8ZgofnQlQ==", "0d2f3801-6813-4d0d-9b92-33d1088f7ad5" });
        }
    }
}
