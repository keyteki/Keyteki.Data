﻿namespace Keyteki.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Autogenerated file")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Autogenerated file")]
    public partial class PatreonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "PatreonTokenId",
                "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                "PatreonToken",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(),
                    Token = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    Expiry = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatreonToken", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                "IX_Users_PatreonTokenId",
                "Users",
                "PatreonTokenId",
                unique: true,
                filter: "[PatreonTokenId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                "FK_Users_PatreonToken_PatreonTokenId",
                "Users",
                "PatreonTokenId",
                "PatreonToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_PatreonToken_PatreonTokenId",
                "Users");

            migrationBuilder.DropTable(
                "PatreonToken");

            migrationBuilder.DropIndex(
                "IX_Users_PatreonTokenId",
                "Users");

            migrationBuilder.DropColumn(
                "PatreonTokenId",
                "Users");
        }
    }
}
