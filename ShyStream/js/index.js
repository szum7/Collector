var user = (function() {
    //Public properties
    var pub = {
        "istyping": false,
        "isonline": false
    };

    //Private properties

    //Public methods
    pub.setTyping = function() {
        var request = $.ajax({
            url: "actions.php",
            type: "POST",
            data: {
                type: "setTyping",
                istyping: user.istyping
            },
            success: function(msg) {

            },
            error: function() {
                console.log("Source was not found!");
            }
        });
    };

    pub.setOnline = function() {
        var request = $.ajax({
            url: "actions.php",
            type: "POST",
            data: {
                type: "setOnline",
                isonline: user.isonline
            },
            success: function(msg) {

            },
            error: function() {
                console.log("Source was not found!");
            }
        });
    };

    //Private methods

    return pub;
}());

var comments = (function() {
    //Public properties
    var pub = {
        "stream": []
    };

    //Private properties
    var hasNew = false;

    //Public methods
    /* pub.checkLength = function() {
        // OLD METHOD 
        if (comments.stream !== $("#comments").html() && !($("#comments").is(":hover"))) {
            $("#comments").animate({
                scrollTop: $("#bottom").height()
            }, 500);
            comments.stream = $("#comments").html();
        }
    };*/

    pub.checkForNewComment = function() {
        if (hasNew && $("body").is(":hover")) {
            hasNew = false;
            resetTitle();
        }

        if (!hasNew) {
            var linesNow = [];
            $("div.line").each(function(i, obj) {
                linesNow.push($(obj).data("lineid"));
            });

            if (linesNow.length > comments.stream) {
                hasNew = true;
            } else {
                /* check if every mysql comment line can be found in js stream (array) */
                for (var i = 0; i < linesNow.length; i++) {
                    if (jQuery.inArray(linesNow[i], comments.stream) === -1) {
                        hasNew = true;
                        break;
                    }
                }
            }

            if (hasNew && !($("#comments").is(":hover"))) {
                $("#comments").animate({scrollTop: $("#comments")[0].scrollHeight }, 500);
                if (!($("body").is(":hover"))) {
                    document.title = document.title + " (NEW!)";
                }
                comments.stream = linesNow;
            }
        }
    };

    //Private methods
    function resetTitle() {
        document.title = "Stream";
    }

    return pub;
}());

function deleteThis(id) {
    var request = $.ajax({
        url: "actions.php",
        type: "POST",
        data: {
            type: "deleteComment",
            id: id
        },
        error: function() {
            console.log("Source was not found!");
        }
    });
}

function simpleAjax(type) {
        var request = $.ajax({
            url: "actions.php",
            type: "POST",
            data: {
                type: type
            },
            error: function() {
                console.log("Source was not found!");
            }
        });
    }

$(document).ready(function() {

    $("#btn_register input").keypress(function(event) { // register
        if (event.which == 13) {
            event.preventDefault();
            var username = $("#r-name").val();
            var password = $("#r-password").val();
            var color = "#" + $.trim($("#r-color").val());
            if ($.trim(username) !== "" && $.trim(password) !== "" && (color) !== "" && (/(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test(color))) {
                document.forms["register"].submit();
            }
        }
    });

    $("#btn_login input").keypress(function(event) { // login
        if (event.which == 13) {
            event.preventDefault();
            var username = $("#l-name").val();
            var password = $("#l-password").val();
            if ($.trim(username) !== "" && $.trim(password) !== "") {
                document.forms["login"].submit();
            }
        }
    });

    $("#data form input").keypress(function(event) {
        if (event.which == 13) {
            event.preventDefault();
            var username = $.trim($("#m-name").val());
            var color = $.trim($("#m-color").val());
            if ((username !== "" && ((/(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test("#" + color))
                    || color === "")) || (username === "" && (/(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test("#" + color)))) {
                document.forms["modify"].submit();
            }
        }
    });

    $("#comment").keypress(function(event) {
        if (event.which == 13) {
            event.preventDefault();
            var comment = $("#comment").val();
            if ($.trim(comment) !== "") {
                var request = $.ajax({
                    url: "actions.php",
                    type: "POST",
                    data: {
                        type: "createComment",
                        comment: comment
                    },
                    error: function() {
                        console.log("Source was not found!");
                    }
                });
            }
            $("#comment").val("");
        }
    });

    $("p.btn").mousedown(function() {
        $(this).addClass("on");
    }).mouseup(function() {
        $(this).removeClass("on");
    });

    /* main navigation */
    var isinprogress = false;
    $("ul li div.menu").click(function() {

        if (!isinprogress) {
            isinprogress = true;

            var hw = $(this).parent().find("div.hidden").css("width");
            var hw_w = $(this).parent().find("div.hidden").data("width");

            if (hw === "0px") {

                // find out if a menu is open
                var hasopen = false;
                $("ul li div.hidden").each(function(i, obj) {
                    if ($(obj).css("width") === ($(obj).data("width") + "px")) {
                        hasopen = true;
                    }
                });
                if (hasopen) {
                    $("ul li div.hidden").animate({
                        width: "0px"
                    }, 500); // reset all
                }

                $(this).parent().find("div.hidden").animate({
                    width: hw_w + "px"
                }, 500, function() {
                    isinprogress = false;
                }); // show selected

            } else if (hw === (hw_w + "px")) {

                $("ul li div.hidden").animate({
                    width: "0px"
                }, 500, function() {
                    isinprogress = false;
                }); // reset all (previously selected)

            } else {
                // animation in progress
            }
        }

    });

    /* Prevent form submitting on ENTER key press */
    document.onkeypress = (function(evt) {
        var evt = (evt) ? evt : ((event) ? event : null);
        var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        if ((evt.keyCode == 13) && (node.type == "text")) {
            return false;
        }
    });

    var timer;

    $("#comment").keyup(function() {

        if (!(user.istyping)) {
            user.istyping = true;
            user.setTyping();
        }

        clearTimeout(timer);
        timer = setTimeout(function() {
            if (user.istyping) {
                user.istyping = false;
                user.setTyping();
            }
        }, 1000);
    });

    $("#comment").focusout(function() {
        if (user.istyping) {
            user.istyping = false;
            user.setTyping();
        }
    });
    
    var chaters_load = setInterval(function() {
        $("#chaters").load("ajax/chaters.php");
    }, 1000);
    
    var comments_load = setInterval(function() {
        $("#comments").load("ajax/comments.php", function() {
            comments.checkForNewComment();
        });
    }, 1000);
    
    var online_counter = 0;
    var online_timer = setInterval(function() { // sets user offline if BROWSER ON and PAST 5 MINUTES
        if (!($("body").is(":hover"))) {
            online_counter++;
            if (online_counter === (60 * 5)) { // 5 minutes (interval(1 sec = 1000) *) 60 * 5
                if (user.isonline) {
                    user.isonline = false;
                    user.setOnline();
                }
            }
        } else {
            online_counter = 0;
            if (!(user.isonline)) {
                user.isonline = true;
                user.setOnline();
            }
        }
    }, 1000);
    
    var online_chaters_check = setInterval(function() { // sets every users offline who's mysql lastactivity value PAST 5 MINUTES
        var cc = 0;
        $("div.chater").each(function(i, obj) {
            cc++;
        });
        if (cc > 0) {
            simpleAjax("checkOnlineChaters");
        }
    }, 1000);
    
    var activity_setter = setInterval(function() { // sets user offline if BROWSER OFF and PAST 5 MINUTES
        if ($("body").is(":hover")) {
            simpleAjax("setUserLastactivity");
        }
    }, 1000);

    

});