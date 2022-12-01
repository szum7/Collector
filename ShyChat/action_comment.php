<?php
header("Content-Type: text/html; charset=utf8");
include 'db.php';

if(isset($_POST["comment"]) && isset($_POST["user"])){
    $user = $_POST["user"];
    $comment = $_POST["comment"];
    
    $query = "INSERT INTO comments VALUES
        (NULL, '$comment', '$user', NOW());";
    $result = mysqli_query($connection_id, $query) or die("Rossz lekérdezés " . $query);
}

?>
