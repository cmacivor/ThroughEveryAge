using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThroughEveryAge.Data.Migrations
{
    public partial class FileNameToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "LessonContents",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FileId",
                table: "LessonContents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
