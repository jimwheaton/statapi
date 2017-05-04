using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data;

namespace Data.Migrations
{
    [DbContext(typeof(StatContext))]
    [Migration("20170501204801_InitialModel")]
    partial class InitialModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.DataStaging", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AdvertiserCompetition");

                    b.Property<int>("Bing");

                    b.Property<string>("BingUrl");

                    b.Property<decimal>("CPC");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Device");

                    b.Property<int>("GlobalMonthlySearches");

                    b.Property<int>("Google");

                    b.Property<int>("GoogleBaseRank");

                    b.Property<string>("GoogleUrl");

                    b.Property<string>("Keyword");

                    b.Property<string>("Market");

                    b.Property<int>("RegionalMonthlySearches");

                    b.Property<string>("Site");

                    b.Property<string>("Tags");

                    b.Property<int>("Yahoo");

                    b.Property<string>("YahooUrl");

                    b.HasKey("Id");

                    b.ToTable("DataStaging");
                });

            modelBuilder.Entity("Models.Date", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.HasKey("Id");

                    b.ToTable("Dates");
                });

            modelBuilder.Entity("Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GlobalMonthlySearches");

                    b.Property<string>("Phrase");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("Models.KeywordRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bing");

                    b.Property<string>("BingUrl");

                    b.Property<int>("DateId");

                    b.Property<int>("DeviceId");

                    b.Property<int>("Google");

                    b.Property<int>("GoogleBaseRank");

                    b.Property<string>("GoogleUrl");

                    b.Property<int>("KeywordId");

                    b.Property<int>("MarketId");

                    b.Property<int>("SiteId");

                    b.Property<int>("Yahoo");

                    b.Property<string>("YahooUrl");

                    b.HasKey("Id");

                    b.HasIndex("DateId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("KeywordId");

                    b.HasIndex("MarketId");

                    b.HasIndex("SiteId");

                    b.ToTable("KeywordRanks");
                });

            modelBuilder.Entity("Models.Market", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("Models.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("Models.KeywordRank", b =>
                {
                    b.HasOne("Models.Date", "Date")
                        .WithMany()
                        .HasForeignKey("DateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Keyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Market", "Market")
                        .WithMany()
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
