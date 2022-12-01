<?php
header("Content-Type: text/html; charset=utf8");
require_once 'db.php';
?>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf8">
        <title>Comments</title>
    </head>
    <body bgcolor="black">
        <?php
        $query="
            SELECT * 
            FROM comments 
            ORDER BY created;";
        $result = mysqli_query($connection_id, $query) or die("Rossz lekérdezés " . $query);
        
        if (mysqli_num_rows($result) > 0) {
            while ($row = mysqli_fetch_assoc($result)) {
                echo "<div style='padding:10px; margin-top:2px; background-color: #B4D9D6;'>";
                echo "<font style='color:#777777;'>[" . $row["created"] . "] </font>";
                echo "<font style='font-weight:bold;'>" . $row["user"] . ": </font>";
                echo $row["comment"];
                echo "<div style='float:right;'><a style='color:red;' href='javascript:deleteComment(" . $row["cid"] . ")'>[x]</a></div></div>";
            }
        }
        ?>
    </body>
</html>