using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace treni_contact.Migrations
{
    public partial class inittwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "contacts",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "contacts");
        }
    }
}
