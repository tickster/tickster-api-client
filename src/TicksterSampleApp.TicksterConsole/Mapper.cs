using TicksterSampleApp.Domain.Enums;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Importer;

public static class Mapper
{
    public static Purchase MapPurchase(Tickster.Api.Models.Crm.Purchase purchase)
        => MapPurchase(purchase, new Purchase());

    public static Purchase MapPurchase(Tickster.Api.Models.Crm.Purchase purchase, Purchase dbPurchase)
    {
        dbPurchase.TicksterCrmId = purchase.CrmId;
        dbPurchase.TicksterPurchaseRefNo = purchase.PurchaseRefno;
        dbPurchase.Status = StatusExtensions.FromString(purchase.Status.ToString());
        dbPurchase.Created = purchase.CreatedUtc;
        dbPurchase.LastUpdated = purchase.LastUpdatedUtc;
        dbPurchase.Currency = purchase.Currency;
        dbPurchase.Channel = ChannelExtensions.FromString(purchase.Channel.ToString());
        dbPurchase.ToBePaidInRestaurantSystem = purchase.ToBePaidInRestaurantSystem;
        dbPurchase.DiscountCodeName = purchase.DiscountCodeName;
        dbPurchase.DiscountCode = purchase.DiscountCode;
        dbPurchase.EogRequestCode = purchase.EogRequestCode;
        dbPurchase.PrivacyRefNo = purchase.PrivacyRefNo;
        dbPurchase.TermsRefNo = purchase.TermsRefNo;

        return dbPurchase;
    }

    public static Goods MapGoods(Tickster.Api.Models.Crm.GoodsItem goods)
        => MapGoods(goods, new Goods());

    public static Goods MapGoods(Tickster.Api.Models.Crm.GoodsItem goods, Goods dbGoods)
    {
        dbGoods.TicksterGoodsId = goods.GoodsId;
        dbGoods.Name = goods.Name;
        dbGoods.ReceiptText = goods.ReceiptText;
        dbGoods.Type = GoodsTypeExtensions.FromString(goods.Type.ToString());
        dbGoods.ArticleNumber = goods.ArtNo;
        dbGoods.PriceIncVatAfterDiscount = goods.PriceIncVatAfterDiscount;
        dbGoods.VatPortion = goods.VatPortion;
        dbGoods.VatPercent = goods.VatPercent;
        dbGoods.Section = goods.Section ?? "";
        dbGoods.Seat = int.Parse(goods.Seat ?? "0");
        dbGoods.Row = int.Parse(goods.Row ?? "0");
        dbGoods.PartOfSeasonToken = goods.PartOfSeasonToken;
        dbGoods.PartOfSeasonTokenGoodsId = goods.PartOfSeasonTokenGoodsId ?? "";
        dbGoods.PartOfTableReservation = goods.PartOfTableReservation;
        dbGoods.CanBePlacedAtTable = goods.CanBePlacedAtTable;
        dbGoods.TicksterEventId = goods.EventId;

        return dbGoods;
    }

    public static Event MapEvent(Tickster.Api.Models.Crm.Event ev)
        => MapEvent(ev, new Event());

    public static Event MapEvent(Tickster.Api.Models.Crm.Event ev, Event dbEvent)
    {
        dbEvent.TicksterEventId = ev.Id;
        dbEvent.Name = ev.Name;
        dbEvent.Start = ev.Start;
        dbEvent.End = ev.End;
        dbEvent.LastUpdated = ev.LastUpdated;
        dbEvent.TicksterProductionId = ev.ProductionId;
        dbEvent.ProductionName = ev.ProductionName;
        dbEvent.HasTableReservation = ev.HasTableReservation;

        return dbEvent;
    }

    public static Campaign MapCampaign(Tickster.Api.Models.Crm.Campaign campaign)
        => MapCampaign(campaign, new Campaign());

    public static Campaign MapCampaign(Tickster.Api.Models.Crm.Campaign campaign, Campaign dbCampaign)
    {
        dbCampaign.TicksterCampaignId = campaign.Id;
        dbCampaign.TicksterCommunicationId = campaign.CommunicationId;
        dbCampaign.ActivationCode = campaign.ActivationCode;
        dbCampaign.TicksterInternalReference = campaign.InternalReference;

        return dbCampaign;
    }

    public static Customer MapCustomer(Tickster.Api.Models.Crm.Purchase purchase)
        => MapCustomer(purchase, new Customer());

    public static Customer MapCustomer(Tickster.Api.Models.Crm.Purchase purchase, Customer dbCustomer)
    { 
        dbCustomer.TicksterUserRefNo = purchase.UserRefNo;
        dbCustomer.IdNumber = purchase.IdNo;
        dbCustomer.IsCompany = purchase.IsCompany;
        dbCustomer.FirstName = purchase.FirstName;
        dbCustomer.LastName = purchase.LastName;
        dbCustomer.PostalAddressLineOne = purchase.PostalAddressLineOne;
        dbCustomer.PostalAddressLineTwo = purchase.PostalAddressLineTwo;
        dbCustomer.ZipCode = purchase.Zipcode;
        dbCustomer.City = purchase.City;
        dbCustomer.CountryCode = purchase.CountryCode;
        dbCustomer.CompanyName = purchase.CompanyName;
        dbCustomer.MobilePhone = purchase.MobilePhoneNo;
        dbCustomer.Email = purchase.Email;

        return dbCustomer;
    }

    public static Venue MapVenue(Tickster.Api.Models.Crm.Venue venue)
        => MapVenue(venue, new Venue());

    public static Venue MapVenue(Tickster.Api.Models.Crm.Venue venue, Venue dbVenue)
    {
        dbVenue.TicksterVenueId = venue.Id;
        dbVenue.Name = venue.Name;
        dbVenue.Address = venue.Address;
        dbVenue.ZipCode = venue.Zipcode;
        dbVenue.City = venue.City;
        dbVenue.CountryCode = venue.Country;
        dbVenue.Latitude = venue.Geo?.Latitude;
        dbVenue.Longitude = venue.Geo?.Longitude;

        return dbVenue;
    }

    public static Restaurant MapRestaurant(Tickster.Api.Models.Crm.Restaurant restaurant)
        => MapRestaurant(restaurant, new Restaurant());

    public static Restaurant MapRestaurant(Tickster.Api.Models.Crm.Restaurant restaurant, Restaurant dbRestaurant)
    {
        dbRestaurant.RestaurantId = restaurant.RestaurantId;
        dbRestaurant.RestaurantName = restaurant.RestaurantName;

        return dbRestaurant;
    }

    public static PurchaseCampaign MapPurchaseCampaign(Purchase dbPurchase, string campaignId)
        => new()
        {
            PurchaseId = dbPurchase.Id,
            CampaignId = campaignId
        };

    public static EventRestaurant MapEventRestaurant(Event dbEvent, Restaurant dbRestaurant)
        => new()
        {
            EventId = dbEvent.Id,
            RestaurantId = dbRestaurant.Id
        };
}