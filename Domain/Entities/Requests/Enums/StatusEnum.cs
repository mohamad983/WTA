namespace Domain.Entities.Requests.Enums
{
    public enum StatusEnum
    {
        /// <summary>
        /// پیش‌نویس (هنوز ثبت نهایی نشده و در جریان فرآیند قرار نگرفته است)
        /// </summary>
        Draft = 1,

        /// <summary>
        /// در حال بررسی / در جریان کارتابل (در یکی از مراحل منتظر تأیید است)
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// تأیید نهایی شده و با موفقیت به پایان رسیده
        /// </summary>
        Approved = 3,

        /// <summary>
        /// رد شده توسط یکی از تأییدکنندگان
        /// </summary>
        Rejected = 4,

        /// <summary>
        /// لغو شده توسط خود ثبت‌کننده (ارسال‌کننده پشیمان شده)
        /// </summary>
        Canceled = 5,

        /// <summary>
        /// برگشت‌داده‌شده جهت اصلاح به متقاضی (نیاز به بازنگری)
        /// </summary>
        ReturnedForCorrection = 6,

        /// <summary>
        /// منقضی شده (به علت اتمام مهلت SLA یا مهلت اقدام)
        /// </summary>
        Expired = 7,

        /// <summary>
        /// معلق / متوقف موقت (به دستور مدیر سیستم یا برای بررسی تکمیلی)
        /// </summary>
        Suspended = 8
    }
}
