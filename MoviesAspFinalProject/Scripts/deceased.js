$("input[name='Deceased']").change(function () {
    var deathform = $("div[name = 'DeathDay']");
    var deathinput = $("input[name = 'DeathDay']");
    if (this.checked) {
        deathform.show();
    }
    else {
        deathform.hide();
        deathinput.val("January 01, 0001");
        deathinput.attr("value", "January 01, 0001");
    }
})