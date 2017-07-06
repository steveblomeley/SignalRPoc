(function () {
    var sessionsHub = $.connection.sessionsHub;
    $.connection.hub.logging = true;
    $.connection.hub.start();
        //.done(function () { alert("Now connected to SignalR hub, connection ID=" + $.connection.hub.id); })
        //.fail(function () { alert("Could not connect to SignalR hub!")});

    sessionsHub.client.sessionsChanged = function () {
        //this is called from the server to signal to clients that the list of sessions has changed
        //in response, the client will hit the web api at /api/sessions to retrieve the updated list of sessions
        //it will use this list to update the DOM to reflect who is editing what

        //but for now, it just does this:
        $("#sessions").load("/Home/SessionsPartial");

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
            error: function (data) {
                alert("uh oh");
            }
        });
    };
})();