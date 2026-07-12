using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllItems.Translation.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiUsageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YearMonth = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterCount = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUsageRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentenceTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceLanguage = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetLanguage = table.Column<int>(type: "INTEGER", nullable: false),
                    NormalizedSourceText = table.Column<string>(type: "TEXT", nullable: false),
                    TranslatedText = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentenceTranslations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<int>(type: "INTEGER", nullable: false),
                    NormalizedText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordEntryId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetLanguage = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetText = table.Column<string>(type: "TEXT", nullable: false),
                    IsPreferred = table.Column<bool>(type: "INTEGER", nullable: false),
                    UsageCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordTranslations_WordEntries_WordEntryId",
                        column: x => x.WordEntryId,
                        principalTable: "WordEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiUsageRecords_YearMonth",
                table: "ApiUsageRecords",
                column: "YearMonth",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentenceTranslations_SourceLanguage_TargetLanguage_NormalizedSourceText",
                table: "SentenceTranslations",
                columns: new[] { "SourceLanguage", "TargetLanguage", "NormalizedSourceText" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordEntries_Language_NormalizedText",
                table: "WordEntries",
                columns: new[] { "Language", "NormalizedText" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordTranslations_WordEntryId_TargetLanguage",
                table: "WordTranslations",
                columns: new[] { "WordEntryId", "TargetLanguage" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUsageRecords");

            migrationBuilder.DropTable(
                name: "SentenceTranslations");

            migrationBuilder.DropTable(
                name: "WordTranslations");

            migrationBuilder.DropTable(
                name: "WordEntries");
        }
    }
}
