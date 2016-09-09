/* site.js */
(function () {

    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        } else {
            $(this).text("Hide Sidebar");
        }
    })   


    //var ele = $("#username");
    //ele.text("Surya Chundru");

    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.css("background-color", "#888");
    //});
    //main.on("mouseleave", function () {
    //    main.css("background-color", "");
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert (me.text ());
    //});

    //var ele = document.getElementById("username");
    //ele.innerHTML = "Surya Chundru";

    //var main = document.getElementById("main");
    //main.onmouseenter = function () {
    //    main.style.backgroundColor = "#888";
    //};

    //main.onmouseleave = function () {
    //    main.style.backgroundColor = "";
    //};
}) ();
