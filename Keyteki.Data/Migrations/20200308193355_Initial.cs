namespace Keyteki.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "PatreonToken",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    Expiry = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatreonToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Roles",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    RegisteredDate = table.Column<DateTime>(),
                    LastLoginDate = table.Column<DateTime>(),
                    RegisterIp = table.Column<string>(nullable: true),
                    EmailHash = table.Column<string>(nullable: true),
                    Settings_Background = table.Column<string>(nullable: true, defaultValue: "BG1"),
                    Settings_CardSize = table.Column<string>(nullable: true, defaultValue: "normal"),
                    Disabled = table.Column<bool>(),
                    CustomData = table.Column<string>(nullable: true),
                    PatreonTokenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        "FK_Users_PatreonToken_PatreonTokenId",
                        x => x.PatreonTokenId,
                        "PatreonToken",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "BlockListEntry",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(),
                    BlockedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockListEntry", x => x.Id);
                    table.ForeignKey(
                        "FK_BlockListEntry_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "LobbyMessage",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageText = table.Column<string>(nullable: true),
                    SenderId = table.Column<int>(),
                    MessageDateTime = table.Column<DateTime>(),
                    Removed = table.Column<bool>(),
                    RemovedById = table.Column<int>(),
                    RemovedDateTime = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbyMessage", x => x.Id);
                    table.ForeignKey(
                        "FK_LobbyMessage_Users_RemovedById",
                        x => x.RemovedById,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_LobbyMessage_Users_SenderId",
                        x => x.SenderId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "News",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePublished = table.Column<DateTime>(),
                    PosterId = table.Column<int>(),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        "FK_News_Users_PosterId",
                        x => x.PosterId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "UserRoles",
                table => new
                {
                    UserId = table.Column<int>(),
                    RoleId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        "FK_UserRoles_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_UserRoles_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_BlockListEntry_UserId",
                "BlockListEntry",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_LobbyMessage_RemovedById",
                "LobbyMessage",
                "RemovedById");

            migrationBuilder.CreateIndex(
                "IX_LobbyMessage_SenderId",
                "LobbyMessage",
                "SenderId");

            migrationBuilder.CreateIndex(
                "IX_News_PosterId",
                "News",
                "PosterId");

            migrationBuilder.CreateIndex(
                "IX_UserRoles_RoleId",
                "UserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "IX_Users_PatreonTokenId",
                "Users",
                "PatreonTokenId",
                unique: true,
                filter: "[PatreonTokenId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BlockListEntry");

            migrationBuilder.DropTable(
                "LobbyMessage");

            migrationBuilder.DropTable(
                "News");

            migrationBuilder.DropTable(
                "UserRoles");

            migrationBuilder.DropTable(
                "Roles");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "PatreonToken");
        }
    }
}
