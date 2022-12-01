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
        <title>Raw chaters</title>
    </head>
    <body>
        <?php
        if(isset($_SESSION["chaters"])){
            $_SESSION["chaters"]->refreshChaters($connection_id);
        }else{
            $_SESSION["chaters"] = new chaters($connection_id);
        }
        
        echo $_SESSION["chaters"]->getChaters();
        ?>
    </body>
</html>