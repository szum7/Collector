$(function () {
    /* "Public" */
    var drawSpeed = 20;
    var timeBetween = 200;
    var lineWidth = 3;
    var lineColor = "#000";
    
    /* HTML Elements */
    var button1 = "#btn0";
    var button2 = "#btn1";
    var button3 = "#btn2";
    var button4 = "#btn3";
    var button5 = "#btn4";
    var textarea1 = "#txt1";
    var canvas1 = "#c";
    
    /* "Private" */
    var ctx = $(canvas1)[0].getContext("2d");
    var path = [];
    var count = 0;
    var string = "";
    var running = false;
    
    var hexvalues = Array( "A", "B", "C", "D", "E", "F", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" );
    function random_C() {
        var colour = "#";
        for(var counter = 1; counter <= 6; counter++) {
            var hexvalue = hexvalues[Math.floor( hexvalues.length * Math.random())];
            colour = colour + hexvalue;
        }
        return colour;
    }
    
    function drawPath(pts, ctx) {
        ctx.beginPath();
        for (var i = 0; i < pts.length; i++) {
            var x = pts[i].x;
            var y = pts[i].y;
            ctx.lineTo(x, y);
        }
        ctx.lineWidth = lineWidth;
        ctx.strokeStyle = lineColor;
        ctx.stroke();
    }
    
    function drawPath_anim(pts, ctx) {
        var n = 0;
        var iid = setInterval(function () {
            //clearCanvas(ctx);
            drawPath(pts.slice(0, n), ctx);
            if (++n == pts.length) {
                clearInterval(iid);
                running = false;
            }
        }, drawSpeed);
    }

    function clearCanvas(ctx) {
        ctx.clearRect(0, 0, 1500, 550);
    }

    $(document).on("selectstart", function () {
        return false;
    });

    /* Clear all */
    $(button1).on("click", function () {
        string = "";
        path = [];
        $(textarea1).val(string);
        clearCanvas(ctx);
    });
    
    /* Clear canvas */
    $(button2).on("click", function () {
        string = "";
        path = [];
        clearCanvas(ctx);
    });
    
    /* Delete last */
    $(button3).on("click", function () {
        clearCanvas(ctx);
        string = "";
        path = [];
        
        var spl = $(textarea1).val().split("]");
        spl.length = spl.length - 1;
        for (i = 0; i < spl.length - 1; i++) {
            spl[i] += "]";
            string += spl[i];
            path[i] = JSON.parse(spl[i]);
            drawPath(JSON.parse(spl[i]), ctx);
        }
        $(textarea1).val(string);
        
        /*spl.length = spl.length - 1;
        spl[spl.length] = null;
        spl.length = spl.length - 1;
        $(textarea1).val(spl);
        /*for (i = 0; i < spl.length; i++) {
            $(textarea1).val() += spl[i];
        }*/
    });
    
    /* Basic draw */
    $(button4).on("click", function () {
        clearCanvas(ctx);
        
        var spl = $(textarea1).val().split("]");
        spl.length = spl.length - 1;
        for (i = 0; i < spl.length; i++) {
            spl[i] += "]";
        }
        
        for (i = 0; i < spl.length; i++) {
            drawPath(JSON.parse(spl[i]), ctx);
        }
    });
    
    /* Animated draw */
    $(button5).on("click", function () {
        clearCanvas(ctx);
        
        var spl = $(textarea1).val().split("]");
        spl.length = spl.length - 1;
        for (i = 0; i < spl.length; i++) {
            spl[i] += "]";
        }
        
        var counter = 0;
        var draw_intval = setInterval(function () {
            if(!running){
                running = true;
                drawPath_anim(JSON.parse(spl[counter]), ctx);
                counter++;
            }
            if(counter == spl.length){
                clearInterval(draw_intval);
            }
        }, timeBetween);
    });


    $(canvas1).on("mousedown", function (e) {
        console.log("Path start");
        path[count] = [];
        $(canvas1).on("mousemove", function(e){
            var x = e.pageX - $(canvas1).offset().left
            var y = e.pageY - $(canvas1).offset().top
            path[count].push({
                "x": x, 
                "y": y
            });
        });
    });

    $(canvas1).on("mouseup", function () {
        console.log("Path end");
        $(canvas1).off("mousemove");
        string += JSON.stringify(path[count]);
        $(textarea1).val(string);
        drawPath(path[count], ctx);
        count++;
    });
});