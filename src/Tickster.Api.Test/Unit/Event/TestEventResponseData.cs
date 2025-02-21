using Tickster.Api.Models.Event;
using Tickster.Api.Models.Event.Enum;

namespace Tickster.Api.Test.Unit.Event;

public class TestEventResponseData : MockAgentBase
{
    [Fact]
    public async Task SearchUpcomingEvents_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-searchupcomingevents-ok.json");

        // Act
        var result = await TicksterClient.SearchUpcomingEvents();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalItems);
        Assert.Equal(0, result.Skipped);
        Assert.NotNull(result.Items);
        Assert.Equal(2, result.Items.Count);

        // Validate first event: "GIFT CARD - GRAND THEATER"
        var firstEvent = result.Items.FirstOrDefault(e => e.Id == "abc123xyz456");
        Assert.NotNull(firstEvent);
        Assert.Equal("GIFT CARD - GRAND THEATER", firstEvent.Name);
        Assert.NotNull(firstEvent.Description);
        Assert.Contains("Gift card valid for purchasing tickets to events at the theater.", firstEvent.Description.Markdown);
        Assert.Contains("<p><strong>Gift card valid for purchasing tickets to events at the theater.", firstEvent.Description.Html);

        Assert.Equal(EventState.ReleasedForSale, firstEvent.State);
        Assert.Equal(new DateTime(2005, 6, 15, 14, 0, 0, DateTimeKind.Utc), firstEvent.StartUtc);
        Assert.Equal(new DateTime(2099, 12, 31, 22, 59, 0, DateTimeKind.Utc), firstEvent.EndUtc);
        Assert.Equal(new DateTime(2025, 2, 16, 18, 20, 0, DateTimeKind.Utc), firstEvent.LastUpdatedUtc);

        // Validate URLs
        Assert.Equal("https://www.ticketsite.com/events/abc123xyz456", firstEvent.InfoUrl);
        Assert.Equal("https://secure.ticketsite.com/abc123xyz456", firstEvent.ShopUrl);

        // Validate organizer
        Assert.NotNull(firstEvent.Organizer);
        Assert.Equal("org789abc456", firstEvent.Organizer.Id);
        Assert.Equal("City Cultural Events", firstEvent.Organizer.Name);
        Assert.Contains(firstEvent.Organizer.Links, link => link.Rel == "self" && link.Href == "https://api.ticketsite.com/v1.0/organizers/org789abc456");
        Assert.Contains(firstEvent.Organizer.Links, link => link.Rel == "events" && link.Href == "https://api.ticketsite.com/v1.0/organizers/org789abc456/events");

        // Validate venue
        Assert.NotNull(firstEvent.Venue);
        Assert.Equal("venue567def123", firstEvent.Venue.Id);
        Assert.Equal("Grand Theater", firstEvent.Venue.Name);
        Assert.Equal("123 Main St", firstEvent.Venue.Address);
        Assert.Equal("12345", firstEvent.Venue.ZipCode);
        Assert.Equal("Metropolis", firstEvent.Venue.City);
        Assert.Equal("Fakeland", firstEvent.Venue.Country);
        Assert.Equal(40.7128, firstEvent.Venue.Geo.Latitude);
        Assert.Equal(-74.0060, firstEvent.Venue.Geo.Longitude);

        // Validate second event: "CULTURE PASS - Community Arts Center"
        var secondEvent = result.Items.FirstOrDefault(e => e.Id == "xyz789lmn456");
        Assert.NotNull(secondEvent);
        Assert.Equal("CULTURE PASS - Community Arts Center", secondEvent.Name);
        Assert.NotNull(secondEvent.Description);
        Assert.Contains("What do you get for someone who has everything?", secondEvent.Description.Markdown);
        Assert.Contains("<p><strong>What do you get for someone who has everything?</strong></p>", secondEvent.Description.Html);

        Assert.Equal(EventState.ReleasedForSale, secondEvent.State);
        Assert.Equal(new DateTime(2015, 8, 20, 18, 0, 0, DateTimeKind.Utc), secondEvent.StartUtc);
        Assert.Equal(new DateTime(2099, 12, 31, 21, 0, 0, DateTimeKind.Utc), secondEvent.EndUtc);
        Assert.Equal(new DateTime(2024, 11, 28, 5, 39, 0, DateTimeKind.Utc), secondEvent.LastUpdatedUtc);

        // Validate URLs
        Assert.Equal("https://www.ticketsite.com/events/xyz789lmn456", secondEvent.InfoUrl);
        Assert.Equal("https://secure.ticketsite.com/xyz789lmn456", secondEvent.ShopUrl);

        // Validate organizer
        Assert.NotNull(secondEvent.Organizer);
        Assert.Equal("org123xyz789", secondEvent.Organizer.Id);
        Assert.Equal("Community Arts Organization", secondEvent.Organizer.Name);
        Assert.Contains(secondEvent.Organizer.Links, link => link.Rel == "self" && link.Href == "https://api.ticketsite.com/v1.0/organizers/org123xyz789");
        Assert.Contains(secondEvent.Organizer.Links, link => link.Rel == "events" && link.Href == "https://api.ticketsite.com/v1.0/organizers/org123xyz789/events");

        // Validate venue
        Assert.NotNull(secondEvent.Venue);
        Assert.Equal("venue456lmn789", secondEvent.Venue.Id);
        Assert.Equal("Community Arts Center", secondEvent.Venue.Name);
        Assert.Equal("456 Elm St", secondEvent.Venue.Address);
        Assert.Equal("67890", secondEvent.Venue.ZipCode);
        Assert.Equal("Springfield", secondEvent.Venue.City);
        Assert.Equal("Fakeland", secondEvent.Venue.Country);
        Assert.Equal(37.7749, secondEvent.Venue.Geo.Latitude);
        Assert.Equal(-122.4194, secondEvent.Venue.Geo.Longitude);
    }

    [Fact]
    public async Task GetDetailsForSingleEvent_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-getdetailsforsingleevent-ok.json");

        // Act
        var result = await TicksterClient.GetDetailsForSingleEvent("abc123xyz789");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("abc123xyz789", result.Id);
        Assert.Equal("GIFT CARD - GRAND CITY THEATER", result.Name);

        // Validate organizer
        Assert.NotNull(result.Organizer);
        Assert.Equal("org456def123", result.Organizer.Id);
        Assert.Equal("Grand City Events", result.Organizer.Name);
        Assert.Equal("us", result.Organizer.Country);
        Assert.Contains(result.Organizer.Links, link => link.Rel == "self" && link.Href == "https://api.example.com/organizers/org456def123");
        Assert.Contains(result.Organizer.Links, link => link.Rel == "events" && link.Href == "https://api.example.com/organizers/org456def123/events");

        // Validate venue
        Assert.NotNull(result.Venue);
        Assert.Equal("venue789ghi456", result.Venue.Id);
        Assert.Equal("Grand City Theater", result.Venue.Name);
        Assert.Equal("123 Main Street", result.Venue.Address);
        Assert.Equal("10001", result.Venue.ZipCode);
        Assert.Equal("Metropolis", result.Venue.City);
        Assert.Equal("US", result.Venue.Country);
        Assert.Equal(40.7128, result.Venue.Geo.Latitude);
        Assert.Equal(-74.0060, result.Venue.Geo.Longitude);

        // Validate event metadata
        Assert.Equal(EventState.ReleasedForSale, result.State);
        Assert.Equal(StockLevel.Unknown, result.StockLevel);
        Assert.Equal(new DateTime(2023, 1, 1, 16, 0, 0, DateTimeKind.Utc), result.StartUtc);
        Assert.Equal(new DateTime(2030, 12, 31, 22, 59, 0, DateTimeKind.Utc), result.EndUtc);
        Assert.Equal(new DateTime(2023, 1, 1, 16, 0, 0, DateTimeKind.Utc), result.DoorsOpenUtc);
        Assert.Equal(new DateTime(2025, 12, 31, 20, 0, 0, DateTimeKind.Utc), result.CurfewUtc);

        // Validate URLs
        Assert.Equal("https://static.example.com/sample-image.jpg", result.ImageUrl);
        Assert.Equal("https://www.example.com/events/abc123xyz789", result.InfoUrl);
        Assert.Equal("https://secure.example.com/abc123xyz789", result.ShopUrl);

        Assert.NotNull(result.LocalizedShopUrls);
        Assert.Equal("https://secure.example.com/sv/abc123xyz789", result.LocalizedShopUrls["sv"]);
        Assert.Equal("https://secure.example.com/no/abc123xyz789", result.LocalizedShopUrls["nb"]);
        Assert.Equal("https://secure.example.com/da/abc123xyz789", result.LocalizedShopUrls["da"]);
        Assert.Equal("https://secure.example.com/en/abc123xyz789", result.LocalizedShopUrls["en"]);

        // Validate description
        Assert.NotNull(result.Description);
        Assert.Contains("Gift cards valid for purchasing tickets to events at the theater.", result.Description.Markdown);
        Assert.Contains("<p><strong>Gift cards valid for purchasing tickets to events at the theater.", result.Description.Html);

        // Validate tags
        Assert.NotNull(result.Tags);
        Assert.Contains("Metropolis", result.Tags);
        Assert.Contains("Grand-City-Theater", result.Tags);
        Assert.Contains("support-your-event", result.Tags);
        Assert.Contains("event-rescheduled", result.Tags);

        // Validate products
        Assert.NotNull(result.Products);
        Assert.Equal(5, result.Products.Count);

        Assert.Contains(result.Products, p => p.Name == "Gift Card - 50 units" && p.ProductType == ProductType.GiftCertificate);
        Assert.Contains(result.Products, p => p.Name == "Gift Card - 150 units" && p.ProductType == ProductType.GiftCertificate);
        Assert.Contains(result.Products, p => p.Name == "Gift Card - 250 units" && p.ProductType == ProductType.GiftCertificate);
        Assert.Contains(result.Products, p => p.Name == "Gift Card - 400 units" && p.ProductType == ProductType.GiftCertificate);
        Assert.Contains(result.Products, p => p.Name == "Gift Card - 750 units" && p.ProductType == ProductType.GiftCertificate);

        // Validate last updated timestamp
        Assert.Equal(new DateTime(2025, 2, 16, 18, 20, 52, 857, DateTimeKind.Utc), result.LastUpdatedUtc);
    }

    [Fact]
    public async Task GetOrganizers_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-getorganizers-ok.json");

        // Act
        var result = await TicksterClient.GetOrganizers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());

        // Validate first organizer
        var organizer1 = result.FirstOrDefault(o => o.Id == "abc123xyz789");
        Assert.NotNull(organizer1);
        Assert.Equal("Metro Dance Club", organizer1.Name);
        Assert.Contains(organizer1.Links, link => link.Rel == "self" && link.Href == "https://api.example.com/v1.0/en/organizers/abc123xyz789");
        Assert.Contains(organizer1.Links, link => link.Rel == "events" && link.Href == "https://api.example.com/v1.0/en/organizers/abc123xyz789/events");

        // Validate second organizer
        var organizer2 = result.FirstOrDefault(o => o.Id == "def456uvw123");
        Assert.NotNull(organizer2);
        Assert.Equal("Rock Legends Live", organizer2.Name);
        Assert.Contains(organizer2.Links, link => link.Rel == "self" && link.Href == "https://api.example.com/v1.0/en/organizers/def456uvw123");
        Assert.Contains(organizer2.Links, link => link.Rel == "events" && link.Href == "https://api.example.com/v1.0/en/organizers/def456uvw123/events");

        // Validate third organizer
        var organizer3 = result.FirstOrDefault(o => o.Id == "ghi789rst456");
        Assert.NotNull(organizer3);
        Assert.Equal("The Art Loft", organizer3.Name);
        Assert.Contains(organizer3.Links, link => link.Rel == "self" && link.Href == "https://api.example.com/v1.0/en/organizers/ghi789rst456");
        Assert.Contains(organizer3.Links, link => link.Rel == "events" && link.Href == "https://api.example.com/v1.0/en/organizers/ghi789rst456/events");
    }

    [Fact]
    public async Task GetOrganizerById_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-getorganizerbyid-ok.json");

        // Act
        var result = await TicksterClient.GetOrganizerById("XYZ123ABC456");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("XYZ123ABC456", result.Id);
        Assert.Equal("Fusion Lounge", result.Name);
        Assert.Equal("https://fusionlounge.fake", result.Website);
        Assert.Equal("contact@fusionlounge.fake", result.Email);
        Assert.Equal("123456-7890", result.OrganizationalId);
        Assert.Equal("cluborconcert", result.SegmentName);
        Assert.NotNull(result.Address);
        Assert.Contains("Sunset Blvd 42", result.Address);
        Assert.Contains("90210 Beverly Hills", result.Address);
        Assert.Equal("en", result.DefaultLanguage);
        Assert.Equal("us", result.Country);

        // Validate social media presence
        Assert.NotNull(result.SocialMedia);
        Assert.True(result.SocialMedia.ContainsKey("facebook"));
        Assert.True(result.SocialMedia.ContainsKey("instagram"));
        Assert.Equal("https://facebook.com/fusionlounge", result.SocialMedia["facebook"]);
        Assert.Equal("https://instagram.com/fusionlounge", result.SocialMedia["instagram"]);

        // Validate API links
        Assert.NotNull(result.Links);
        var eventsLink = result.Links.FirstOrDefault(link => link.Rel == "events");
        Assert.NotNull(eventsLink);
        Assert.Equal("https://event.api.fake.com/api/v1.0/en/organizers/xyz123abc456/events", eventsLink.Href);
        Assert.Equal("GET", eventsLink.Method);
    }

    [Fact]
    public async Task SearchUpcomingEventsByOrganizer_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-searchupcomingeventsbyorganizer-ok.json");

        // Act
        var result = await TicksterClient.SearchUpcomingEventsByOrganizer("XYZ123ABC456");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.TotalItems);
        Assert.Equal(0, result.Skipped);
        Assert.NotNull(result.Items);
        Assert.Equal(2, result.Items.Count);

        // Validate first event
        var firstEvent = result.Items.FirstOrDefault(e => e.Id == "abc123xyz456");
        Assert.NotNull(firstEvent);
        Assert.Equal("Whiskyprovning med SWC", firstEvent.Name);
        Assert.Equal(EventState.ReleasedForSale, firstEvent.State);
        Assert.Equal(new DateTime(2025, 3, 15, 18, 0, 0, DateTimeKind.Utc), firstEvent.StartUtc);
        Assert.Equal(new DateTime(2025, 3, 15, 20, 0, 0, DateTimeKind.Utc), firstEvent.EndUtc);
        Assert.Equal(new DateTime(2025, 1, 20, 10, 0, 0, DateTimeKind.Utc), firstEvent.LastUpdatedUtc);
        Assert.Equal("https://www.example.com/events/abc123xyz456", firstEvent.InfoUrl);
        Assert.Equal("https://secure.example.com/abc123xyz456", firstEvent.ShopUrl);
        Assert.Equal(EventHierarchyType.Event, firstEvent.EventHierarchyType);
        Assert.Null(firstEvent.ParentEventId);

        // Validate event description
        Assert.NotNull(firstEvent.Description);
        Assert.Contains("Välkommen till vår SWC whiskyprovning", firstEvent.Description.Markdown);
        Assert.Contains("<p>Välkommen till vår SWC whiskyprovning", firstEvent.Description.Html);

        // Validate event venue
        Assert.NotNull(firstEvent.Venue);
        Assert.Equal("The Oak Barrel", firstEvent.Venue.Name);
        Assert.Equal("Drottninggatan 12", firstEvent.Venue.Address);
        Assert.Equal("Norrköping", firstEvent.Venue.City);
        Assert.Equal("602 22", firstEvent.Venue.ZipCode);
        Assert.Equal("Sweden", firstEvent.Venue.Country);
        Assert.Equal(58.5878, firstEvent.Venue.Geo.Latitude);
        Assert.Equal(16.1924, firstEvent.Venue.Geo.Longitude);

        // Validate event organizer
        Assert.NotNull(firstEvent.Organizer);
        Assert.Equal("org98765", firstEvent.Organizer.Id);
        Assert.Equal("Scandinavian Whisky Club", firstEvent.Organizer.Name);
        Assert.Contains(firstEvent.Organizer.Links, l => l.Rel == "self" && l.Href == "https://event.api.example.com/api/v1.0/sv/organizers/org98765");
        Assert.Contains(firstEvent.Organizer.Links, l => l.Rel == "events" && l.Href == "https://event.api.example.com/api/v1.0/sv/organizers/org98765/events");

        // Validate second event
        var secondEvent = result.Items.FirstOrDefault(e => e.Id == "def789uvw123");
        Assert.NotNull(secondEvent);
        Assert.Equal("Exklusiv Ölprovning 2025", secondEvent.Name);
        Assert.Equal(EventState.ReleasedForSale, secondEvent.State);
        Assert.Equal(new DateTime(2025, 4, 12, 19, 0, 0, DateTimeKind.Utc), secondEvent.StartUtc);
        Assert.Equal(new DateTime(2025, 4, 12, 21, 0, 0, DateTimeKind.Utc), secondEvent.EndUtc);
        Assert.Equal(new DateTime(2025, 2, 1, 15, 30, 0, DateTimeKind.Utc), secondEvent.LastUpdatedUtc);
        Assert.Equal("https://www.example.com/events/def789uvw123", secondEvent.InfoUrl);
        Assert.Equal("https://secure.example.com/def789uvw123", secondEvent.ShopUrl);
        Assert.Equal(EventHierarchyType.Event, secondEvent.EventHierarchyType);
        Assert.Null(secondEvent.ParentEventId);

        // Validate second event description
        Assert.NotNull(secondEvent.Description);
        Assert.Contains("Upplev en kväll fylld med öl", secondEvent.Description.Markdown);
        Assert.Contains("<p>Upplev en kväll fylld med öl", secondEvent.Description.Html);

        // Validate second event venue
        Assert.NotNull(secondEvent.Venue);
        Assert.Equal("Gamla Bryggeriet", secondEvent.Venue.Name);
        Assert.Equal("Storgatan 45", secondEvent.Venue.Address);
        Assert.Equal("Göteborg", secondEvent.Venue.City);
        Assert.Equal("411 36", secondEvent.Venue.ZipCode);
        Assert.Equal("Sweden", secondEvent.Venue.Country);
        Assert.Equal(57.7089, secondEvent.Venue.Geo.Latitude);
        Assert.Equal(11.9746, secondEvent.Venue.Geo.Longitude);

        // Validate second event organizer
        Assert.NotNull(secondEvent.Organizer);
        Assert.Equal("org54321", secondEvent.Organizer.Id);
        Assert.Equal("Swedish Craft Brewers", secondEvent.Organizer.Name);
        Assert.Contains(secondEvent.Organizer.Links, l => l.Rel == "self" && l.Href == "https://event.api.example.com/api/v1.0/sv/organizers/org54321");
        Assert.Contains(secondEvent.Organizer.Links, l => l.Rel == "events" && l.Href == "https://event.api.example.com/api/v1.0/sv/organizers/org54321/events");

        // Validate links for both events
        Assert.Contains(firstEvent.Links, l => l.Rel == "self" && l.Href == "https://event.api.example.com/api/v1.0/sv/events/abc123xyz456");
        Assert.Contains(secondEvent.Links, l => l.Rel == "self" && l.Href == "https://event.api.example.com/api/v1.0/sv/events/def789uvw123");
    }

    [Fact]
    public async Task GetExtendedOrganizerDetails_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-getextendedorganizerdetails-ok.json");

        // Act
        var result = await TicksterClient.GetExtendedOrganizerDetails("ABX4YZ9MK1PL2XJ");

        // Assert
        // Validate the organizer's id
        Assert.Equal("ABX4YZ9MK1PL2XJ", result.Id);

        // Validate the organizer's name
        Assert.Equal("Sample Event Organizer", result.Name);

        // Validate the organizer's website
        Assert.Equal("https://www.sampleeventorg.com", result.Website);

        // Validate the organizer's email
        Assert.Equal("contact@sampleeventorg.com", result.Email);

        // Validate the organizer's organizationalId
        Assert.Equal("123456-7890", result.OrganizationalId);

        // Validate the organizer's segment name
        Assert.Equal("corporate", result.SegmentName);

        // Validate the address (assuming it's an array of strings)
        Assert.Equal("Main Street 45", result.Address[0]);
        Assert.Equal("123 45 Fictional City", result.Address[1]);

        // Validate social media links
        Assert.True(result.SocialMedia.ContainsKey("facebook"));
        Assert.Equal("https://facebook.com/sampleeventorg", result.SocialMedia["facebook"]);

        Assert.True(result.SocialMedia.ContainsKey("twitter"));
        Assert.Equal("https://twitter.com/sampleeventorg", result.SocialMedia["twitter"]);

        // Validate the default language
        Assert.Equal("en", result.DefaultLanguage);

        // Validate the country
        Assert.Equal("us", result.Country);

        // Validate the event API link
        Assert.Equal("https://event.api.sample.com/api/v1.0/en/partner/organizers/abx4yz9mk1pl2xj/events", result.Links[0].Href);
        Assert.Equal("GET", result.Links[0].Method);
    }

    [Fact]
    public async Task SearchUpcomingEventsByOrganizerPartner_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-searchupcomingeventsbyorganizerpartner-ok.json");

        // Act
        var result = await TicksterClient.SearchUpcomingEventsByOrganizerPartner("org56789");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(93, result.TotalItems);
        Assert.Equal(0, result.Skipped);
        Assert.NotNull(result.Items);
        Assert.Equal(2, result.Items.Count);

        // Validate first event
        var firstEvent = result.Items[0];
        Assert.Equal("abc123xyz789", firstEvent.Id);
        Assert.Equal("Music Fest Support Ticket", firstEvent.Name);
        Assert.NotNull(firstEvent.Description);
        Assert.Equal("Support Music Fest by purchasing this special ticket. Your contribution helps us continue bringing amazing live events to your city!", firstEvent.Description.Markdown);
        Assert.Equal("<p>Support Music Fest by purchasing this special ticket. Your contribution helps us continue bringing amazing live events to your city!</p>\n", firstEvent.Description.Html);
        Assert.Equal(new DateTime(2019, 6, 15, 17, 30, 0), firstEvent.StartUtc);
        Assert.Equal(new DateTime(2099, 12, 31, 23, 59, 59), firstEvent.EndUtc);
        Assert.Equal(new DateTime(2025, 1, 15, 8, 20, 00), firstEvent.LastUpdatedUtc);
        Assert.Equal(EventState.SaleEnded, firstEvent.State);
        Assert.Equal("https://www.example.com/events/abc123xyz789", firstEvent.InfoUrl);
        Assert.Equal("https://secure.example.com/abc123xyz789", firstEvent.ShopUrl);

        // Validate organizer details
        Assert.NotNull(firstEvent.Organizer);
        Assert.Equal("org56789", firstEvent.Organizer.Id);
        Assert.Equal("Live Events Inc.", firstEvent.Organizer.Name);
        Assert.NotEmpty(firstEvent.Organizer.Links);
        Assert.Contains(firstEvent.Organizer.Links, link => link.Rel == "self" && link.Href == "https://api.example.com/v1.0/en/organizers/org56789");

        // Validate venue details
        Assert.NotNull(firstEvent.Venue);
        Assert.Equal("venue12345", firstEvent.Venue.Id);
        Assert.Equal("Grand Hall", firstEvent.Venue.Name);
        Assert.Equal("123 Main St", firstEvent.Venue.Address);
        Assert.Equal("10001", firstEvent.Venue.ZipCode);
        Assert.Equal("New York", firstEvent.Venue.City);
        Assert.Equal("USA", firstEvent.Venue.Country);
        Assert.Equal(40.7128, firstEvent.Venue.Geo.Latitude);
        Assert.Equal(-74.0060, firstEvent.Venue.Geo.Longitude);

        // Validate second event
        var secondEvent = result.Items[1];
        Assert.Equal("def456uvw123", secondEvent.Id);
        Assert.Equal("Support Your Local Artists", secondEvent.Name);
        Assert.NotNull(secondEvent.Description);
        Assert.Equal("Join us in supporting emerging artists with this special ticket purchase. Your support helps fund creative talent!", secondEvent.Description.Markdown);
        Assert.Equal("<p>Join us in supporting emerging artists with this special ticket purchase. Your support helps fund creative talent!</p>\n", secondEvent.Description.Html);
        Assert.Equal(new DateTime(2021, 5, 10, 14, 0, 0), secondEvent.StartUtc);
        Assert.Equal(new DateTime(2099, 12, 31, 23, 59, 59), secondEvent.EndUtc);
        Assert.Equal(new DateTime(2025, 1, 15, 8, 20, 0), secondEvent.LastUpdatedUtc);
        Assert.Equal(EventState.ReleasedForSale, secondEvent.State);
        Assert.Equal("https://www.example.com/events/def456uvw123", secondEvent.InfoUrl);
        Assert.Equal("https://secure.example.com/def456uvw123", secondEvent.ShopUrl);

        // Validate organizer of second event
        Assert.NotNull(secondEvent.Organizer);
        Assert.Equal("org56789", secondEvent.Organizer.Id);
        Assert.Equal("Live Events Inc.", secondEvent.Organizer.Name);

        // Validate venue of second event (same as first event)
        Assert.NotNull(secondEvent.Venue);
        Assert.Equal("venue12345", secondEvent.Venue.Id);
        Assert.Equal("Grand Hall", secondEvent.Venue.Name);
        Assert.Equal("123 Main St", secondEvent.Venue.Address);
        Assert.Equal("10001", secondEvent.Venue.ZipCode);
        Assert.Equal("New York", secondEvent.Venue.City);
        Assert.Equal("USA", secondEvent.Venue.Country);
        Assert.Equal(40.7128, secondEvent.Venue.Geo.Latitude);
        Assert.Equal(-74.0060, secondEvent.Venue.Geo.Longitude);
    }

    [Fact]
    public async Task GetExtendedEventDetails_ValidateResponse()
    {
        // Arrange
        SetupEventMockResponse("event-getextendedeventdetails-ok.json");

        // Act
        var result = await TicksterClient.GetExtendedEventDetails("org56789", "abc123xyz789");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("event-xyz1234", result.Id);
        Assert.Equal("Virtual Music Concert", result.Name);
        Assert.Equal("2025-03-15T19:00:00Z", result.StartUtc.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal("2025-03-15T21:00:00Z", result.EndUtc.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal("2025-03-15T18:30:00Z", result.DoorsOpenUtc.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal("2025-03-15T21:30:00Z", result.CurfewUtc.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal(EventState.ReleasedForSale, result.State);
        Assert.Equal(StockLevel.Instock, result.StockLevel);
        Assert.Equal("https://example.com/virtual-concert-image.jpg", result.ImageUrl);
        Assert.Equal("https://example.com/shop", result.ShopUrl);
        Assert.Contains("en", result.LocalizedShopUrls);
        Assert.Equal("https://example.com/en/shop", result.LocalizedShopUrls["en"]);
        Assert.NotNull(result.Tags);
        Assert.Contains("virtual concert", result.Tags);

        // Assert: Validate the Organizer object
        var organizer = result.Organizer;
        Assert.Equal("org-9876", organizer.Id);
        Assert.Equal("Music Events Ltd.", organizer.Name);
        Assert.Equal("en", organizer.DefaultLanguage);
        Assert.Equal("US", organizer.Country);

        // Assert: Validate the Venue object
        var venue = result.Venue;
        Assert.Equal("venue-5678", venue.Id);
        Assert.Equal("Online Streaming Platform", venue.Name);
        Assert.Equal("Virtual Space", venue.Address);
        Assert.Equal("12345", venue.ZipCode);
        Assert.Equal("Online City", venue.City);
        Assert.Equal("Virtual", venue.Country);
        Assert.Equal(0.0000, venue.Geo.Latitude);
        Assert.Equal(0.0000, venue.Geo.Longitude);

        // Assert: Validate the Products
        Assert.Single(result.Products);
        var product = result.Products[0];
        Assert.Equal("VIP Ticket", product.Name);
        Assert.Equal(ProductType.Ticket, product.ProductType);
        Assert.Equal("Access to all performances with VIP perks.", product.Description);
        Assert.Equal("https://example.com/vip-ticket.jpg", product.MainImageUrl);
        Assert.Equal(99.99, product.Price.Amount);
        Assert.Equal("USD", product.Price.Currency);

        // Assert: Validate the WebLinks
        Assert.Equal(2, result.WebLinks.Count);
        var webLink1 = result.WebLinks[0];
        Assert.Equal("https://example.com/performance1", webLink1.Url);
        Assert.Equal("Performance 1", webLink1.Text);
        var webLink2 = result.WebLinks[1];
        Assert.Equal("https://example.com/performance2", webLink2.Url);
        Assert.Equal("Performance 2", webLink2.Text);

        // Additional validation checks based on other fields
        Assert.NotNull(result.Performers);
        Assert.Contains("Artist 1", result.Performers);
        Assert.Contains("Artist 2", result.Performers);

        // Validate child events are empty (since it's not populated in the mock)
        Assert.Equal(2, result.ChildEvents.Count);
    }
}
