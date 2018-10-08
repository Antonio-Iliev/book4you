﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("136c7bfc-c5b9-41a3-909f-b20fed4f188f"),
                column: "BooksInStore",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("354690b9-d516-454e-9651-15d7d9e504e1"),
                column: "BooksInStore",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("41e3ccaa-144c-4f8f-9740-61d73da359fb"),
                column: "BooksInStore",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("73a1a1de-a54f-4578-a51f-6fe896f5c630"),
                column: "BooksInStore",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b01b4f03-20ea-4dd6-a0a2-3bab3db66cbf"),
                column: "BooksInStore",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("bd7fc284-bc29-43be-b9ac-93f313f9ccfb"),
                column: "BooksInStore",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e90de0c0-9666-46d2-a015-f1d25c6e7c20"),
                column: "BooksInStore",
                value: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("136c7bfc-c5b9-41a3-909f-b20fed4f188f"),
                column: "BooksInStore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("354690b9-d516-454e-9651-15d7d9e504e1"),
                column: "BooksInStore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("41e3ccaa-144c-4f8f-9740-61d73da359fb"),
                column: "BooksInStore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("73a1a1de-a54f-4578-a51f-6fe896f5c630"),
                column: "BooksInStore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b01b4f03-20ea-4dd6-a0a2-3bab3db66cbf"),
                column: "BooksInStore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("bd7fc284-bc29-43be-b9ac-93f313f9ccfb"),
                column: "BooksInStore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e90de0c0-9666-46d2-a015-f1d25c6e7c20"),
                column: "BooksInStore",
                value: 0);
        }
    }
}
