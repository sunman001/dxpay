/* jquery 表单验证使用实例！  */
//获取Request notnull
function isRequestNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}
function isChinaName(name) {
    var pattern = /^[\u4E00-\u9FA5]{1,6}$/;
    return pattern.test(name);
}
//验证不为空 notnull
function isNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}

//验证数字 num
function isInteger(obj) {
    reg = /^[0-9]+.?[0-9]*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证数字 num  或者null,空
function isIntegerOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[-+]?\d+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//Email验证 email
function isEmail(obj) {
    reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//Email验证 email   或者null,空
function isEmailOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^\w{3,}@\w+(\.\w+)+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文字符串 echar
function isEnglishStr(obj) {
    reg = /^[a-z,A-Z]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文字符串 echar 或者null,空
function isEnglishStrOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[a-z,A-Z]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否是n位数字字符串编号 nnum
function isLenNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否是n位数字字符串编号 nnum或者null,空
function isLenNumOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否小于等于n位数的字符串 nchar
function isLenStr(obj, n) {
    //reg = /^[A-Za-z0-9\u0391-\uFFE5]+$/;
    obj = $.trim(obj);
    if (obj.length == 0 || obj.length > n)
        return false;
    else
        return true;
    //    if (!reg.test(obj)) {
    //        return false;
    //    } else {
    //        return true;
    //    }
}

//验证是否小于等于n位数的字符串 nchar或者null,空
function isLenStrOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    //reg = /^[A-Za-z0-9\u0391-\uFFE5]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
        //    if (!reg.test(obj)) {
        //        return false;
        //    } else {
        //        return true;
        //    }
    else
        return true;
}

//验证是否QQ号码
function isQQ(obj) {
    reg = /^\d{5,16}$/;
    obj = $.trim(obj);
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否电话号码 phone
function isTelephone(obj) {
    reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否电话号码 phone或者null,空
function isTelephoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号 mobile
function isMobile(obj) {
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号 mobile或者null,空
function isMobileOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号或电话号码 mobile phone 
function isMobileOrPhone(obj) {
    reg_mobile = /^(\+\d{2,3}\-)?\d{11}$/;
    reg_phone = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg_mobile.test(obj) && !reg_phone.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号或电话号码 mobile phone或者null,空
function isMobileOrPhoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    reg2 = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj) && !reg2.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证网址 uri
function isUri(obj) {
    reg = /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证网址 uri或者null,空
function isUriOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证两个值是否相等 equals
function isEqual(obj1, controlObj) {
    if (obj1.length != 0 && controlObj.length != 0) {
        if (obj1 == controlObj)
            return true;
        else
            return false;
    }
    else
        return false;
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date
function isDate(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date或者null,空
function isDateOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime
function isDateTime(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime或者null,空
function isDateTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time
function isTime(obj) {
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time或者null,空
function isTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为中文 cchar 
function isChinese(obj) {
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为中文 cchar或者null,空
function isChineseOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的邮编(只能为六位)是否正确 zip
function isZip(obj) {
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        if (!reg.test(str)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的邮编(只能为六位)是否正确 zip或者null,空
function isZipOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        if (!reg.test(str)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为双精度 double
function isDouble(obj) {
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为双精度 double或者null,空
function isDoubleOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断是否为身份证 idcard
function isIDCard(code) {
    var city = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江 ", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北 ", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏 ", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外 " };
    var tip = "";
    var pass = true;

    if (!code || !/^\d{6}(18|19|20)?\d{2}(0[1-9]|1[12])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i.test(code)) {
        tip = "身份证号格式错误";
        pass = false;
    }

    else if (!city[code.substr(0, 2)]) {
        tip = "地址编码错误";
        pass = false;
    }
    else {
        //18位身份证需要验证最后一位校验位
        if (code.length == 18) {
            code = code.split('');
            //∑(ai×Wi)(mod 11)
            //加权因子
            var factor = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
            //校验位
            var parity = [1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2];
            var sum = 0;
            var ai = 0;
            var wi = 0;
            for (var i = 0; i < 17; i++) {
                ai = code[i];
                wi = factor[i];
                sum += ai * wi;
            }
            var last = parity[sum % 11];
            if (parity[sum % 11] != code[17]) {
                tip = "校验位错误";
                pass = false;
            }
        }
    }
    //if (!pass) alert(tip);
    return pass;
}

function isValidBusCode(busCode) {
    var ret = false;
    if (busCode.length == 15 || busCode.length == 18) {
        var sum = 0;
        var s = [];
        var p = [];
        var a = [];
        var m = 10;
        p[0] = m;
        for (var i = 0; i < busCode.length; i++) {
            a[i] = parseInt(busCode.substring(i, i + 1), m);
            s[i] = (p[i] % (m + 1)) + a[i];
            if (0 == s[i] % m) {
                p[i + 1] = 10 * 2;
            } else {
                p[i + 1] = (s[i] % m) * 2;
            }
        }
        if ((1 == (s[14] % m))||(1 == (s[17] % m))) {            
            ret = true;//营业执照编号正确!
        } else {            
            ret = false;//营业执照编号错误!
        }
    } else if ("" == busCode) {
        ret = true;
    }
    return ret;
}

//判断是否为身份证 idcard或者null,空
function isIDCardOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^\d{15}(\d{2}[A-Za-z0-9;])?$/;
        if (!reg.test(obj))
            return false;
        else
            return true;
    }
}

//验证是否是图片
function CheckImgExtName(imgUrl) {
    var flag = false;
    var extion = ".jpg.png.bmp.jpeg.JPG.PNG.BMP.JPEG";
    var len = imgUrl.lastIndexOf(".");
    var extName = imgUrl.substr(len, imgUrl.length - len);
    if (extion.indexOf(extName) > -1) {
        flag = true;
    }
    return flag;
}

//验证脚本
//obj为当前input所在的空间容器 (例如：Div,Panel)
//脚本中 checkvalue 验证函数  err 属性表示提示【中文名称】
function JudgeValidate(obj) {
    var Validatemsg = "";
    var Validateflag = true;
    $(obj).find("[datacol=yes]").each(function () {
        if ($(this).attr("checkexpession") != undefined) {
            switch ($(this).attr("checkexpession")) {
                case "default":
                    {
                        if (isNotNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NotNull":
                    {
                        if (isNotNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "不能为空！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Num":
                    {
                        if (!isInteger($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NumOrNull":
                    {
                        if (!isIntegerOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Email":
                    {
                        if (!isEmail($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为E-mail格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EmailOrNull":
                    {
                        if (!isEmailOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为E-mail格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EnglishStr":
                    {
                        if (!isEnglishStr($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为字符串！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EnglishStrOrNull":
                    {
                        if (!isEnglishStrOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为字符串！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenNum":
                    {
                        if (!isLenNum($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenNumOrNull":
                    {
                        if (!isLenNumOrNull($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenStr":
                    {
                        if (!isLenStr($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须小于" + $(this).attr("length") + "位字符！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenStrOrNull":
                    {
                        if (!isLenStrOrNull($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须小于" + $(this).attr("length") + "位字符！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Phone":
                    {
                        if (!isTelephone($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须电话格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Fax":
                    {
                        if (!isTelephoneOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为传真格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "PhoneOrNull":
                    {
                        if (!isTelephoneOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须电话格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Mobile":
                    {
                        if (!isMobile($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrNull":
                    {
                        if (!isMobileOrnull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrPhone":
                    {
                        if (!isMobileOrPhone($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为电话格式或手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrPhoneOrNull":
                    {
                        if (!isMobileOrPhoneOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为电话格式或手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Uri":
                    {
                        if (!isUri($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为网址格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "UriOrNull":
                    {
                        if (!isUriOrnull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为网址格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Equal":
                    {
                        if (!isEqual($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "不相等！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Date":
                    {
                        if (!isDate($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateOrNull":
                    {
                        if (!isDateOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateTime":
                    {
                        if (!isDateTime($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateTimeOrNull":
                    {
                        if (!isDateTimeOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Time":
                    {
                        if (!isTime($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "TimeOrNull":
                    {
                        if (!isTimeOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseStr":
                    {
                        if (!isChinese($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为中文！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseStrOrNull":
                    {
                        if (!isChineseOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为中文！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Zip":
                    {
                        if (!isZip($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为邮编格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ZipOrNull":
                    {
                        if (!isZipOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为邮编格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Double":
                    {
                        if (!isDouble($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为小数！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DoubleOrNull":
                    {
                        if (!isDoubleOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为小数！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IDCard":
                    {
                        if (!isIDCard($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为身份证格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IDCardOrNull":
                    {
                        if (!isIDCardOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为身份证格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "RequestNotNull":
                    {
                        if (isNotNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    });
    if (Validatemsg.length > 0) {
        return Validateflag;
    }
    return Validateflag;
}
//修改出错的input的外观
function ChangeCss(obj, Validatemsg) {
    $(obj).removeClass("txt");
    if ($(obj).attr('class') == 'select') {
        $(obj).addClass("tooltipinputerr");
        $(obj).css('height', '22').css('line-height', '22');
    } else {
        $(obj).addClass("tooltipinputerr");
    }
    $(obj).focus(); //焦点
    $('body').append('<table id="tipTable" class="tableTip"><tr><td  class="leftImage"></td> <td class="contenImage" align="left"></td> <td class="rightImage"></td></tr></table>');
    var X = $(obj).offset().top;
    var Y = $(obj).offset().left;
    $('#tipTable').css({ left: Y - 2 + 'px', top: X + 21 + 'px' });
    $('#tipTable').show()
    $('.contenImage').html(Validatemsg);
    $(obj).change(function () {
        if ($(obj).val() != "") {
            $(obj).removeClass("tooltipinputerr");
            $(obj).addClass("tooltipinputok");
            $('#tipTable').hide()
        }
    });
}