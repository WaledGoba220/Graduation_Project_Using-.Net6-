$(".openModelComment").on('click', function () {
    $("#modelComment").css('display', 'block');
    $("#modelComment .modal-content").animate({
        top: 0
    }, 300);
});

// When the user clicks on <span> (x), close the modal
$(".close3").on('click', function () {
    $("#modelComment").css('display', 'none');
    $("#modelComment .modal-content").animate({
        top: '-300px'
    }, 300);
});
