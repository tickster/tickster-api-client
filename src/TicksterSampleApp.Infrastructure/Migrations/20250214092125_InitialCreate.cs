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
                name: "Campaigns",
                columns: table => new
                {
                    TicksterCampaignId = table.Column<string>(type: "varchar(10)", nullable: false),
                    TicksterCommunicationId = table.Column<string>(type: "varchar(10)", nullable: false),
                    ActivationCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    TicksterInternalReference = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => new { x.TicksterCampaignId, x.TicksterCommunicationId });
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TicksterUserRefNo = table.Column<string>(type: "varchar(10)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsCompany = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    PostalAddressLineOne = table.Column<string>(type: "varchar(255)", nullable: true),
                    PostalAddressLineTwo = table.Column<string>(type: "varchar(255)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    CountryCode = table.Column<string>(type: "char(2)", nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(200)", nullable: true),
                    MobilePhone = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventRestaurants",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRestaurants", x => new { x.EventId, x.RestaurantId });
                });

            migrationBuilder.CreateTable(
                name: "ImportLogs",
                columns: table => new
                {
                    LastTicksterCrmId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportLogs", x => new { x.LastTicksterCrmId, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "PurchaseCampaign",
                columns: table => new
                {
                    PurchaseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CampaignId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseCampaign", x => new { x.PurchaseId, x.CampaignId });
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantName = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TicksterVenueId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: false),
                    CountryCode = table.Column<string>(type: "char(2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(6, 2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(6, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TicksterCrmId = table.Column<int>(type: "INTEGER", nullable: false),
                    TicksterPurchaseRefNo = table.Column<string>(type: "varchar(10)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "char(3)", nullable: false),
                    Channel = table.Column<string>(type: "varchar(20)", nullable: false),
                    ToBePaidInRestaurantSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    DiscountCodeName = table.Column<string>(type: "varchar(255)", nullable: false),
                    DiscountCode = table.Column<string>(type: "varchar(255)", nullable: false),
                    EogRequestCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    PrivacyRefNo = table.Column<string>(type: "varchar(5)", nullable: false),
                    TermsRefNo = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VenueId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TicksterEventId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TicksterProductionId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ProductionName = table.Column<string>(type: "varchar(255)", nullable: false),
                    HasTableReservation = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TicksterEventId = table.Column<string>(type: "TEXT", nullable: false),
                    TicksterGoodsId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    ReceiptText = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    ArticleNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    PriceIncVatAfterDiscount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    VatPortion = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    VatPercent = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Section = table.Column<string>(type: "varchar(50)", nullable: false),
                    Seat = table.Column<int>(type: "INTEGER", nullable: false),
                    Row = table.Column<int>(type: "INTEGER", nullable: false),
                    PartOfSeasonToken = table.Column<bool>(type: "INTEGER", nullable: false),
                    PartOfSeasonTokenGoodsId = table.Column<string>(type: "varchar(50)", nullable: false),
                    PartOfTableReservation = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanBePlacedAtTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goods_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_EventId",
                table: "Goods",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_PurchaseId",
                table: "Goods",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportLogs_ApiKey",
                table: "ImportLogs",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CustomerId",
                table: "Purchases",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "EventRestaurants");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "ImportLogs");

            migrationBuilder.DropTable(
                name: "PurchaseCampaign");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
