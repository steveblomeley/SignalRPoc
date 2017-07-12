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

    $("#display-for-model-" + data.Id).remove();
    $("#editor-for-model-" + data.Id).remove();

    if ($("[id^=editor-for-model-]").length === 0) {
        $("#no-more-data").removeClass("hidden");
    }
}

function editCancelled(data) {
    $("#alert-info").showAlertMessage(data.Message);

    $("#editor-for-model-" + data.Id).empty();
}

function editPostFailure(data) {
    $("#alert-error").showAlertMessage(data.responseJSON.Message);

    $("#editor-for-model-" + data.responseJSON.Id).empty();
}

function editPost(e) {
    var target = (e.target) ? e.target : e.srcElement;
    var action = $(target).attr("data-action");
    $(target).closest("form").attr("action", action);
}

$(document).ready(function() {
    $("body").on("submit",
        "form",
        function() {
            $(this).find(":submit").attr("disabled", "disabled");
        });
});