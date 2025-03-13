function GetData(url, dataObject, resultDiv = null, async = true) {
    let result = (resultDiv != null) ? resultDiv : "div_Results";

    $.ajax({
        url: url,
        async, async,
        type: "POST",
        async: false,
        dataType: "html",
        data: dataObject,
        success: function (data) {
            $(`#${result}`).html(data);
        },
        error: function (data) {
            let errorMsg = `${data.error}`;

            console.log(errorMsg);
        }
    });
}


function deleteData(id, url) {
    if (id == null) {
        console.log("Id is leeg");
    }

    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        async: true,
        data: {
            id: id,
        },
        success: function (data) {
            console.log('data', data);
            if (data.success) {
                console.log(data.message);
            } else {
                let errorMsg = `Error deleting data: ${data.message}`;
                console.log(errorMsg);
            }
        },
        error: function (data) {
            let errorMsg = `Error deleting data: ${data.message}`;
            console.log(errorMsg);
        }
    });
}