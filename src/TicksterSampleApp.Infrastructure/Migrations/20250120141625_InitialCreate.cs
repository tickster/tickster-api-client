using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicksterSampleApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicksterCampaignId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    TicksterCommunicationId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ActivationCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    TicksterInternalReference = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicksterUserRefNo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsCompany = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PostalAddressLineOne = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PostalAddressLineTwo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MobilePhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicksterEventId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TicksterProductionId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ProductionName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    HasTableReservation = table.Column<bool>(type: "INTEGER", nullable: false),
                    VenueId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportLog",
                columns: table => new
                {
                    LastTicksterCrmId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportLog", x => new { x.LastTicksterCrmId, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicksterVenueId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Latitude = table.Column<decimal>(type: "TEXT", precision: 8, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "TEXT", precision: 9, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicksterCrmId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                    TicksterPurchaseRefNo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Channel = table.Column<string>(type: "TEXT", nullable: false),
                    ToBePaidInRestaurantSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    DiscountCodeName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    DiscountCode = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    EogRequestCode = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    PrivacyRefNo = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    TermsRefNo = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VenueId = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurant_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoodsId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PurchaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ReceiptText = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    ArticleNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PriceIncVatAfterDiscount = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    VatPortion = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    VatPercent = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    Section = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Seat = table.Column<int>(type: "INTEGER", nullable: false),
                    Row = table.Column<int>(type: "INTEGER", nullable: false),
                    PartOfSeasonToken = table.Column<bool>(type: "INTEGER", nullable: false),
                    PartOfSeasonTokenGoodsId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PartOfTableReservation = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanBePlacedAtTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goods_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goods_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_EventId",
                table: "Goods",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_PurchaseId",
                table: "Goods",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_RestaurantId",
                table: "Goods",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CampaignId",
                table: "Purchase",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CustomerId",
                table: "Purchase",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_VenueId",
                table: "Restaurant",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "ImportLog");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Venue");
        }
    }
}
