using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "StreetAddress", "TownId" },
                values: new object[] { 1, "AdminAddres", 1 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "279faf65-cebb-4e7b-b5f4-ac5bb4fa5865", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "ec14078c-b475-43d5-8a42-d7d24947e8c0", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "7944e193-64c1-42b9-a85e-f04309a7d197", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAEGVxGNMaIGZJnfqtmLu/7Qwwy+NeZFPQuLDYWQrnyqAZtSPZwz5+sXt8vZnmrHUNqQ==", "+111111111", true, "1c521d2b-a22e-4399-b4aa-514d14253f6e", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2", "ec14078c-b475-43d5-8a42-d7d24947e8c0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1", "279faf65-cebb-4e7b-b5f4-ac5bb4fa5865" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", "7944e193-64c1-42b9-a85e-f04309a7d197" });

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
