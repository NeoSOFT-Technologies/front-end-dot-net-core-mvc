$(document).ready(function () {

    $("#test-registers").DataTable({
        "lengthMenu": [2,4,8,10,20,50],
        autoWidth: true,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ajax: {
            url: "/Order/LoadTable",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            //data: function (data) {
            //    return JSON.stringify(data);
            //}
        },
        columns: [
            { data: "id" },
            { data: "orderTotal" },
            { data: "orderPlaced" }
            //{
            //    render: function (data, type, row) {
            //        return `<span style="color: green;">${data}</span>`;
            //    }
            //}
        ]
    });
});