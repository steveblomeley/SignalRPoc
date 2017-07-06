jQuery.fn.showAlertMessage = function (message) {
    return $(this).empty().append(message).removeClass("hidden").show().delay(3000).hide(2000);
};

function PostSuccess(data) {
    $("#alert-success").showAlertMessage(data.Message);
    var editorId = "#editor-for-model-" + data.Id;
    $(editorId).prev("tr").remove();
    $(editorId).remove();
    if ($("[id^=editor-for-model-]").length === 0) {
        $("#no-more-data").removeClass("hidden");
    }
}

function PostFail(data) {
    $("#alert-error").showAlertMessage(data.responseJSON.Message);
    var editorId = "#editor-for-model-" + data.responseJSON.Id;
    $(editorId).empty();
}