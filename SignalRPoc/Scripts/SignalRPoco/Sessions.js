(function () {
    var sessionsHub = $.connection.sessionsHub;
    $.connection.hub.logging = true;
    $.connection.hub.start();
        //.done(function () { alert("Now connected to SignalR hub, connection ID=" + $.connection.hub.id); })
        //.fail(function () { alert("Could not connect to SignalR hub!")});

    sessionsHub.client.sessionsChanged = function () {
        $("#sessions").load("/Home/SessionsPartial");
    };
})();