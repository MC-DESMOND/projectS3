var filterBtn = document.getElementById("filter")
    var nameInput = document.getElementById("name")
    var deptInput = document.getElementById("dept")
    var typeInput = document.getElementById("type")

    filterBtn.onclick = ()=>{
        var name = String(nameInput.value).trim() == "" ? "" : "name=" + nameInput.value
        var dept = String(deptInput.value).trim() == "" ? "" : "department=" + deptInput.value
        var type = String(typeInput.value).trim() == "" ? "" : "type=" + typeInput.value
        window.open(`/filter?${[name,dept,type].join("&")}`,"_self").focus()
    }
