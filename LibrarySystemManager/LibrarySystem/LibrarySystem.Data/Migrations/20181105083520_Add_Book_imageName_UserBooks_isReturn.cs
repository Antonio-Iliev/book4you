using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class Add_Book_imageName_UserBooks_isReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", "5c0fb7fa-a412-4116-847b-cf917e886aad" });

            migrationBuilder.AddColumn<bool>(
                name: "IsReturn",
                table: "UsersBooks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Books",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f893566b-438b-490d-80fc-5bf8c87457d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5f31ece4-28de-4eb7-8c31-c4f1e77df320");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f9592391-1af6-4f47-8d42-bf92f0dac9be", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "a2605622-e86f-415e-bc82-d0804760022b", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAENxNGC7ala1ZX6ALCjWnEkwt1fYMinU5L6MkSXJ8BP7ZEN2eUWudZAi/HV0WyKr1KA==", "+111111111", true, "ea897287-6139-4b23-8969-b75145b0ce9f", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "f9592391-1af6-4f47-8d42-bf92f0dac9be", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "f9592391-1af6-4f47-8d42-bf92f0dac9be", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f9592391-1af6-4f47-8d42-bf92f0dac9be", "a2605622-e86f-415e-bc82-d0804760022b" });

            migrationBuilder.DropColumn(
                name: "IsReturn",
                table: "UsersBooks");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0fc0a00d-3dd2-47d1-b7ac-57abfa295ba7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "907a9f1b-5fc4-4d7b-b7fc-fb814b96cbe5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddOnDate", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "5c0fb7fa-a412-4116-847b-cf917e886aad", "admin@mail.com", true, "Admin", false, "AdminLastName", false, null, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAEJnMpEx5//f45C29JFrQjTGfWba0VkoVSVCuEJHStGW+xnS/GQ/1ukFDPRg24FCgPQ==", "+111111111", true, "e91a560b-850b-42dd-aa6f-cc025c73d64e", false, "adminMain" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "92a3cc2e-923c-4c6a-94c4-d60c9696427d", "1" });
        }
    }
}
