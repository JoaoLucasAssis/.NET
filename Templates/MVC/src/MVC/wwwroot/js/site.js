$(document).ready(function () {
    var clientId;
    var form = $("#antiForgeryForm");
    var token = form[0][0].value;

    $("#deleteModal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        clientId = button.data("id");
    });

    $("#confirmDeleteClient").click(function () {
        $.ajax({
            url: "/clients/delete-client/" + clientId,
            type: "POST",
            headers: { "RequestVerificationToken": token },
            success: function () {
                location.reload();
            },
            error: function () {
                alert("error when deleting a customer.");
            }
        });
    });
});