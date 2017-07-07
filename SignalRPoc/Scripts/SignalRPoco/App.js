jQuery.fn.showAlertMessage = function (message) {
    return $(this).empty().append(message).removeClass("hidden").show().delay(1000).hide(2000);
};

function editPostSuccess(data) {
    if (data.Cancelled) {
        editCancelled(data);
    } else {
        editSaved(data);
    }
}

function editSaved(data) {
    $("#alert-success").showAlertMessage(data.Message);

    var editorId = "#editor-for-model-" + data.Id;

    $(editorId).prev("tr").remove();
    $(editorId).remove();

    if ($("[id^=editor-for-model-]").length === 0) {
        $("#no-more-data").removeClass("hidden");
    }
}

function editCancelled(data) {
    $("#alert-info").showAlertMessage(data.Message);

    var editorId = "#editor-for-model-" + data.Id;

    $(editorId).empty();
}

function editPostFailure(data) {
    $("#alert-error").showAlertMessage(data.responseJSON.Message);

    var editorId = "#editor-for-model-" + data.responseJSON.Id;

    $(editorId).empty();
}

function editPost(e) {
    var target = (e.target) ? e.target : e.srcElement;
    var action = $(target).attr("data-action");
    $(target).closest("form").attr("action", action);
}

function putSignalRClientIdInHiddenField() {
    var signalRClientId = $.connection.hub.id;
    $("input[name='SignalRClientId']").val(signalRClientId);
}