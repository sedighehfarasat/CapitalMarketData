namespace CapitalMarketData.Entities.Enums;

public enum InstrumentType
{
    /// <summary>
    /// گام-اوراق گواهی اعتبار مولد
    /// </summary>
    Gam = 206,

    /// <summary>
    /// صکوک
    /// </summary>
    Sokuk = 208,

    /// <summary>
    /// سهام - بورس
    /// </summary>
    Stock_Exchange = 300,

    /// <summary>
    /// اوراق مشارکت
    /// </summary>
    Mosharekat = 301,

    /// <summary>
    ///  سهام - فرابورس
    /// </summary>
    Stock_OffExchange = 303,

    /// <summary>
    /// آتی سهام
    /// </summary>
    StockFuture = 304,

    /// <summary>
    ///  صندوق سرمايه گذاري اوراق 
    /// </summary>
    Etf = 305,

    /// <summary>
    /// اسناد خزانه، اجاره، مرابحه 
    /// </summary>
    Bond = 306,

    /// <summary>
    /// تسهیلات مسکن - تسه
    /// </summary>
    Tese = 307,

    /// <summary>
    /// سلف
    /// </summary>
    Salaf = 308,

    /// <summary>
    /// سهام - بازار پایه
    /// </summary>
    Stock_BaseMarket = 309,

    /// <summary>
    /// اختیار خرید
    /// </summary>
    BuyOption1 = 311,

    /// <summary>
    /// اختیار فروش
    /// </summary>
    SellOption1 = 312,

    /// <summary>
    /// سهام
    /// </summary>
    Stock4 = 313,

    /// <summary>
    /// اختیار خرید
    /// </summary>
    BuyOption2 = 320,

    /// <summary>
    /// اختیار فروش
    /// </summary>
    SellOption2 = 321,

    /// <summary>
    /// صندوق سرمايه گذاري کالایی
    /// </summary>
    CommodityEtf = 380,

    /// <summary>
    /// حق تقدم
    /// </summary>
    Warrant = 400,

    /// <summary>
    /// حق تقدم
    /// </summary>
    Warrant1 = 403,

    /// <summary>
    /// حق تقدم
    /// </summary>
    Warrant2 = 404,

    /// <summary>
    /// اختیار فروش تبعی
    /// </summary>
    TabaieOption = 600,

    /// <summary>
    /// گواهی سپرده کالایی
    /// </summary>
    CommodityCertificateOfDeposit = 701,

    /// <summary>
    /// مرابحه دولتی
    /// </summary>
    GovernmentMorabeheh = 706,
}
