using Tickster.Api.Models.Crm;

namespace Tickster.Api.Test.Unit.Crm;
public class TestCrmRequest : RequestTestBase
{
    [Fact]
    public async Task GetCrmPurchasesAsync_ValidateResponse()
    {
        // Arrange
        SetupMockResponse("crm-purchases-ok.json");

        // Act

        var result = await TicksterClient.GetCrmPurchasesAsync(1);

        // Assert

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());

        // Assert data conversions

        var purchase = result.First();
        Assert.Equal(6301344, purchase.CrmId);
        Assert.Equal("ATW3ZA7GR8XXXXX", purchase.EogRequestCode);
        Assert.Equal(PurchaseStatus.Completed, purchase.Status);
        Assert.Equal("https://booking.payment/url", purchase.BookingPaymentUrl);
        Assert.Equal("XNZ9PH1XX1", purchase.PurchaseRefno);
        Assert.Equal("XNZ9PH1XX2", purchase.ParentPurchaseRefno);
        Assert.Equal(new DateTime(2023, 11, 24, 8, 39, 52, 823, DateTimeKind.Utc), purchase.CreatedUtc);
        Assert.Equal(new DateTime(2023, 11, 24, 8, 45, 35, 550, DateTimeKind.Utc), purchase.LastUpdatedUtc);
        Assert.Equal("SEK", purchase.Currency);
        Assert.Equal("ATL77", purchase.PrivacyRefNo);
        Assert.Equal("9Z075", purchase.TermsRefNo);
        Assert.Equal(Channel.Webshop, purchase.Channel);
        Assert.Equal("UK3A19XX1", purchase.UserRefNo);
        Assert.Equal("John", purchase.FirstName);
        Assert.Equal("Exempelsson", purchase.LastName);
        Assert.Equal("Addressrad 1", purchase.PostalAddressLineOne);
        Assert.Equal("Andra raden", purchase.PostalAddressLineTwo);
        Assert.Equal("123 45", purchase.Zipcode);
        Assert.Equal("Exampelbo", purchase.City);
        Assert.Equal("XX", purchase.CountryCode);
        Assert.Equal("Example Industries AB", purchase.CompanyName);
        Assert.True(purchase.IsCompany);
        Assert.Equal("+467123456878", purchase.MobilePhoneNo);
        Assert.Equal("john.exempelsson@example.com", purchase.Email);
        Assert.True(purchase.AcceptInfo);
        Assert.Equal("xxxxx", purchase.IdNo);
        Assert.False(purchase.ToBePaidInRestaurantSystem);
        Assert.Equal("Cheaper", purchase.DiscountCodeName);
        Assert.Equal("ABC123", purchase.DiscountCode);

        Assert.Empty(purchase.Campaigns);

        Assert.IsType<List<Event>>(purchase.Events);
        Assert.Equal(2, purchase.Events.Count);

        var firstEvent = purchase.Events.First();

        Assert.Equal("6fx92hehmwaXXXX", firstEvent.Id);
        Assert.Equal("Bordsbokning 1", firstEvent.Name);
        Assert.Equal(DateTime.Parse("2024-04-17T13:00:00+02:00"), firstEvent.Start);
        Assert.Equal(DateTime.Parse("2025-11-30T03:00:00+01:00"), firstEvent.End);
        Assert.Equal(DateTime.Parse("2024-10-18T14:25:03.87+02:00"), firstEvent.LastUpdated);
        Assert.Equal("6fx92hehmwaYYYY", firstEvent.ProductionId);
        Assert.Equal("Uppesittarkväll", firstEvent.ProductionName);
        Assert.True(firstEvent.HasTableReservation);
        Assert.Contains("event-start-day-changed", firstEvent.Tags);
        Assert.Single(firstEvent.Tags);

