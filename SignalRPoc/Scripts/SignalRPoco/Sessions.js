function updateLockedRecords() {
        $.ajax({
            url: "/api/Sessions",
            type: "GET",
            dataType: "json",
            success: function (data) {
                $("[id^=display-for-model-]").removeClass("locked");

                $.each(data,
                    function (index, element) {
                        var displayId = "#display-for-model-" + element.RecordId;
                        $(displayId).addClass("locked");
                    });
            },
            error: function () {
                alert("Error pulling back data from /api/sessions in sessionsHub function");
            }
        });
}

function appendSignalRClientIdToEditQueryStrings(signalRClientId) {
    $("a.editor-link").each(function() {
        var newHref = $(this).attr("href") + "?signalRClientID=" + signalRClientId;
        $(this).attr("href", newHref);
    });
}

$(document).ready(function () {
    updateLockedRecords();
});

(function () {
    var sessionsHub = $.connection.sessionsHub;
    $.connection.hub.logging = true;
    $.connection.hub.start().done(function() {
        appendSignalRClientIdToEditQueryStrings($.connection.hub.id);
    });

    sessionsHub.client.sessionsChanged = function () {
        //TODO: split these 2 steps out into separate scripts for the 2 pages that use session info

        //If we're on the sessions listing page, update the list
        $("#sessions").load("/Home/SessionsPartial");

        //If we're in the edit page, pull back the list of edit sessions then update the display
        updateLockedRecords();
    };
})();