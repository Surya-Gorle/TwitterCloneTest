$(document).ready(function () {
    $("#registerCreate").click(function (event) {
        if ($("form").valid()) {
            return true;
        }
    })
    $("a.edtLink").click(function (event) {
        var id = $(this).attr('id').split('_')[1];
        $('#btnSave_'.concat(id)).css('visibility', 'visible');
        $('#btnCancel_'.concat(id)).css('visibility', 'visible');
        $('#'.concat(id)).removeAttr('disabled');
    })
    $("input").click(function (event) {
        var id = $(this).attr('id');
        if (id == 'btnEdtPwd') {
            $('#pwdVal').css('visibility', 'visible');
        }
    })
    $("input").click(function (event) {
        var id = $(this).attr('id').split('_')[1];
        var btn = $(this).attr('id').split('_')[0];
        if (btn == "btnSave") {
            $('input[name="TweetID"]').val(id);
            var len = $("#".concat(id)).val().length;
            if (len > 140) {
                alert('Tweet length cannot be greater than 140 characters')
                return false;
            }
        }
        if (btn == "btnDelete") {
            $('input[name="TweetID"]').val(id);
            if (window.confirm('Are you sure to Delete the tweet')) {
                return true;
            }
            else {
                event.preventDefault();
            }
        }
        return true;
    })
    $("#btnShare").click(function (event) {
        var len = $("#msgTweet").val().length;
        if (len > 140) {
            alert('Tweet length cannot be greater than 140 characters')
            return false;
        }
        return true;
    })
    $("#btnLogout").click(function (event) {
        if (window.confirm('Are you sure you want to Logout')) {
            return true;
        }
        else {
            event.preventDefault();
        }
    })
    $("#btnExit").click(function (event) {
        if (window.confirm('Are you sure you want to exit from this application')) {
            return true;
        }
        else {
            event.preventDefault();
        }
    })
});