        var venue = firstEvent.Venue;
        Assert.Equal("rl7n0elzclfXXXX", venue.Id);
        Assert.Equal("Johns vändkors arena", venue.Name);
        Assert.Equal("Nygatan 12", venue.Address);
        Assert.Equal("67132", venue.Zipcode);
        Assert.Equal("Arvika", venue.City);
        Assert.Equal("SE", venue.Country);
        Assert.IsType<Geo>(venue.Geo);
        Assert.Equal(57.7875m, venue.Geo.Latitude);
        Assert.Equal(14.2307m, venue.Geo.Longitude);

        var restaurant = firstEvent.Restaurants.First();
        Assert.Equal(240, restaurant.RestaurantId);
        Assert.Equal("Johns kök", restaurant.RestaurantName);


        Assert.IsType<List<GoodsItem>>(purchase.Goods);
        Assert.Equal(10, purchase.Goods.Count);
        var item = purchase.Goods.First();

        Assert.Equal("XXXX-252-0", item.GoodsId);
        Assert.Equal("Konsertmiddag Johns kök", item.Name);
        Assert.Equal("Konsertmiddag", item.ReceiptText);
        Assert.Equal(GoodsItemType.Voucher, item.Type);
        Assert.Equal("XXXX-252", item.ArtNo);
        Assert.Equal(50m, item.PriceIncVatAfterDiscount);
        Assert.Equal(5.3571m, item.VatPortion);
        Assert.Equal(0.12m, item.VatPercent);
        Assert.Equal("5wv6bvyrl921XXX", item.EventId);
        Assert.Null(item.Section);
        Assert.Null(item.Seat);
        Assert.Null(item.Row);
        Assert.Null(item.PackageId);
        Assert.False(item.PartOfSeasonToken);
        Assert.Null(item.PartOfSeasonTokenGoodsId);
        Assert.True(item.PartOfTableReservation);
        Assert.False(item.CanBePlacedAtTable);
        Assert.Equal(240, item.RestaurantId);
        Assert.Single(item.Tags);
        Assert.Contains("voucher", item.Tags);

        var ticket = purchase.Goods.Last();

        Assert.Equal("SEAS-TKT-0-12345678", ticket.GoodsId);
        Assert.Equal("Testkort/Testkort (SeasonOff.)", ticket.Name);
        Assert.Equal("Testkort", ticket.ReceiptText);
        Assert.Equal(GoodsItemType.Ticket, ticket.Type);
        Assert.Equal("SEAS-TKT", ticket.ArtNo);
        Assert.Equal("Parkett Bakre", ticket.Section);
        Assert.Equal("298", ticket.Seat);
        Assert.Equal("13", ticket.Row);
        Assert.True(ticket.PartOfSeasonToken);
        Assert.Equal("SEAS-DEF-0", ticket.PartOfSeasonTokenGoodsId);
        Assert.False(ticket.PartOfTableReservation);
        Assert.False(ticket.CanBePlacedAtTable);
        Assert.Null(ticket.RestaurantId);

        // FIXME: Tests for AdditionalInputField list and entity
        // FIXME: Tests for Campaign list and entity
    }

    [Theory]
    [InlineData("crm-restaurant-id-as-int.json", 123)]
    [InlineData("crm-restaurant-id-as-string.json", 123)]
    [InlineData("crm-restaurant-id-as-null.json", null)]
    public async Task GetCrmPurchasesAsync_EventRestaurantIdFormats(string testFile, int? expectedId)
    {
        SetupMockResponse(testFile);
        var result = await TicksterClient.GetCrmPurchasesAsync(1);
        var item = result.FirstOrDefault()?.Goods?.FirstOrDefault();

        Assert.NotNull(item);
        Assert.Equal(expectedId, item.RestaurantId);
    }

    [Fact]
    public async Task GetCrmPurchasesAsync_TestEmptyResponse()
    {
        // Arrange
        SetupMockResponse("crm-purchases-empty.json");

        // Act
        var result = await TicksterClient.GetCrmPurchasesAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}

