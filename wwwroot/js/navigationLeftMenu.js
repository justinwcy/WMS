// to show the menu
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");

});

//to show the dropdown
$(document).ready(function () {
    $('.navDropdown').hover(function () {
        $(this).addClass('show');
        $(this).find('.dropdown-menu').addClass('show');
    }, function () {
        $(this).removeClass('show');
        $(this).find('.dropdown-menu').removeClass('show');
    });
});

// to animate the dropdown to the left side
$(document).ready(function () {
    $('.navDropdown').hover(function () {
        $(this).addClass('show');
        $(this).find('.dropdown-menu').animate({
            left: "200px"
        }, 200);
    }, function () {
        $(this).removeClass('show');
        $(this).find('.dropdown-menu').animate({
            left: "0px"
        }, 200);
    });
});