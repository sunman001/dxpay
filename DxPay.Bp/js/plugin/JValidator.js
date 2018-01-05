//Javascript Document
/*jquery 表单验证使用实例*/

//获取是否为空
function isRequestNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}
function isRequestNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    } else {
        return false;
    }
}

//验证是否为空
function isNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    } else {
        return false;
    }
}

//验证是否为中文名字
function isChinaName(name) {
    var pattern = /^[\u4E00-\u9FA5]{1,6}$/;
    return pattern.test(name);
}

//验证数字 num
function isInteger(obj) {
    reg = /^[0-9]+.?[0-9]*$/;
    return reg.test(obj);
}

//验证数字 num  或者null,空
function isIntegerNotNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^[-+]?\d+$/;
        return reg.test(controlObj);
    }
}
//验证字符串长度
function isStrLength(obj) {
    reg = /^[\w\W]{1,20}$/;
    return reg.test(obj);
}

//Email验证 email
function isEmail(obj) {
    reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    return reg.test(obj);
}

//Email验证 email或者null,空
function isEmailOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^\w{3,}@\w+(\.\w+)+$/;
        return reg.test(controlObj);
    }
}

//验证只能输入英文字符串 echar
function isEnglishStr(obj) {
    reg = /^[a-z,A-Z]+$/;
    return reg.test(obj);
}

//验证只能输入英文字符串 echar 或者null,空
function isEnglishStrOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^[a-z,A-Z]+$/;
        return reg.test(obj);
    }
}

//验证是否是n位数字字符串编号 nnum
function isLenNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n) {
        return false;
    } else {
        return reg.test(obj);
    }
}

//验证是否是n位数字字符串编号 nnum或者null,空
function isLenNumOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^[0-9]+$/;
        if (controlObj.length > n) {
            return false;
        } else {
            return reg.test(obj);
        }
    }
}

//验证是否小于等于n位数的字符串 nchar
function isLenStr(obj, n) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj.length > n) {
        return false;
    } else {
        return true;
    }
}

//验证是否小于等于n位数的字符串 nchar或者null,空
function isLenStrOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        if (controlObj.length > n) {
            return false;
        } else {
            return true;
        }
    }
}

//验证字符串长度是否具体的范围内
function isLenStrBetween(obj, start, end) {
    obj = $.trim(obj);
    if (obj.length >= start && obj.length <= end) {
        return true;
    } else {
        return false;
    }
}

//验证是否QQ号码
function isQQ(obj) {
    reg = /^\d{5,16}$/;
    obj = $.trim(obj);
    return reg.test(obj);
}

//验证是否电话号码 phone
function isTelephone(obj) {
    reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    return reg.test(obj);
}

//验证是否电话号码 phone或者null,空
function isTelephoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
        return reg.test(obj);
    }
}

//验证是否手机号 mobile
function isMobile(obj) {
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    return reg.test(obj);
}

//验证是否手机号 mobile或者null,空
function isMobileOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^(\+\d{2,3}\-)?\d{11}$/;
        return reg.test(obj);
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
    } else {
        reg = /^(\+\d{2,3}\-)?\d{11}$/;
        reg2 = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
        if (!reg.test(obj) && !reg2.test(obj)) {
            return false;
        } else {
            return true;
        }
    }
}

//验证网址 uri
function isUri(obj) {
    // reg = /^(https | http):\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    reg = /^((https|http)?:\/\/)[^\s]+/;
    return reg.test(obj);
}

//验证网址 uri或者null,空
function isUriOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^((https|http)?:\/\/)[^\s]+/;
       // reg = /^(https | http):\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
        return reg.test(controlObj);
    }
}

//验证两个值是否相等 equals
function isEqual(obj1, controlObj) {
    if (obj1.length != 0 && controlObj.length != 0) {
        return obj1 == controlObj;
    } else {
        return false;
    }
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date
function isDate(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        return reg.test(obj);
    } else {
        return false;
    }
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date或者null,空
function isDateOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        return reg.test(controlObj);
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime
function isDateTime(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        return reg.test(obj);
    } else {
        return false;
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime或者null,空
function isDateTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        return reg.test(controlObj);
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time
function isTime(obj) {
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        return reg.test(obj);
    } else {
        return fasle;
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time或者null,空
function isTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        return reg.test(controlObj);
    }
}

//判断输入的字符是否为中文 cchar 
function isChinese(obj) {
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        return reg.test(str);
    } else {
        return false;
    }
}

//判断输入的字符是否为中文 cchar或者null,空
function isChineseOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^[\u0391-\uFFE5]+$/;
        return reg.test(controlObj);
    }
}

//判断输入的邮编(只能为六位)是否正确 zip
function isZip(obj) {
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        return reg.test(obj);
    } else {
        return false;
    }
}

//判断输入的邮编(只能为六位)是否正确 zip或者null,空
function isZipOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^\d{6}$/;
        return reg.test(controlObj);
    }
}

//判断输入的字符是否为双精度 double
function isDouble(obj) {
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        return reg.test(obj);
    } else {
        return false;
    }
}

//判断输入的字符是否为双精度 double或者null,空
function isDoubleOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    } else {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        return reg.test(controlObj);
    }
}

//验证营业执照编号
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
        if ((1 == (s[14] % m)) || (1 == (s[17] % m))) {
            ret = true;//营业执照编号正确!
        } else {
            ret = false;//营业执照编号错误!
        }
    } else if ("" == busCode) {
        ret = true;
    }
    return ret;
}

//验证是否是图片
function CheckImgExtName(imgUrl) {
    var extion = ".jpg.png.bmp.jpeg.JPG.PNG.BMP.JPEG";
    var len = imgUrl.lastIndexOf(".");
    var extName = imgUrl.substr(len, imgUrl.length - len);
    return extion.indexOf(extName) > -1;
}