function callApi() {
    var r = new XMLHttpRequest();
    r.onreadystatechange = function () {
        if (r.readyState == XMLHttpRequest.DONE && r.status == 200) {
            var o = JSON.parse(r.response);
            document.getElementById("title").innerHTML = o.Title;
            document.getElementById("description").innerHTML = o.Description;
        }
    };
    r.open("GET", "/api/home", true);
    r.send();
}