<!--
To change this template, choose Tools | Templates
and open the template in the editor.
-->
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>
        <title>Mouse</title>
        <style>
            body {
                margin: 0;
                padding: 0;
            }

            #c {
                border: 3px solid #eee;
            }
            #canvasdiv{
                border:1px solid black;
                width:900px;
                height:400px;
                display:inline-block;
            }
        </style>
    </head>
    <body>
        <!--<div id="canvasdiv" 
             onmouseover="
                 mouseCoords = null; 
                 mouseCoords = []; 
                 mouseInterval = self.setInterval(function(){getMouseCoords(event);}, 100);"
             >-->
        <div id="canvasdiv">
            <!--<button id="save">Save</button>-->
            <canvas id="c" width="800" height="400"></canvas>
        </div>
        <div id="t">
            <br/>
            <select id="fs"></select>
            <button id="btn2">Animated draw</button>
            <button id="btn3">Clear</button>
        </div>
        <script>
            /* ================== */
            var mouseInterval;
            var mouseCoords = [];
            var running = false;
            
            document.onkeyup = KeyCheck;       
            function KeyCheck(e)
            {
                var KeyID = (window.event) ? event.keyCode : e.keyCode;
                switch(KeyID)
                {
                    case 32:
                        running = !running;
                        console.log("Running:" + running);
                        break; 
                }
            }
            
            jQuery(document).ready(function(){
                $("#canvasdiv").mousemove(function(e){
                    if(running){
                        console.log(e.pageX + " " + e.pageY);
                        mouseCoords.push({
                            x : e.pageX,
                            y : e.pageY
                        });
                    }
                }); 
            })
            
            function getMouseCoords(event){
                if(running){
                    var xC = event.clientX;
                    var yC = event.clientY;
                    console.log(xC + " " + yC);
                    mouseCoords.push({
                        x : xC,
                        y : yC
                    });
                }else{
                    window.clearInterval(mouseInterval);
                }
            }
            function echoMouseCoords(){
                for (i = 0; i < mouseCoords.length; i++) {
                    console.log(mouseCoords[i] + " ")
                }
            }
            /* ================== */
            
            function drawPath(pts, ctx) {
                ctx.beginPath();
                for (var i = 0; i < pts.length; i++) {
                    //var x = 30 * pts[i].x + 400;
                    var x = pts[i].x;
                    //var y = -30 * pts[i].y + 300;
                    var y =  pts[i].y;
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
                ctx.clearRect(0, 0, 800, 400);
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

                document.getElementById("btn2").onclick = function () {
                    //var v = getSelectedF();
                    //var p = getPath(fs[v], -15, 15, 0.25);
                    var p = mouseCoords;
                    //drawPath2([{x : 160, y : 200}, {x : 170, y : 210}, {x : 200, y : 230}], ctx);
                    //drawPath2([{x : 1, y : 1}, {x : 2, y : 2}, {x : 3, y : 3}], ctx);
                    drawPath2(p, ctx);
                };

                document.getElementById("btn3").onclick = function () {
                    clearCanvas(ctx);
                };
            };
        </script>
    </body>
</html>
