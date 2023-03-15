using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Contract;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class ContractService : IContractService
    {
        private readonly IMapper _mapper;
        private readonly IContractRepository _contractRepository;
        private readonly IUserService _userService;
        private readonly UserDTO currentUser;

        public ContractService(IMapper mapper, IContractRepository contractRepository, IUserService userService)
        {
            _mapper = mapper;
            _contractRepository = contractRepository;
            _userService = userService;
            currentUser = _userService.Current();
        }

        public async Task<List<int>> GetAllMyContractBragainCodes()
        {
            var items = await _contractRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.UserId == currentUser.Id)
                .Select(s => s.BargainCode)
                .ToListAsync();
            return items;
        }
        
        public async Task<ContractDTO> CreateContract(CreateContract input)
        {
            var model = _mapper.Map<Contract>(input);

            model.UserId = currentUser.Id;

            model = await _contractRepository.Add(model);

            return _mapper.Map<ContractDTO>(model);
        }

        public async Task<ContractDTO> EditContract(EditContract input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerIsCompany = input.SellerIsCompany;
            find.SellerName = input.SellerName;
            find.SellerLastName = input.SellerLastName;
            find.SellerFatherName = input.SellerFatherName;
            find.SellerNationalCode = input.SellerNationalCode;
            find.SellerBirthDay = input.SellerBirthDay;
            find.SellerBirthDayLocation = input.SellerBirthDayLocation;
            find.SellerAddress = input.SellerAddress;
            find.SellerMobileNumber = input.SellerMobileNumber;
            find.SellerEmail = input.SellerEmail;

            if (find.Status == ContractStatus.Paid || find.Status == ContractStatus.EditedAfterPayment || find.Status == ContractStatus.Approved)
            {
                find.Status = ContractStatus.EditedAfterPayment;
            }

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> GetMyContract(int bargainCode)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, bargainCode);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel2(ContractLevel2 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerTamami = input.SellerTamami;
            find.SellerDong = input.SellerDong;
            find.SellerAutomobileDevice = input.SellerAutomobileDevice;
            find.SellerTip = input.SellerTip;
            find.SellerSystem = input.SellerSystem;
            find.SellerSolarYearModel = input.SellerSolarYearModel;
            find.SellerChassisNumber = input.SellerChassisNumber;
            find.SellerEngineNumber = input.SellerEngineNumber;
            find.SellerVinNumer = input.SellerVinNumer;
            find.SellerPoliceLicensePlateNumber = input.SellerPoliceLicensePlateNumber;
            find.SellerPoliceLicensePlateLetter = input.SellerPoliceLicensePlateLetter;
            find.SellerIran = input.SellerIran;
            find.SellerInsuranceNumber = input.SellerInsuranceNumber;
            find.SellerInsuranceCompany = input.SellerInsuranceCompany;
            find.SellerRemainingValidity = input.SellerRemainingValidity;
            find.SellerRemainingValidityLetter = input.SellerRemainingValidityLetter;
            find.SellerOtherAttachments = input.SellerOtherAttachments;

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel3(ContractLevel3 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerTotalAmountNumber = input.SellerTotalAmountNumber;
            find.SellerTotalAmountLetter = input.SellerTotalAmountLetter;
            find.SellerAmountNumberFirst = input.SellerAmountNumberFirst;
            find.SellerAmountLetterFirst = input.SellerAmountLetterFirst;
            find.SellerPrepaymentFirst = input.SellerPrepaymentFirst;
            find.SellerCheckNumberFirst = input.SellerCheckNumberFirst;
            find.SellerCheckDateFirst = input.SellerCheckDateFirst;
            find.SellerCheckBankFirst = input.SellerCheckBankFirst;
            find.SellerCheckBankBranchFirst = input.SellerCheckBankBranchFirst;
            find.SellerCheckAccountNumberFirst = input.SellerCheckAccountNumberFirst;
            find.SellerCheckBAccountBankFirst = input.SellerCheckBAccountBankFirst;
            find.SellerCheckAccountBranchFirst = input.SellerCheckAccountBranchFirst;
            find.SellerAmountNumberMiddle = input.SellerAmountNumberMiddle;
            find.SellerAmountLetterMiddle = input.SellerAmountLetterMiddle;
            find.SellerPrepaymentMiddle = input.SellerPrepaymentMiddle;
            find.SellerCheckNumberMiddle = input.SellerCheckNumberMiddle;
            find.SellerCheckDateMiddle = input.SellerCheckDateMiddle;
            find.SellerCheckBankMiddle = input.SellerCheckBankMiddle;
            find.SellerCheckBankBranchMiddle = input.SellerCheckBankBranchMiddle;
            find.SellerCheckAccountNumberMiddle = input.SellerCheckAccountNumberMiddle;
            find.SellerCheckBAccountBankMiddle = input.SellerCheckBAccountBankMiddle;
            find.SellerCheckAccountBranchMiddle = input.SellerCheckAccountBranchMiddle;
            find.SellerAmountNumberLast = input.SellerAmountNumberLast;
            find.SellerAmountLetterLast = input.SellerAmountLetterLast;
            find.SellerPrepaymentLast = input.SellerPrepaymentLast;
            find.SellerCheckNumberLast = input.SellerCheckNumberLast;
            find.SellerCheckDateLast = input.SellerCheckDateLast;
            find.SellerCheckBankLast = input.SellerCheckBankLast;
            find.SellerCheckBankBranchLast = input.SellerCheckBankBranchLast;
            find.SellerCheckAccountNumberLast = input.SellerCheckAccountNumberLast;
            find.SellerCheckBAccountBankLast = input.SellerCheckBAccountBankLast;
            find.SellerCheckAccountBranchLast = input.SellerCheckAccountBranchLast;

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel4(ContractLevel4 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerBargainDate = input.SellerBargainDate;
            find.SellerBargainProvince = input.SellerBargainProvince;
            find.SellerBargainCity = input.SellerBargainCity;
            find.SellerBargainStreet = input.SellerBargainStreet;
            find.SellerBargainAlley = input.SellerBargainAlley;
            find.SellerBargainPlaque = input.SellerBargainPlaque;
            find.SellerBargainBuilding = input.SellerBargainBuilding;
            find.SellerBargainFloor = input.SellerBargainFloor;
            find.SellerBargainUnit = input.SellerBargainUnit;
            find.SellerBargainPostalCode = input.SellerBargainPostalCode;
            find.SellerCancellationPaymentNumber = input.SellerCancellationPaymentNumber;
            find.SellerCancellationPaymentLetter = input.SellerCancellationPaymentLetter;

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel5(ContractLevel5 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerRegistryCompanyNumber = input.SellerRegistryCompanyNumber;
            find.SellerRegistryCompanyCity = input.SellerRegistryCompanyCity;
            find.SellerRegistryCompanyDateNumber = input.SellerRegistryCompanyDateNumber;
            find.SellerRegistryCompanyDateLetter = input.SellerRegistryCompanyDateLetter;
            find.SellerDelayPaymentNumber = input.SellerDelayPaymentNumber;
            find.SellerDelayPaymentLetter = input.SellerDelayPaymentLetter;
            find.SellerDelayPossessionNumber = input.SellerDelayPossessionNumber;
            find.SellerDelayPossessionLetter = input.SellerDelayPossessionLetter;


            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel6(ContractLevel6 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel7(ContractLevel7 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerPrepareDocumentDay = input.SellerPrepareDocumentDay;
            find.SellerPrepareDocumentDate = input.SellerPrepareDocumentDate;
            find.SellerPrepareDocumentClock = input.SellerPrepareDocumentClock;
            find.SellerPrepareDocumentProvince = input.SellerPrepareDocumentProvince;
            find.SellerPrepareDocumentCity = input.SellerPrepareDocumentCity;

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel8(ContractLevel8 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel9(ContractLevel9 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerJurisdictionsProvince = input.SellerJurisdictionsProvince;
            find.SellerJurisdictionsCity = input.SellerJurisdictionsCity;

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel10(ContractLevel10 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<ContractDTO> CompleteLevel11(ContractLevel11 input)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, input.BargainCode);

            find.SellerOtherConsiderPresentation = input.SellerOtherConsiderPresentation;
            find.SellerLawCourtResponsible = input.SellerLawCourtResponsible;
            find.SellerOtherItems = input.SellerOtherItems;
            find.SellerDocumentLink = input.SellerDocumentLink;
            find.SellerDocumentArticle = input.SellerDocumentArticle;
            find.SellerDocumentClause = input.SellerDocumentClause;
            find.SellerTrackingCode = input.SellerTrackingCode;

            await _contractRepository.Update(find);

            return _mapper.Map<ContractDTO>(find);
        }

        public async Task<string> GetMyContractHtml(int bargainCode)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, bargainCode);

            var result = "<p>ماده 1)\tطرفین سند:\r\n1-1-\tفروشنده: آقای / خانم / شرکت: ##sellerName## ##sellerLastName## فرزند: ##sellerFatherName## شماره کدملی / شناسه ملی: ##sellerNationalCode## تاریخ تولد / تاریخ ثبت شرکت: ##sellerBirthDay## محل تولد / ثبت شرکت: ##sellerBirthDayLocation## نشانی: ##sellerAddress## شماره تماس: ##sellerMobileNumber## آدرس پست الکترونیکی: ##sellerEmail## \r\n1-2-\tخریدار: آقای / خانم / شرکت: ...... فرزند: ...... شماره کدملی / شناسه ملی: ...... تاریخ تولد / تاریخ ثبت شرکت: ...... محل تولد / ثبت شرکت: ...... نشانی: ...... شماره تماس: ...... آدرس پست الکترونیکی: ......\r\n\r\nماده 2)\tمورد معامله\r\nتمامی ##sellerTamami## دانگ ##sellerDong## دستگاه خودروی: ##sellerAutomobileDevice## تیپ: ##sellerTip## سیستم: ##sellerSystem## مدل سال: ##sellerSolarYearModel## شمسی / میلادی به شماره شاسی: ##sellerChassisNumber## شماره موتور: ##sellerEngineNumber## و شماره VIN: ##sellerVinNumer ## به شماره پلاک انتظامی: ##sellerPoliceLicensePlateNumber## دارای بیمه‌نامه معتبر شماره: ##sellerInsuranceNumber## شرکت بیمه: ##sellerInsuranceCompany## مدت اعتبار باقیمانده: ##sellerRemainingValidity## ماه / سال به انضمام سایر منصوبات و ملحقات ##sellerOtherAttachments## که جزو توابع و متعلقات مورد معامله است. \r\n\r\nماده 3)\tمبلغ (ثمن) معامله و شرایط و نحوه پرداخت آن\r\nبا توجه به توافق طرفین ثمن کل معامله به مبلغ ##sellerTotalAmountNumber## (حروفی: ##sellerTotalAmountLetter##) ریال بوده که طبق توافق فی‌مابین نیز نحوه پرداخت آن به شرح ذیل می‌باشد. \r\n3-1-\tمبلغ ##sellerPrepayment## ریال معادل 10 درصد ثمن کل معامله به عنوان پیش‌پرداخت بوده که همزمان با تنظیم این سند عادی به موجب ...... فقره فیش / تراکنش / چک بانکی به شماره ##sellerCheckNumber## مورخ ##sellerCheckDate## بانک ##sellerCheckBank## شعبه ##sellerCheckBankBranch## نیز در وجه فروشنده / به حساب بانکی شماره ...... بانک ...... شعبه ...... متعلق به فروشنده صادر / پرداخت گردیده است.\r\n3-2-\tمبلغ ...... (......) ریال معادل 80 درصد ثمن کل معامله همزمان با تحویل مورد معامله و به موجب ...... فقره چک بانکی به شماره ...... مورخ ...... بانک ...... شعبه ...... نیز در وجه فروشنده / به حساب بانکی شماره ...... بانک ...... شعبه ...... متعلق به فروشنده صادر / پرداخت می‌گردد.\r\n3-3-\tمبلغ ...... (......) ریال معادل 10 درصد ثمن کل معامله همزمان با تنظیم سند رسمی انتقال در دفتر اسناد رسمی یا وکالت بلاعزل فروش (حسب درخواست و توافق طرفین) و به موجب ...... فقره چک بانکی به شماره ...... مورخ ...... بانک ...... شعبه ...... نیز در وجه فروشنده / به حساب بانکی شماره ...... بانک ...... شعبه ...... متعلق به فروشنده صادر / پرداخت می-گردد. \r\nتبصره 1-\tعدم پرداخت هریک از مبالغ مندرج در بندهای فوق از سوی خریدار (به استثنای بند 5ـ7ـ) به هرعلتی موجب منفسخ شدن و بی‌اعتباری معامله شده و با انفساخ معامله، فروشنده نیز مجاز و مأذون بوده که مورد معامله را بدون اخذ هیچگونه مجوزی به هرشخص دیگری واگذار نماید و مراتب صرفاً باید از طریق ایمیل یا صندوق پیام به اطلاع طرف مقابل برسد. • ماده 4) \r\nماده 4)\tتسلیم مورد معامله:\r\n4-1-\tزمان تسلیم: طبق توافق فیمابین طرفین مقرر شد که مورد معامله در تاریخ ...... از سوی فروشنده صحیح و سالم به همراه تمامی متعلقات آن همراه با کارت خودرو، بیمه‌نامه معتبر و سایر مدارک همزمان با رعایت مفاد بند 3ـ 3ـ طی صورتجلسه‌ای به خریدار تسلیم و تحویل شود.\r\n4-2-\tمکان تسلیم: طبق توافق فیمابین طرفین مقرر شد که مورد معامله در تاریخ فوق در محل وقوع خودرو به نشانی (استان: ...... شهرستان: ...... خیابان: ...... کوچه: ...... پلاک: ...... ساختمان: ...... طبقه: ...... واحد: ......کدپستی: ......) از سوی فروشنده صحیح و سالم طی صورتجلسه‌ای به خریدار تسلیم و تحویل شود. \r\n4-3-\tدر صورت تاخیر و یا تعذر (عذر داشتن) در تسلیم مورد معامله خریدار حق دارد یا معامله را فسخ نموده (ظرف 3 روز از تاریخ تسلیم) و یا آن که روزانه مبلغ ...... (حروفی: ......) ریال را به عنوان خسارت تاخیر و یا تعذر از تسلیم مورد معامله که پرداخت آن بر عهده فروشنده بوده از وی مطالبه نماید.\r\nماده 5)\tسایر شروط و تعهدات طرفین معامله\r\n5-1-\tتاریخ قطعی تنظیم سندرسمی انتقال یا اعطای وکالت بلاعزل فروش در دفتر اسنادرسمی شماره ...... شهر ...... به تاریخ ...... (حروفی: ......) بوده که طرفین ملزم به حضور در آن دفترخانه جهت ایفای کلیه تعهدات مندرج در این سند و انجام تشریفات قانونی نقل و انتقال و قبل از آن حضور در مراکز تعویض پلاک موردنظر می‌باشند. \r\n5-2-\tفروشنده موظف است قبل از تاریخ تنظیم سندرسمی انتقال قطعی، راساً و یا با اعطای وکالت تعویض پلاک یا وکالت فروش و انتقال (حسب مورد) به خریدار کلیه اسناد و مدارک لازم اعم از استعلامات و پاسخ استعلامات و مفاصاحساب‌های مالیاتی، عوارض شهرداری و غیره را اخذ و سپس به دفترخانه تسلیم نماید به طوری که پس از فک پلاک قبلی و اخذ پلاک جدید به نام خریدار نیز در روز تنظیم سندرسمی انتقال قطعی هیچگونه مانعی برای ثبت رسمی معامله (سند قطعی انتقال یا وکالت بلاعزل فروش) وجود نداشته باشد. \r\n5-3-\tعدم حضور فیزیکی هریک از طرفین در دفتر اسنادرسمی و یا عدم آمادگی هریک برای امضاء سند اعم از وجود هرگونه مانع نظیر بازداشت یا رهن مورد معامله، فک پلاک نکردن آن، مسدودی حساب و یا آماده نبودن ثمن معامله حتی در قالب صدور چک بلامحل و غیره به طرف مقابل این حق را می‌دهد که حسب مورد گواهی عدم انجام معامله یا عدم حضور و یا حتی گواهی اعلام مراتب حضور خود در دفترخانه را جهت استیفای حقوق خویش تقاضا و اخذ نماید. \r\n5-4-\tچنانچه کاشف به عمل آید که مورد معامله مستحق‌للغیر بوده و یا در رهن و بازداشت بوده و این مانع قابل مرتفع بودن نباشد فروشنده مکلف است علاوه بر استرداد تمامی ثمن معامله که من غیرحق دریافت کرده از عهده تمامی خسارات وارده به خریدار تحت هر اسم و عنوان و به هر مبلغ و میزان برآید. \r\n5-5-\tبا توجه به الزام طرفین به رعایت مفاد سند و ایفای تعهدات ناشی از قرارداد چنانچه این عدم وفای به عهد از سوی فروشنده باشد (به استثنای مورد بند 5ـ4ـ) نظیر تاخیر در تنظیم سند، روزانه مبلغ ...... (حروفی: ......) ریال به عنوان خسارت تاخیر و یا تعذر از تنظیم سند بر عهده فروشنده بوده که از سوی خریدار قابل مطالبه خواهد بود. \r\n5-6-\tبا توجه به الزام طرفین به رعایت مفاد سند و ایفای تعهدات ناشی از قرارداد چنانچه این عدم وفای به عهد از سوی خریدار باشد نظیر تاخیر در تنظیم سند و یا عدم پرداخت ثمن یا تاخیر در آن (موضوع ماده 3 و بندها و تبصره ذیل آن)، معامله خود به خود منفسخ شده و با انفساخ معامله، فروشنده مستحق تمامی مبالغ دریافتی اعم از پیش‌پرداخت و یا سایر مبالغ پرداختی به عنوان وجه التزام است و می‌تواند مورد معامله را بدون اخذ هیچگونه مجوزی به هرشخص دیگری واگذار نماید و ثمن معامله از سوی خریدار قابل مطالبه نخواهد بود.\r\n5-7-\tلیکن در خصوص بند قبلی چنانچه این عدم ایفای تعهدات خریدار مربوط به مرحله تنظیم سند رسمی (10 درصد پایانی) بوده و مورد معامله به تصرف وی درآمده باشد، روزانه مبلغ ...... (حروفی: ......) ریال به عنوان خسارت تاخیر تادیه و یا تعذر از تنظیم سند (همانند بند 4ـ3ـ) بر عهده خریدار بوده که علاوه بر باقیمانده ثمن معامله از سوی فروشنده قابل مطالبه خواهد بود. \r\n5-8-\tطبق توافق طرفین، فروشنده مکلف به تنظیم سند رسمی انتقال به نام خریدار و یا شخص ثالثی است که به صورت مکتوب به فروشنده باید اعلام گردد لیکن این انتقال به غیر نافی تعهدات خریدار و یا شخص ثالث به عنوان قائم‌مقام قانونی خریدار نبوده و هرگونه اختلاف احتمالی فیمابین خریدار و منتقل‌الیه (شخص ثالث) ارتباطی به حقوق و مطالبات فروشنده ندارد. در غیر این صورت (عدم رفع اختلافات احتمالی)، فروشنده متعهد به تنظیم سند رسمی انتقال به نام خریدار است.\r\n5-9-\tتمامی هزینه‌های مترتب بر اخذ مفاصاحسابها، استعلامات و پاسخ آنها، پرداخت حقوق و دیون دولتی و هزینه‌های رفع بازداشت و فک رهن احتمالی بر عهده فروشنده بوده و هزینه‌های مربوط به تنظیم سند رسمی انتقال و یا وکالت (فروش یا تعویض پلاک) در دفتر اسناد رسمی و حقوق دولتی مترتب بر تنظیم سند رسمی به ترتیب (1ـ بالمناصفه بر عهده طرفین و 2ـ بر عهده فروشنده) است. \r\n5-10-\tوقوع حوادث قهری و یا فورس ماژور نظیر جنگ، سیل، زلزله، بیماری‌های واگیردار از قبیل کرونا و یا هر آنچه که عرفاً در حکم قوه قاهره بوده و از حیطه اراده و اختیارات طرفین خارج است لذا در صورت اثبات این که عدم ایفای تعهدات هریک از طرفین منتسب به این قوای قهری است، شخص نامبرده در برابر طرف مقابل فاقد مسئولیت بوده و برای دیگری ایجاد حق جهت مطالبه‌گری نخواهد کرد. \r\n5-11-\tتعدیل و نوسانات قیمت چه کاهنده چه فزاینده بنا به هر علتی اعم از کاهش یا افزایش عرضه و تقاضا، وقوع حوادث قهریه، توقف واردات، تولید و غیره تاثیری در ثمن معامله نداشته و تعهدات طرفین در رابطه با ثمن معامله و قیمت مورد معامله غیرقابل بحث و غیرمسموع است. \r\n5-12-\tبا تنظیم و امضای این سند عادی ایجاب و قبول فیمابین طرفین منعقد شده و طرفین و قائم‌مقام قانونی آنها وفق مواد 10 و 190 و 219 قانون مدنی و سایر الزامات قانونی و تعهدات قراردادی، مکلف به اجرای مفاد آن خواهند بود.\r\nماده 6)\tحق فسخ معامله:\r\nتمامی خیارات خصوصاً خیار غبن و سایر خیارات مختص عقد بیع از طرفین ساقط شد به استثنای خیار شرط مندرج در بند 4ـ3ـ و خیار تدلیس که در صورت اثبات تدلیس در مراجع قضایی ذیصلاح نیز قابلیت استفاده برای ذیحق متصور بوده و حق فسخ معامله برای وی یا وراث آنها وجود دارد.\r\nماده 7)\tزمان و مکان تنظیم سند\r\n7-1-\tزمان تنظیم عبارتست از: روز: ...... مورخ: ...... ساعت: ...... که به تایید کارشناس مربوطه رسیده و برای طرفین نیز لازم‌الاتباع است.\r\n7-2-\tمکان تنظیم عبارتست از: محل وقوع خودرو به نشانی استان: ...... شهرستان: ...... که به تایید کارشناس مربوطه رسیده و برای طرفین نیز لازم‌الاتباع است.\r\nماده 8)\tقوانین حاکم بر این سند عادی\r\nمفاد و مندرجات این سند عادی از هر حیث حتی در مقام تفسیر قصد و اراده طرفین، تعهدات طرفین و غیره تابع تمامی قوانین و مقررات جمهوری اسلامی ایران علی‌الخصوص قانون مدنی، قانون تجارت الکترونیکی، سایر موازین حقوقی و قواعد فقهی و همچنین تابع عرف محل وقوع ملک بوده و برای هریک از طرفین لازم‌الاجراست و فرض جهل به قانون مسموع نخواهد بود. \r\nماده 9)\tمرجع حل اختلاف\r\nدر صورت بروز هرگونه اختلاف و یا طرح هرگونه دعوا، ادعا، اعتراض و یا استیفای حقوق قانونی و قراردادی طبق بند 7ـ2ـ این قرارداد، محاکم و مراجع قضایی استان: ...... شهرستان: ...... صالح به رسیدگی بوده و برای طرفین لازم‌الاتباع است. \r\nماده 10)\tاعتبار امضاء (صحت امضاء) و مستند اعتبار آن\r\n10-1-\tبا عنایت به طی نمودن مراحل صحت سنجی و النهایه ارسال کد به سامانه ثنای طرفین، رویت کد ارسالی به سامانه ثنا و درج آن در فرآیند تنظیم این سند، این سلسه اقدامات به منزله امضاِ الکترونیکی و منحصر به فرد طرفین تلقی شده و برابر قواعد قانون تجارت الکترونیکی ضمان‌آور و لازم‌الاجراست مگر در صورت اثبات هرگونه تدلیس احتمالی و یا تقلب و سایر عناوین مجرمانه که برای طرف مقابل (مدعی) و مدیریت این سامانه حق پیگیری کیفری و تعقیب جزایی و یا حقوقی حسب مورد متصور است و طرفین اقرار به پذیرش این امر دارند.\r\nماده 11)\tسایر ملاحظات تکمیلی\r\n11-1-\tاین سند الکترونیکی عادی جهت ارائه به ...... تنظیم شده است.\r\n11-2-\tهرگونه حقوق و دیون دولتی احتمالی مترتب بر مفاد این سند عادی اعم از هرگونه مالیات ، عوارض ، بدهی تامین اجتماعی ، هزینه های اجرایی و ثبتی ، صنوف و اتحادیه ها ، مطالبات اشخاص ثالث حقوقی تحت نظارت حاکمیت و غیره به عهده ...... است.\r\n\r\n•\tعلائم و هشدارها و اطلاعات لازم (موضوع ماده 33 قانون تجارت الکترونیکی): \r\nأ‌-\tمشخصات فنی و ویژگی های کاربردی کالا یا خدمات:\r\nب‌-\tقواعد حقوقی حمایت از حقوق مصرف کننده وفق ماده 42 قانون مذکور شامل این موارد نخواهد شد:\r\n1.\tفهرست خدمات مالی آیین نامه اجرایی موضوع ماده 79 ق.ت.ا. \r\n2.\tمعاملات راجع به فروش اموال غیرمنقول و یا حقوق مالکیت ناشی از اموال غیر منقول (به جز اجاره اموال غیرمنقول). \r\n3.\tخرید از ماشین های فروش مستقیم کالا و خدمات.\r\n4.\tانجام معاملات از طریق تلفن عمومی (همگانی). \r\n5.\tمعاملات راجع به حراجی ها. \r\nت‌-\tسایر موارد لازم به ذکر ..... [ترجیحاً یکی دو صفحه در نظر گرفته شود] \r\n•\tتذکر:\r\n1-\tمفاد این سند الکترونیکی عادی (داده پیام) فقط نسبت به طرفین و قائم مقام قانونی آنها لازم الاجرا است.\r\n2-\tاین سند برابر قانون جمهوری اسلامی ایران تابع آثار و احکام جاری بر اسناد عادی (غیررسمی) و اسناد موضوع قانون تجارت الکترنیکی می‌باشد.\r\n3-\tصحت مفاد و مندرجات این سند الکترونیکی عادی (داده پیام) از طریق لینک سامانه به آدرس ...... قابل بررسی و صحت سنجی است.\r\n4-\tدر صورت درخواست کتبی مراجع قضایی ، انتظامی ، امنیتی ذیصلاح (حتی پلیس فتا) نیز این سند وفق مقررات در اختیار آنها قرار خواهد گرفت.\r\n5-\tاین سند الکترونیکی عادی طی ... ماده ؛ ... بند ؛ ... تبصره و در 3 نسخه که هریک در حکم واحد است فی‌مابین نامبردگان فوق \"با کد رهگیری نهایی ......\" تنظیم، امضاء و مبادله شد که قابلیت چاپ گرفتن و استناد به مفاد و مندرجات آن به تایید طرفین رسیده است.\r\n</p>";

            result = result.Replace("##sellerName##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerName}</span>");
            result = result.Replace("##sellerLastName##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerLastName}</span>");
            result = result.Replace("##sellerFatherName##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerFatherName}</span>");
            result = result.Replace("##sellerNationalCode##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerNationalCode}</span>");
            result = result.Replace("##sellerBirthDay##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBirthDay}</span>");
            result = result.Replace("##sellerBirthDayLocation##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBirthDayLocation}</span>");
            result = result.Replace("##sellerAddress##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAddress}</span>");
            result = result.Replace("##sellerMobileNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerMobileNumber}</span>");
            result = result.Replace("##sellerEmail##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerEmail}</span>");
            result = result.Replace("##sellerTamami##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerTamami}</span>");
            result = result.Replace("##sellerDong##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDong}</span>");
            result = result.Replace("##sellerAutomobileDevice##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAutomobileDevice}</span>");
            result = result.Replace("##sellerTip##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerTip}</span>");
            result = result.Replace("##sellerSystem##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerSystem}</span>");
            result = result.Replace("##sellerSolarYearModel##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerSolarYearModel}</span>");
            result = result.Replace("##sellerChassisNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerChassisNumber}</span>");
            result = result.Replace("##sellerEngineNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerEngineNumber}</span>");
            result = result.Replace("##sellerVinNumer##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerVinNumer}</span>");
            result = result.Replace("##sellerPoliceLicensePlateNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPoliceLicensePlateNumber}</span>");
            result = result.Replace("##sellerPoliceLicensePlateLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPoliceLicensePlateLetter}</span>");
            result = result.Replace("##sellerIran##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerIran}</span>");
            result = result.Replace("##sellerInsuranceNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerInsuranceNumber}</span>");
            result = result.Replace("##sellerInsuranceCompany##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerInsuranceCompany}</span>");
            result = result.Replace("##sellerRemainingValidity##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerRemainingValidity}</span>");
            result = result.Replace("##sellerRemainingValidityLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerRemainingValidityLetter}</span>");
            result = result.Replace("##sellerOtherAttachments##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerOtherAttachments}</span>");
            result = result.Replace("##sellerTotalAmountNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerTotalAmountNumber}</span>");
            result = result.Replace("##sellerTotalAmountLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerTotalAmountLetter}</span>");
            result = result.Replace("##sellerAmountNumberFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAmountNumberFirst}</span>");
            result = result.Replace("##sellerAmountLetterFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAmountLetterFirst}</span>");
            result = result.Replace("##sellerPrepaymentFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepaymentFirst}</span>");
            result = result.Replace("##sellerCheckNumberFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckNumberFirst}</span>");
            result = result.Replace("##sellerCheckDateFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckDateFirst}</span>");
            result = result.Replace("##sellerCheckBankFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBankFirst}</span>");
            result = result.Replace("##sellerCheckBankBranchFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBankBranchFirst}</span>");
            result = result.Replace("##sellerCheckAccountNumberFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckAccountNumberFirst}</span>");
            result = result.Replace("##sellerCheckBAccountBankFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBAccountBankFirst}</span>");
            result = result.Replace("##sellerCheckAccountBranchFirst##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckAccountBranchFirst}</span>");
            result = result.Replace("##sellerAmountNumberMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAmountNumberMiddle}</span>");
            result = result.Replace("##sellerAmountLetterMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAmountLetterMiddle}</span>");
            result = result.Replace("##sellerPrepaymentMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepaymentMiddle}</span>");
            result = result.Replace("##sellerCheckNumberMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckNumberMiddle}</span>");
            result = result.Replace("##sellerCheckDateMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckDateMiddle}</span>");
            result = result.Replace("##sellerCheckBankMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBankMiddle}</span>");
            result = result.Replace("##sellerCheckBankBranchMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBankBranchMiddle}</span>");
            result = result.Replace("##sellerCheckAccountNumberMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckAccountNumberMiddle}</span>");
            result = result.Replace("##sellerCheckBAccountBankMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBAccountBankMiddle}</span>");
            result = result.Replace("##sellerCheckAccountBranchMiddle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckAccountBranchMiddle}</span>");
            result = result.Replace("##sellerAmountNumberLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAmountNumberLast}</span>");
            result = result.Replace("##sellerAmountLetterLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerAmountLetterLast}</span>");
            result = result.Replace("##sellerPrepaymentLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepaymentLast}</span>");
            result = result.Replace("##sellerCheckNumberLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckNumberLast}</span>");
            result = result.Replace("##sellerCheckDateLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckDateLast}</span>");
            result = result.Replace("##sellerCheckBankLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBankLast}</span>");
            result = result.Replace("##sellerCheckBankBranchLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBankBranchLast}</span>");
            result = result.Replace("##sellerCheckAccountNumberLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckAccountNumberLast}</span>");
            result = result.Replace("##sellerCheckBAccountBankLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckBAccountBankLast}</span>");
            result = result.Replace("##sellerCheckAccountBranchLast##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCheckAccountBranchLast}</span>");
            result = result.Replace("##sellerBargainDate##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainDate}</span>");
            result = result.Replace("##sellerBargainProvince##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainProvince}</span>");
            result = result.Replace("##sellerBargainCity##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainCity}</span>");
            result = result.Replace("##sellerBargainStreet##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainStreet}</span>");
            result = result.Replace("##sellerBargainAlley##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainAlley}</span>");
            result = result.Replace("##sellerBargainPlaque##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainPlaque}</span>");
            result = result.Replace("##sellerBargainBuilding##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainBuilding}</span>");
            result = result.Replace("##sellerBargainFloor##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainFloor}</span>");
            result = result.Replace("##sellerBargainUnit##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainUnit}</span>");
            result = result.Replace("##sellerBargainPostalCode##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerBargainPostalCode}</span>");
            result = result.Replace("##sellerCancellationPaymentNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCancellationPaymentNumber}</span>");
            result = result.Replace("##sellerCancellationPaymentLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerCancellationPaymentLetter}</span>");
            result = result.Replace("##sellerRegistryCompanyNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerRegistryCompanyNumber}</span>");
            result = result.Replace("##sellerRegistryCompanyCity##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerRegistryCompanyCity}</span>");
            result = result.Replace("##sellerRegistryCompanyDateNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerRegistryCompanyDateNumber}</span>");
            result = result.Replace("##sellerRegistryCompanyDateLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerRegistryCompanyDateLetter}</span>");
            result = result.Replace("##sellerDelayPaymentNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDelayPaymentNumber}</span>");
            result = result.Replace("##sellerDelayPaymentLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDelayPaymentLetter}</span>");
            result = result.Replace("##sellerDelayPossessionNumber##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDelayPossessionNumber}</span>");
            result = result.Replace("##sellerDelayPossessionLetter##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDelayPossessionLetter}</span>");
            result = result.Replace("##sellerPrepareDocumentDay##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepareDocumentDay}</span>");
            result = result.Replace("##sellerPrepareDocumentDate##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepareDocumentDate}</span>");
            result = result.Replace("##sellerPrepareDocumentClock##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepareDocumentClock}</span>");
            result = result.Replace("##sellerPrepareDocumentProvince##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepareDocumentProvince}</span>");
            result = result.Replace("##sellerPrepareDocumentCity##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerPrepareDocumentCity}</span>");
            result = result.Replace("##sellerJurisdictionsProvince##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerJurisdictionsProvince}</span>");
            result = result.Replace("##sellerJurisdictionsCity##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerJurisdictionsCity}</span>");
            result = result.Replace("##sellerOtherConsiderPresentation##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerOtherConsiderPresentation}</span>");
            result = result.Replace("##sellerLawCourtResponsible##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerLawCourtResponsible}</span>");
            result = result.Replace("##sellerOtherItems##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerOtherItems}</span>");
            result = result.Replace("##sellerDocumentLink##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDocumentLink}</span>");
            result = result.Replace("##sellerDocumentArticle##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDocumentArticle}</span>");
            result = result.Replace("##sellerDocumentClause##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerDocumentClause}</span>");
            result = result.Replace("##sellerTrackingCode##", $"<span style='font-size: larger;font-weight: bolder;color: #007bff;'>{find.SellerTrackingCode}</span>");

            return result;
        }

        public async Task GetFactor(int bargainCode)
        {
            var find = await _contractRepository.GetContract(currentUser.Id, bargainCode);

            ///
        }
    }
}
