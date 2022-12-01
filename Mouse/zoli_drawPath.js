$(function () {
    var ctx = $("#c")[0].getContext("2d");
    var path = [];
    var count = 0;
    var string = "";
    
    function drawPath(pts, ctx) {
        ctx.beginPath();
        for (var i = 0; i < pts.length; i++) {
            var x = pts[i].x;
            var y = pts[i].y;
            ctx.lineTo(x, y);
        }
        ctx.lineWidth = 8;
        ctx.strokeStyle = "#000";
        ctx.stroke();
    }

    function clearCanvas(ctx) {
        ctx.clearRect(0, 0, 800, 600);
    }

    $(document).on("selectstart", function () {
        return false;
    });

    $("#btn1").on("click", function () {
        clearCanvas(ctx);
        
        var spl = $("#txt1").val().split("]");
        spl.length = spl.length - 1;
        for (i = 0; i < spl.length; i++) {
            spl[i] += "]";
        }
        
        for (i = 0; i < spl.length; i++) {
            drawPath(JSON.parse(spl[i]), ctx);
        }
    });

    $("#btn2").on("click", function () {
        string = "";
        path = [];
        $("#txt1").val(string);
        clearCanvas(ctx);
    });

    $("#c").on("mousedown", function (e) {
        console.log("Path start");
        path[count] = [];
        $("#c").on("mousemove", function(e){
            var x = e.pageX - $("#c").offset().left
            var y = e.pageY - $("#c").offset().top
            path[count].push({
                "x": x, 
                "y": y
            });
        });
    });

    $("#c").on("mouseup", function () {
        console.log("Path end");
        $("#c").off("mousemove");
        string += JSON.stringify(path[count]);
        $("#txt1").val(string);
        drawPath(path[count], ctx);
        count++;
    });
});