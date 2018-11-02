using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class NormalaizeRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", "7944e193-64c1-42b9-a85e-f04309a7d197" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "0fc0a00d-3dd2-47d1-b7ac-57abfa295ba7", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "907a9f1b-5fc4-4d7b-b7fc-fb814b96cbe5", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "5c0fb7fa-a412-4116-847b-cf917e886aad", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAEJnMpEx5//f45C29JFrQjTGfWba0VkoVSVCuEJHStGW+xnS/GQ/1ukFDPRg24FCgPQ==", "+111111111", true, "e91a560b-850b-42dd-aa6f-cc025c73d64e", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", "5c0fb7fa-a412-4116-847b-cf917e886aad" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "279faf65-cebb-4e7b-b5f4-ac5bb4fa5865", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "ec14078c-b475-43d5-8a42-d7d24947e8c0", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "7944e193-64c1-42b9-a85e-f04309a7d197", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAEGVxGNMaIGZJnfqtmLu/7Qwwy+NeZFPQuLDYWQrnyqAZtSPZwz5+sXt8vZnmrHUNqQ==", "+111111111", true, "1c521d2b-a22e-4399-b4aa-514d14253f6e", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "68c0e422-b1ee-412d-bac9-23eee1da6d74", "1" });
        }
    }
}
