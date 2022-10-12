﻿$((function () {
    var url;
    var redirectUrl;
    var target;

    $('body').append(`
             <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="myModalLabel"> </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body delete-modal-body">

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" id="cancel-delete">Cancel</button>
                    <button type="button" class="btn btn-primary Confirm-btn" data-bs-dismiss="modal" id="confirm-delete" >Confirm</button>
                </div>
                </div>
            </div>
            </div>`);

    //Delete Action
    $(".delete").on('click', (e) => {
        e.preventDefault();

        target = e.target;
        var Id = $(target).data('id');
        var controller = $(target).data('controller');
        var label = $(target).data('label');
        var action = $(target).data('action');
        var bodyMessage = $(target).data('body-message');
        var actionUrl = $(target).data('action-url');
        redirectUrl = $(target).data('redirect-url');
        if (action == "Delete") {
            url = actionUrl;// "/" + controller + "/" + action + "?Id=" + Id;
        }
        $("#myModalLabel").text(label);
        $(".delete-modal-body").text(bodyMessage);
        $("#deleteModal").modal('show');
    });

    $("#confirm-delete").on('click', () => {
         $.ajax({
             type: "DELETE",
             url: url,
             contentType: "application/json;charset=utf-8",
             success: () => {
                 isSuccess = true;
                 $("#deleteModal").modal('hide');
                // window.location.reload();
             },
             complete: () => {
                 if (isSuccess) $(this).parent().parent().hide();
             }
         });
       /* $.get(url)
            .done((result) => {
                if (!redirectUrl) {
                    return $(target).parent().parent().hide("slow");
                }
                window.location.href = redirectUrl;
            })
            .fail((error) => {
                if (redirectUrl)
                    window.location.href = redirectUrl;
            }).always(() => {
                $("#deleteModal").modal('hide');
            });*/
    });

}()));