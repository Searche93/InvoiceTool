function getData(url, dataObject, resultDiv = "div_Results") {
    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: new URLSearchParams(dataObject)
    })
    .then(response => {
        if (!response.ok) {
            console.log(`HTTP error! Status: ${response.status}`);
        }
        return response.text();
    })
    .then(data => {
        if (document.getElementById(resultDiv)) {
            document.getElementById(resultDiv).innerHTML = data;
        } else {
            console.warn(`Element met ID '${resultDiv}' niet gevonden.`);
        }
    })
    .catch(error => console.error(`Fout bij ophalen van ${url}:`, error));
}

function saveData(url, data, callback) {
    fetch(`${url}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            if (callback) callback();
        })
        .catch(error => console.error(`Error saving in ${url}:`, error));
}

function deleteData(url, callback) {
    fetch(`${url}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
    })
    .then(response => response.json())
        .then(data => {
        if (callback) callback();
    })
    .catch(error => console.error(`Error deleting in ${url}:`, error));
}

function reloadPage() {
    location.reload();
}