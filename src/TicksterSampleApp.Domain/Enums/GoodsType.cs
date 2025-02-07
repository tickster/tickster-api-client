using System.Security.Cryptography.X509Certificates;

namespace TicksterSampleApp.Domain.Enums;

public enum GoodsType
{
    Undefined = 0,
    Ticket = 1,
    Package = 2,
    GiftCertificate = 3,
    GiftCertificateDeposit = 4,
    GiftCertificateDiscount = 5,
    Voucher = 6,
    Goods = 7,
    SeasonToken = 8,
    AdminGoods = 9,
    EventInsurance = 10,
    Donation = 11,
    SeasonTokenExtension = 12,
    ArticlePoolGiftCertificate = 13,
    CreditPointsGiftCertificate = 14
}

public static class GoodsTypeExtensions
{
    public static GoodsType FromString(string goodsType)
    {
        goodsType = goodsType.Trim().ToLower();

        return goodsType switch
        {
            "ticket" => GoodsType.Ticket,
            "package" => GoodsType.Package,
            "giftcertificate" => GoodsType.GiftCertificate,
            "giftcertificatedeposit" => GoodsType.GiftCertificateDeposit,
            "giftcertificatediscount" => GoodsType.GiftCertificateDiscount,
            "voucher" => GoodsType.Voucher,
            "goods" => GoodsType.Goods,
            "seasontoken" => GoodsType.SeasonToken,
            "admingoods" => GoodsType.AdminGoods,
            "eventinsurance" => GoodsType.EventInsurance,
            "creditpointsgiftcertificate" => GoodsType.CreditPointsGiftCertificate,
            "articlepoolgiftcertificate" => GoodsType.ArticlePoolGiftCertificate,
            "seasontokenextension" => GoodsType.SeasonTokenExtension,
            "donation" => GoodsType.Donation,
            _ => GoodsType.Undefined
        };
    }
}