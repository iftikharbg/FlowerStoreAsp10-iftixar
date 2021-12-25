$(document).ready(() => {
    $("#search").on("keyup", function () {
        const value = $(this).val()
        $.ajax({
            type: "POST",
            url: "Category/search",
            data: {
                search:value
            },
            success: function (res) {
                $("#tablebody").empty()
                $("#tablebody").append(res)
            }


        })
    })
})