<?php
header("Content-Type: text/html; charset=utf-8");
$root = "../";
require_once $root . 'db.php';
require_once $root . 'class/user.php';
require_once $root . 'class/comments.php';
require_once $root . 'class/chaters.php';
session_start();
?>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Raw comments</title>
    </head>
    <body>
        <?php
        if(isset($_SESSION["stream"])){
            $_SESSION["stream"]->refreshStream($connection_id);
        }else{
            if(!isset($_SESSION["user"])){
                $_SESSION["user"] = new user(0, "unset", "unset", "unset");
            }
            $_SESSION["stream"] = new comments($connection_id, $_SESSION["user"]);
        }
        
        echo $_SESSION["stream"]->getStream();
        ?>
    </body>
</html>