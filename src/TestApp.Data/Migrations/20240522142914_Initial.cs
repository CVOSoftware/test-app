using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Use = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Family = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<byte>(type: "smallint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DoB = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoB",
                table: "Patients",
                column: "DoB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
