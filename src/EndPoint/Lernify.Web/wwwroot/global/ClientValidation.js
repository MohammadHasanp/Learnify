function getExtension(path) {
    var basename = path.split(/[\\/]/).pop(),  // extract file name from full path ...
        // (supports `\\` and `/` separators)
        pos = basename.lastIndexOf(".");       // get last position of `.`

    if (basename === "" || pos < 1)            // if file name is empty or ...
        return "";                             //  `.` not found (-1) or comes first (0)

    return basename.slice(pos + 1);            // extract extension ignoring `.`
}
jQuery.validator.addMethod("fileSize", function (value, element, params) {
    if (!element.files || element.files.length === 0) return true;

    var fileSize = element.files[0].size / 1024 / 1024; // مگابایت
    var maxFileSizeAttr = $(element).attr("filesize");
    var maxFileSize = maxFileSizeAttr ? maxFileSizeAttr / 1024 / 1024 : 5; // پیش فرض 5MB

    return fileSize <= maxFileSize;
}, "حجم فایل انتخابی بزرگ‌تر از حد مجاز است.");



jQuery.validator.addMethod("fileSize", function (value, element, params) {
    if (!element.files || !element.files.length) return true;
    var fileSize = element.files[0].size / 1024 / 1024;
    var maxFileSize = $(element).attr("filesize") / 1024;
    return fileSize <= maxFileSize;
});

jQuery.validator.addMethod("fileType",
    function (value, element, params) {
        var fileType = getExtension(value.toLowerCase());
        var acceptType = $(element).attr("fileType");
        if (fileType === acceptType)
            return true;
        else
            return false;
    });
jQuery.validator.unobtrusive.adapters.addBool("fileType");
jQuery.validator.unobtrusive.adapters.addBool("fileSize");
jQuery.validator.unobtrusive.adapters.addBool("fileImage");