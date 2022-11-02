window.onload = function () {
    var fileupload = document.getElementById("FL");
    var filePath = document.getElementById("spnFilePath");
    var button = document.getElementById("btnSubmit");
    var buttonUpload = document.getElementById("btnUpload");
    button.onclick = function () {
        fileupload.click();
    };
    fileupload.onchange = function () {
        //var fileName = fileupload.value.split('\\')[fileupload.value.split('\\').length - 1];
        if (fileupload.files.length == 0) {
            buttonUpload.disabled = true;
        }
        else {
            buttonUpload.disabled = false;
        }
        var fileName = $(this).val().split("\\").pop();
        filePath.innerHTML = "<b>Selected File: </b>" + fileName;
        fileValidation();
    };
};

//var ImgUploads = document.getElementsByClassName("custom-file-input");
//for (let i = 0; i < ImgUploads.length; i++) {
//    ImgUploads[i].addEventListener("change", function () {
//        var x = document.getElementById('FileUrl').href = window.URL.createObjectURL(this.files[0]);
//        $("#FileUrl").prop("href", x)

//        var fileName = $(this).val().split("\\").pop();
//        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
//        $("h6").text(fileName);
//        $("p").text();
//    });
//}
function fileValidation() {
    let header;
    var fileInput = document.getElementById('FL');
    var filePath = fileInput.value;
   // var x=true;

    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.csv|\.xls|\.xlsx|\.pdf)$/i;

    if (!allowedExtensions.exec(filePath)) {
        header = document.querySelector("#spnFilePath").innerText = "";
        header = document.querySelector("#spnFileName");
        header.innerText = "Failed to upload !! Please upload csv,jpg, jpeg, png, pdf file only.";
       // x = false;
        fileInput.value = '';
    }
    else {
        header = document.querySelector("#spnFileName").innerText = "";
    }
    if (FL.files.length > 0) {
        for (const i = 0; i <= FL.files.length - 1; i++) {
            const fsize = FL.files.item(i).size;
            const file = Math.round((fsize / 1024));

            if (fsize >= 32768) {
                header = document.querySelector("#spnFilePath").innerText ="";
                header = document.querySelector("#spnFileName").innerText = "Failed to upload !! Max allowed file size is 32kb";
           //     x = false;
                fileInput.value = '';
            } else {
                header = document.querySelector("#spnFileName");
                header.innerText = "";
            }
        } fileInput.value = '';
    }
   // return x;
}