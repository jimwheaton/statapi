using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataStaging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertiserCompetition = table.Column<decimal>(nullable: false),
                    Bing = table.Column<int>(nullable: false),
                    BingUrl = table.Column<string>(nullable: true),
                    CPC = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Device = table.Column<string>(nullable: true),
                    GlobalMonthlySearches = table.Column<int>(nullable: false),
                    Google = table.Column<int>(nullable: false),
                    GoogleBaseRank = table.Column<int>(nullable: false),
                    GoogleUrl = table.Column<string>(nullable: true),
                    Keyword = table.Column<string>(nullable: true),
                    Market = table.Column<string>(nullable: true),
                    RegionalMonthlySearches = table.Column<int>(nullable: false),
                    Site = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Yahoo = table.Column<int>(nullable: false),
                    YahooUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStaging", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GlobalMonthlySearches = table.Column<int>(nullable: false),
                    Phrase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeywordRanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bing = table.Column<int>(nullable: false),
                    BingUrl = table.Column<string>(nullable: true),
                    DateId = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false),
                    Google = table.Column<int>(nullable: false),
                    GoogleBaseRank = table.Column<int>(nullable: false),
                    GoogleUrl = table.Column<string>(nullable: true),
                    KeywordId = table.Column<int>(nullable: false),
                    MarketId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    Yahoo = table.Column<int>(nullable: false),
                    YahooUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeywordRanks_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordRanks_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordRanks_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordRanks_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordRanks_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeywordRanks_DateId",
                table: "KeywordRanks",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordRanks_DeviceId",
                table: "KeywordRanks",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordRanks_KeywordId",
                table: "KeywordRanks",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordRanks_MarketId",
                table: "KeywordRanks",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordRanks_SiteId",
                table: "KeywordRanks",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataStaging");

            migrationBuilder.DropTable(
                name: "KeywordRanks");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
