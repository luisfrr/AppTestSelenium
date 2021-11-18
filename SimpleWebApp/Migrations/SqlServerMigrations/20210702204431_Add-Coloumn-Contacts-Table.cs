using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebApp.Migrations.SqlServerMigrations
{
    public partial class AddColoumnContactsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Entity",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entity",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Contacts");
        }
    }
}
