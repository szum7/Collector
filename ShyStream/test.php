<?php
header("Content-Type: text/html; charset=utf-8");
session_start();
$_SESSION["s1"] = "session 1 created.";
?>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title></title>
    </head>
    <body>
        <?php
        if (isset($_SESSION["s1"])) {
            echo $_SESSION["s1"];
        }
        if (isset($_SESSION["s2"])) {
            echo $_SESSION["s2"];
        }
        ?>
    </body>
</html>