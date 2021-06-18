using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamDatabaseImplement.Migrations
{
    public partial class innitialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field1 = table.Column<int>(nullable: false),
                    Field2 = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field1 = table.Column<int>(nullable: false),
                    Field2 = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    MainClassId = table.Column<int>(nullable: false),
                    MainClassName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraClasses_MainClasses_MainClassId",
                        column: x => x.MainClassId,
                        principalTable: "MainClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraClasses_MainClassId",
                table: "ExtraClasses",
                column: "MainClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraClasses");

            migrationBuilder.DropTable(
                name: "MainClasses");
        }
    }
}
