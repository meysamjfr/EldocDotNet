const ticketStatus = {
    0: { 'title': "بسته شده", 'class': 'dark' },
    1: { 'title': "منتظر پاسخ پشتیبان", 'class': 'light-primary' },
    2: { 'title': "منتظر پاسخ کاربر", 'class': 'light-success' },
    3: { 'title': "درانتظار", 'class': 'light-warning' },
};
const ticketPriority = {
    0: { 'title': "فوری", 'class': 'light-danger' },
    1: { 'title': "معمولی", 'class': 'light-info' },
    2: { 'title': "جهت اطلاع", 'class': 'light' },
};
const colors = [
    KTUtil.getCssVariableValue('--bs-blue'),
    KTUtil.getCssVariableValue('--bs-purple'),
    KTUtil.getCssVariableValue('--bs-pink'),
    KTUtil.getCssVariableValue('--bs-red'),
    KTUtil.getCssVariableValue('--bs-orange'),
    KTUtil.getCssVariableValue('--bs-yellow'),
    KTUtil.getCssVariableValue('--bs-green'),
    KTUtil.getCssVariableValue('--bs-teal'),
    KTUtil.getCssVariableValue('--bs-cyan'),
];

const unilateralContractTypeDTO = {
    0: { 'title': "اقرار", },
    1: { 'title': "رضایت", },
    2: { 'title': "تعهد", },
    3: { 'title': "وفای به عهد", },
    4: { 'title': "تنفیذ", },
    5: { 'title': "استشهادیه - طلب شهادت", },
    6: { 'title': "ابراء ذمه", },
    7: { 'title': "رجوع - از اذن یا حق", },
    8: { 'title': "بذل مدت - نکاح موقت", },
    9: { 'title': "فسخ قرارداد - انحلال یکطرفه", },
};

const bilateralContractTypeDTO = {
    0: { 'title': "بیع - خرید / فروش", },
    1: { 'title': "تعهد به بیع", },
    2: { 'title': "رهن و وثیقه - غیربانکی", },
    3: { 'title': "اجاره - تملیک منافع", },
    4: { 'title': "مشارکت مدنی - ساخت و ساز", },
    5: { 'title': "مشارکت - شراکت نامه", },
    6: { 'title': "تقسیم نامه", },
    7: { 'title': "وصیت نامه - عهدی / تملیکی", },
    8: { 'title': "صلح - در مقام بیع", },
    9: { 'title': "صلح و سازش - ترک دعوی", },
    10: { 'title': "هبه", },
    11: { 'title': "جعاله", },
    12: { 'title': "خرید خدمات - استخدام / مشاوره / نمایندگی", },
    13: { 'title': "قرارداد داوری", },
    14: { 'title': "قراردادهای موضوع ماده 10 قانون مدنی", },
    15: { 'title': "سایر عقود نامعین", },
    16: { 'title': "متمم قرارداد - الحاقیه / اصلاحیه", },
    17: { 'title': "اقاله / تفاسخ قرارداد - انحلال دوطرفه", },
};

const financialContractTypeDTO = {
    0: { 'title': "فاکتور", },
    1: { 'title': "پیش فاکتور", },
    2: { 'title': "صورت حساب", },
};

const transactionType = {
    0: { 'class': 'success' },
    1: { 'class': 'danger' },
};

const paymentType = {
    0: { 'title': "شارژ حساب" },
    1: { 'title': "درخواست مذاکره" },
    2: { 'title': "شارژ مدیر" },
    3: { 'title': "برداشت مدیر" },
    4: { 'title': "هدیه" },
};