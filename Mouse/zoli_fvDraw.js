function drawPath(pts, ctx) {
    ctx.beginPath();
    for (var i = 0; i < pts.length; i++) {
        var x = 30 * pts[i].x + 400;
        var y = -30 * pts[i].y + 300;
        ctx.lineTo(x, y);
    }
    ctx.stroke();
}

function drawPath2(pts, ctx) {
    var n = 0;
    var iid = setInterval(function () {
        clearCanvas(ctx);
        drawPath(pts.slice(0, n), ctx);
        if (++n == pts.length) {
            clearInterval(iid);
        }
    }, 20);
}

function clearCanvas(ctx) {
    ctx.clearRect(0, 0, 800, 600);
}

function getPath(f, start, end, step) {
    var arr = [];
    for (var i = start; i < end; i += step) {
        arr.push({
            x : i,
            y : f(i)
        });
    }
    return arr;
}

function getSelectedF() {
    return document.getElementById("fs").value;
}

var fs = {
    "sin" : function (x) {
        return Math.sin(x);
    },
    "pow2" : function (x) {
        return Math.pow(2, x);
    },
    "sqr" : function (x) {
        return x * x;
    },
    "sqrt" : function (x) {
        return Math.sqrt(x);
    }
};

window.onload = function () {
    for (var f in fs) {
        var option = document.createElement("option");
        option.setAttribute("value", f);
        option.appendChild(document.createTextNode(f));
        document.getElementById("fs").appendChild(option);
    }

    var ctx = document.getElementById("c").getContext("2d");
    ctx.strokeStyle = "#000";
    ctx.lineWidth = 3;

    document.getElementById("btn1").onclick = function () {
        var v = getSelectedF();
        var p = getPath(fs[v], -15, 15, 0.25);
        drawPath(p, ctx);
    };

    document.getElementById("btn2").onclick = function () {
        var v = getSelectedF();
        var p = getPath(fs[v], -15, 15, 0.25);
        drawPath2(p, ctx);
    };

    document.getElementById("btn3").onclick = function () {
        clearCanvas(ctx);
    };
};