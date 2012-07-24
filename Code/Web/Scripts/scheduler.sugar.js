function GetTeams1() {
    GetTeams($("#Division1Id"), $("#Team1Id"));
}
function GetTeams2() {
    GetTeams($("#Division2Id"), $("#Team2Id"));
}
 
function GetTeams(ddlSource, ddlTarget) {

    var success = function (results) {
        $(ddlTarget).html("");
        var options = "";
        for (var i = 0; i < results.length; i++) {
            options += "<option value='" + results[i].Value + "'>" + results[i].Text + "</option>";
        }
        $(ddlTarget).html(options);
    };
 
    $.ajax({ url: '/Team/GetByDivision', 
        type: 'POST', 
        data: { division : $(ddlSource).val() }, 
        dataType: 'json',
        success: success 
    });
}