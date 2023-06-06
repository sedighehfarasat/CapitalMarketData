namespace CapitalMarketData.Entities.Enums;

public enum Status
{
    /// <summary>
    /// مجاز
    /// <space>A
    /// </summary>
    Trading = 1,

    /// <summary>
    /// مجاز-محفوظ
    /// امکان ثبت و حذف سفارش
    /// AR
    /// </summary>
    EnteringOrder = 2,

    /// <summary>
    /// مجاز-متوقف
    /// امکان حذف سفارش
    /// AS
    /// </summary>
    RemovingOrder = 3,

    /// <summary>
    /// ممنوع
    /// بازگشایی و انجام معاملات در همان جلسه معاملاتی می باشد
    /// <space>I
    /// </summary>
    Suspended = 4,

    /// <summary>
    /// ممنوع-متوقف
    /// بازگشایی و انجام معاملات در همان جلسه معاملاتی نمی باشد
    /// IS
    /// </summary>
    Halted = 5,
}
