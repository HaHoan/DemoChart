$(function () {
    var date = $('#DATE_DETAIL').val();
    changeSeletedDate(date);
});
$('#DATE_DETAIL').change(function () {
    var date = $(this).val();
    changeSeletedDate(date);
});
function changeSeletedDate(date) {
    $.ajax({
        url: "/Home/ChangeSelectedDate",
        async: true,
        data: { date: date},
        success: function (response) {
            $('#VN').val(response.VN);
            $('#TG').val(response.TG);
        },
        error: function (e) {
            alert(e);
        }
    });
}