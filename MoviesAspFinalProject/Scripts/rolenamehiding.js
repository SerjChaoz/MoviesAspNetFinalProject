$("input[name='Ids']").change(function () {
    var roleName = $("#r_" + this.id);
    if (this.checked)
    {
        roleName.removeAttr("disabled");
        roleName.prop("disabled", false);
    }
    else
    {
        roleName.attr("disabled", "disabled");
        roleName.prop("disabled", true);
    }
})