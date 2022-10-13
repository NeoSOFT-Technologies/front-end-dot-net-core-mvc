var ImgUploads = document.getElementsByClassName("custom-file-input");
for (let i = 0; i < ImgUploads.length; i++) {
    ImgUploads[i].addEventListener("change", function () {
        var x = document.getElementById('FileUrl').href = window.URL.createObjectURL(this.files[0]);
        $("#FileUrl").prop("href", x)

        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        $("h6").text(fileName);
        $("p").text();
    });
}
function fileValidation() {
    var fileInput = document.getElementById('FL');
    let header;
    var filePath = fileInput.value;

    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.csv|\.xls|\.pdf)$/i;

    if (!allowedExtensions.exec(filePath)) {
        // alert('Invalid file type');
        header = document.querySelector("p");
        header.innerText = "Failed to upload !! Please upload csv,jpg, jpeg, png, pdf file only.";

        ;
        fileInput.value = '';

        // return false;
    }
    else {
        header = document.querySelector("p");
        header.innerText = "";
    }
    if (FL.files.length > 0) {
        for (const i = 0; i <= FL.files.length - 1; i++) {

            const fsize = FL.files.item(i).size;
            if (fsize >= 32768) {
                alert("a-" + fsize + "b-" + file);
                header.innerText = "Failed to upload !! Max allowed file size is 1Mb";
                fileInput.value = '';

            } else {
                header = document.querySelector("p");
                header.innerText = "";
            }

        } fileInput.value = '';
    }
}