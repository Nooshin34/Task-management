<div dir="rtl">

# مدیریت تسک (Task Management)

## پیاده‌سازی

پروژه با **ASP.NET Core MVC** روی **.NET 10** و معماری Onion در چهار لایه پیاده شده است (از معماری Onion و Bootstrap استفاده شده چون شرکت از آن‌ها استفاده می‌کرد):

- `TaskManagement.Domain` — موجودیت‌ها و اینترفیس ریپازیتوری
- `TaskManagement.Application` — سرویس‌ها، DTO و اعتبارسنجی تسک
- `TaskManagement.Infrastructure` — EF Core، SQL Server، مهاجرت‌ها، JWT و هش رمز
- `TaskManagement.Web` — کنترلرها، ویوها، ViewModel ورود/ثبت‌نام و فایل‌های استاتیک

دیتابیس **SQL Server LocalDB** است و دو جدول دارد: `Users` و `Tasks`. هر تسک به سازنده و کاربر اختصاص‌یافته با کلید خارجی وصل می‌شود.

فرانت با HTML، CSS و JavaScript است. حذف تسک قطعی نیست؛ با `fetch` تأیید می‌شود و همان ردیف در جدول قرمز می‌ماند.

## اجرای اولیه بعد از دانلود از GitHub

پیش‌نیاز: Visual Studio با workload مربوط به ASP.NET و SQL Server LocalDB.

1. ریپو را کلون کن و فایل `TaskManagement.sln` را در Visual Studio باز کن.
2. روی `TaskManagement.Web` راست‌کلیک کن و **Set as Startup Project** را بزن.
3. از منوی **Tools → NuGet Package Manager → Package Manager Console** کنسول را باز کن، **Default project** را روی `TaskManagement.Infrastructure` بگذار و این دستور را اجرا کن:

<pre dir="ltr">Update-Database</pre>

4. با **F5** برنامه را اجرا کن. کاربر پیش‌فرض وجود ندارد؛ از صفحه ثبت‌نام یک حساب بساز و وارد شو.

</div>
