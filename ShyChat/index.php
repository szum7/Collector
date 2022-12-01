<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Telefon 1.0</title>
        <link rel="shortcut icon" href="icon_telefon.png" />
        <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
        <style>
            * {
                font-family: Arial;
                font-size: 11pt;
            }
            a{
                text-decoration: none;
                color:white;
            }
        </style>
    </head>
    <body bgcolor="black">
        <div style="margin:auto; width:1000px;" id="border">
            <!--<form name="comments" action="action_comment.php" method="post" enctype="multipart/form-data">-->
            <div style="margin:auto; width:900px;">

                <div id="div_comments"></div>

                <div>
                    <input type="text" style="width:878px; margin-top: 4px; padding:10px;" id="user" name="user" placeholder="Felhasználónév..." />
                    <textarea style="
                              width:878px; 
                              height:100px; 
                              margin-top:5px; 
                              padding:10px;"
                              name="comment" id="comment" placeholder="..." ></textarea><br/>
                    <div style="padding:5px; border:1px solid white; display: inline-block;">
                        <a href="javascript:void(0)" id="sendCommentButton">Hozzászólás küldése</a>
                    </div>
                </div>
            </div>
            <!--</form>-->
        </div>
        <!--<p style="color:white;" id="a1">a</p>
        <p style="color:white;" id="a2">b</p>-->
        <SCRIPT type="text/javascript">
            var comment = $("#comment");
            var user = $("#user");
            //var a1 = $("#a1");
            //var a2 = $("#a2");
            function refresh(){
                $("#div_comments").load("comments.php");
                //height = $('#border').height();
                //newMessage();
                //a1.text("1" + bool);
                //a2.text(height + " - " + lastHeight);
            }
            setInterval('refresh()', 10000 );
            setInterval('newMessage()', 3000 );
            
            $(document).ready(function(){
                height = $('#border').height();
                $("#div_comments").load("comments.php");
            });
            
            function sending(){
                if(comment.val().length > 0 && user.val().length > 0){
                    $.ajax({
                        type: "POST",
                        url: "action_comment.php",
                        data: "user=" + user.val() + "&comment=" + comment.val(),
                        success: function(){
                            $("#div_comments").load("comments.php");
                        }
                    });
                    document.getElementById("comment").value = "";
                } else {
                    alert("Valami nem jó.");
                }
                return false;
            }
                
            function deleteComment(cid){
                $.ajax({
                    type: "POST",
                    url: "action_deleteComment.php",
                    data: "cid=" + cid,
                    success: function(){
                        $("#div_comments").load("comments.php");
                    }
                });
                document.getElementById("comment").value = "";
            }
                
            $("#sendCommentButton").click(function(){
                sending();
            });
            
            document.onkeyup = KeyCheck;       
            function KeyCheck(e){
                var KeyID = (window.event) ? event.keyCode : e.keyCode;
                switch(KeyID)
                {
                    case 13:
                        if(comment.val().length > 0 && user.val().length > 0){
                            sending();
                        }
                        break; 
                }
            }
            var bool = true;
            var height = 0;
            var lastHeight = 0;
            $("#border").mouseover(function() {
                height = $('#border').height();
                lastHeight = $('#border').height();
                document.title = "Telefon 1.0";
                bool = true;
                window.clearInterval(interval_prr);
                PrrBool = -1;
            }).mouseout(function() {
                bool = false;
            });
            
            function newMessage(){
                if(!bool){
                    if(height != lastHeight){
                        Prr();
                    }
                }
                height = $('#border').height();
            }
            
            
            var szoveg = Array("Pr", "Prr", "Prrr", "Prrrr", "Prrrr", "Prrrrr");
            var PrrBool = -1;
            var interval_prr = "";
            function Prr(){
                if (PrrBool == -1){
                    interval_prr = setInterval("Prr()", 350);
                    PrrBool = 0;
                }
                document.title = "" + szoveg[PrrBool] + " Telefon 1.0";
                PrrBool++;
                if (PrrBool >= szoveg.length){
                    PrrBool = 0;
                }
            }
            
        </SCRIPT>
    </body>
</html>
